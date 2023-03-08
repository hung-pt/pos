using Ddd.Application.Dtos;
using MediatR;
using Ddd.Application.Offices;
using System.ComponentModel;

namespace multi_threadings.Routes;

public static class OfficeApi {
    public static RouteGroupBuilder MapOfficesApi(this RouteGroupBuilder group) {
        group.MapGet("/", async (IMediator md, [DefaultValue(1)] int? pageIndex, [DefaultValue(10)] int? pageSize) =>
             await md.Send(new GetOfficesQuery(pageIndex ?? 1, pageSize ?? 10)));

        group.MapGet("/{id}",
            async (IMediator mediator, string id) => {
                var response = await mediator.Send(new GetOfficeByIdQuery(id));
                return response == null ? Results.NotFound() : Results.Ok(response);
            });

        group.MapPost("/", async (IMediator mediator, CancellationToken cancellationToken, OfficeCreateDto dto) => {
            var response = await mediator.Send( // todo: add mapper
                new AddOfficeCommand(dto.OfficeCode, dto.City, dto.Phone, dto.AddressLine1, dto.AddressLine2, dto.State, dto.Country, dto.PostalCode, dto.Territory),
                cancellationToken);
            return Results.Ok(response);
        });

        group.MapDelete("/{id}", async (IMediator mediator, CancellationToken ccToken, string id) => {
            var response = await mediator.Send(new RemoveOfficeCommand(id), ccToken);
            return response == null ? (IResult)TypedResults.NotFound() : TypedResults.Ok(response.OfficeCode);
        });

        group.MapMethods("/options-or-head", new[] { "OPTIONS", "HEAD" },
                                  () => "This is an options or head request ");

        return group;
    }



}
