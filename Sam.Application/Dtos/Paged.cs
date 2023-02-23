namespace Sam.Application.Dtos;

public record Paged<T>(IList<T> Data, int PageIndex, int PageSize, int PageCount, int Count);
