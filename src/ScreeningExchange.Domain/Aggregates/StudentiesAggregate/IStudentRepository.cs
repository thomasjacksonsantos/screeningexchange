

namespace ScreeningExchange.Domain.Aggregates.StudentiesAggregate;

public interface IStudentRepository
{
    ValueTask AddAsync(Student student);
    ValueTask<Student> FindAsync(Ulid id);
    ValueTask<Student> FindByEmailAsnyc(string email);
    ValueTask<IEnumerable<Student>> FindAllAsync();
}