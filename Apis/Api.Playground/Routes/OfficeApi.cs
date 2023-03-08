using Ddd.Application.Dtos;
using MediatR;
using Sam.Application.Offices;
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

        // Idempotency: request gui nhieu lan nhung ket qua van nhu mong muon
        // 
        // vd: POST (ko Idempotent) neu gui nhieu lan se insert mot dong moi
        // POST /add_row HTTP/1.1
        // POST /add_row HTTP/1.1   -> Adds a 2nd row
        // POST /add_row HTTP/1.1   -> Adds a 3rd row

        // DELETE hay PUT (Idempotent) thi chi update hay xoa moi record do
        // DELETE /idX/delete HTTP/1.1   -> Returns 200 if idX exists
        // DELETE /idX/delete HTTP/1.1   -> Returns 404 as it just got deleted
        // DELETE /idX/delete HTTP/1.1   -> Returns 404
        //group.MapPut("/{id}", Update); 

        group.MapDelete("/{id}", async (IMediator mediator, CancellationToken ccToken, string id) => {
            var response = await mediator.Send(new RemoveOfficeCommand(id), ccToken);
            return response == null ? (IResult)TypedResults.NotFound() : TypedResults.Ok(response.OfficeCode);
        });
        // OPTIONS: thong tin api, co nhung http method nao
        // HEAD: thong in resource
        group.MapMethods("/options-or-head", new[] { "OPTIONS", "HEAD" },
                                  () => "This is an options or head request ");

        return group;
    }



}
