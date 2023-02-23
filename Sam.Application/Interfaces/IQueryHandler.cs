namespace Sam.Application.Interfaces;

public interface IQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult> {
    TResult Handle(TQuery query);
}
