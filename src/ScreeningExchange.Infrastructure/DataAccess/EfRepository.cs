using System.Linq.Expressions;
using ScreeningExchange.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace ScreeningExchange.Infrastructure.DataAccess;

public interface IUnitOfWork
{
    Task<IUnitOfWorkTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    void EnsureIsAttached<TEntity>(TEntity entity) where TEntity : class;
    void SetModified<TEntity>(TEntity entity) where TEntity : class;
}

public interface IUnitOfWorkAttachedRepository
{
    IUnitOfWork UnitOfWork { get; }
}

public interface IUnitOfWorkTransaction : IDisposable
{
    Guid? TransactionId { get; }
    Task Commit(CancellationToken cancellationToken);
    Task Rollback(CancellationToken cancellationToken);
}

public class EfUnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : DbContext
{
    public TDbContext CurrentContext { get; }

    public EfUnitOfWork(TDbContext context) =>
        CurrentContext = context ?? throw new ArgumentNullException(nameof(context));

    public async Task<IUnitOfWorkTransaction> BeginTransactionAsync(CancellationToken cancellationToken) =>
        new EfUnitOfWorkTransaction(await CurrentContext.Database.BeginTransactionAsync(cancellationToken));

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken) =>
        CurrentContext.SaveChangesAsync(cancellationToken);

    public void EnsureIsAttached<TEntity>(TEntity entity) where TEntity : class
    {
        var enttityEntry = CurrentContext.Entry(entity);

        if (enttityEntry.State == EntityState.Detached)
        {
            CurrentContext.Attach(enttityEntry);
        }
    }

    public void SetModified<TEntity>(TEntity entity) where TEntity : class
    {
        EnsureIsAttached(entity);
        var enttityEntry = CurrentContext.Entry(entity);
        enttityEntry.State = EntityState.Modified;
    }
}

public class EfUnitOfWorkAttachedRepository<TDbContext, TEntity> : IUnitOfWorkAttachedRepository
    where TDbContext : DbContext
    where TEntity : class
{
    #region Properties

    public IUnitOfWork UnitOfWork { get; }
    public TDbContext CurrentContext { get; init; }

    #endregion

    #region Constructor

    public EfUnitOfWorkAttachedRepository(EfUnitOfWork<TDbContext> unitOfWork)
    {
        UnitOfWork = unitOfWork;
        CurrentContext = unitOfWork.CurrentContext;
    }

    #endregion

    #region Methods

    protected DbSet<TEntity> Queryable() =>
        CurrentContext.Set<TEntity>();

    protected IQueryable<TEntity> QueryableWithIncludes<TProperty>(
        Expression<Func<TEntity, TProperty>>[] eagerLoadingPaths)
    {
        var queryable = Queryable().AsNoTracking();

        if (eagerLoadingPaths == null) return queryable;

        foreach (var path in eagerLoadingPaths)
        {
            queryable = queryable.Include(path);
        }

        return queryable;
    }

    protected Task<int> Count(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) =>
            Queryable().Where(predicate).CountAsync(cancellationToken);

    protected ValueTask<TEntity> Find(
        object key,
        CancellationToken cancellationToken = default) =>
            Queryable().FindAsync(new object[1] { key }, cancellationToken);

    protected async Task<IEnumerable<TEntity>> GetAll(
        CancellationToken cancellationToken = default)
    {
        return await Queryable()
            .ToListAsync(cancellationToken);
    }

    protected Task<TEntity?> FindFirstByPredicate(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) =>
            Queryable()
                .FirstOrDefaultAsync(predicate, cancellationToken: cancellationToken);

    protected Task<TEntity?> FindFirstByPredicate(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>[] eagerLoadingPaths,
        CancellationToken cancellationToken = default) =>
            QueryableWithIncludes(eagerLoadingPaths)
                .FirstOrDefaultAsync(predicate, cancellationToken);

    protected Task<List<TEntity>> FindAllByPredicate(
        Expression<Func<TEntity, bool>> predicate,
        CancellationToken cancellationToken = default) =>
            Queryable()
                .Where(predicate)
                .ToListAsync(cancellationToken);

    protected Task<List<TEntity>> FindAllByPredicate(
        Expression<Func<TEntity, bool>> predicate,
        Expression<Func<TEntity, object>>[] eagerLoadingPaths,
        CancellationToken cancellationToken = default) =>
            QueryableWithIncludes(eagerLoadingPaths)
                .Where(predicate)
                .ToListAsync(cancellationToken);

    protected async Task<PagedResult<TEntity>> FindAllByQueryable(
        IQueryable<TEntity> queryable,
        Pagination pagination,
        CancellationToken cancellationToken = default)
    {
        var totalRecords = await queryable.CountAsync(cancellationToken);

        var pagedResult = await queryable
            .Skip((pagination.CurrentPage - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ToListAsync(cancellationToken);

        return new PagedResult<TEntity>
        {
            TotalRecords = totalRecords,
            CurrentPage = pagination.CurrentPage,
            PageSize = pagination.PageSize,
            Records = pagedResult.AsEnumerable()
        };
    }

    protected Task<PagedResult<TEntity>> FindAllByPredicate(
        Expression<Func<TEntity, bool>> predicate,
        Pagination pagination,
        CancellationToken cancellationToken = default)
    {
        var queryable = Queryable().Where(predicate);
        return FindAllByQueryable(queryable, pagination, cancellationToken);
    }

    protected Task<PagedResult<TEntity>> FindAllByPredicate(
        Expression<Func<TEntity, bool>> predicate,
        Pagination pagination,
        Expression<Func<TEntity, object>>[] eagerLoadingPaths,
        CancellationToken cancellationToken = default)
    {
        var queryable = QueryableWithIncludes(eagerLoadingPaths).Where(predicate);
        return FindAllByQueryable(queryable, pagination, cancellationToken);
    }

    protected Task<int> SaveChanges(CancellationToken cancellationToken) =>
         CurrentContext.SaveChangesAsync(cancellationToken);

    protected void MarkAs(TEntity entity, EntityState entityState)
    {
        CurrentContext.Entry(entity).State = entityState;
    }

    protected EfUnitOfWorkAttachedRepository<TDbContext, TEntity> Add(TEntity entity)
    {
        MarkAs(entity, EntityState.Added);
        return this;
    }

    protected EfUnitOfWorkAttachedRepository<TDbContext, TEntity> Update(TEntity entity)
    {
        MarkAs(entity, EntityState.Modified);
        return this;
    }

    protected EfUnitOfWorkAttachedRepository<TDbContext, TEntity> Delete(TEntity entity)
    {
        MarkAs(entity, EntityState.Deleted);
        return this;
    }

    #endregion
}

public class EfUnitOfWorkTransaction : IUnitOfWorkTransaction
{
    private IDbContextTransaction CurrentTransaction { get; }

    public EfUnitOfWorkTransaction(IDbContextTransaction transaction) =>
        CurrentTransaction = transaction;

    public Guid? TransactionId =>
        CurrentTransaction.TransactionId;

    public Task Commit(CancellationToken cancellationToken) =>
        CurrentTransaction.CommitAsync(cancellationToken);

    public Task Rollback(CancellationToken cancellationToken) =>
        CurrentTransaction.RollbackAsync(cancellationToken);

    public void Dispose() =>
        CurrentTransaction.Dispose();
}