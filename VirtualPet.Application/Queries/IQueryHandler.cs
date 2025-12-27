namespace VirtualPet.Application.Queries;

public interface IQueryHandler<TQuery, TResult>
{
    Task<Result<TResult>> HandleAsync(TQuery query, CancellationToken cancellationToken);
}

public interface IQueryHandler<TQuery>
{
    Task<Result> HandleAsync(TQuery query, CancellationToken cancellationToken);
}

public interface IQuery<TResult>
{
}

public interface IQuery
{
}