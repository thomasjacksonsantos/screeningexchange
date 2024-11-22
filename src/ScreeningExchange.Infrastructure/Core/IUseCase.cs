namespace ScreeningExchange.Infrastructure.Core;

public interface IUseCase<in TInputPort, TResult>
{
    ValueTask<TResult> Execute(TInputPort input, CancellationToken ct = default);
}


public interface IOutputPortUseCase<TOutputPort, TResult>
   where TOutputPort : IOutputPort<TResult>
{
    ValueTask<TResult> Execute(CancellationToken cancellationToken = default);
}

public interface IInputOutputPortUseCase<TInputPort, TOutputPort, TResult>
   where TOutputPort : IOutputPort<TResult>
{
    ValueTask<TResult> Execute(TInputPort inputPort, CancellationToken cancellationToken = default);
}