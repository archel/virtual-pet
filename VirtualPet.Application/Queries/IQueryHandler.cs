namespace VirtualPet.Application.Queries;

public interface IQueryHandler<TQuery, TResult>
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}

public interface IQueryHandler<TQuery>
{
    Task HandleAsync(TQuery query, CancellationToken cancellationToken);
}

public interface IQuery<TResult>
{
}

public interface IQuery
{
}