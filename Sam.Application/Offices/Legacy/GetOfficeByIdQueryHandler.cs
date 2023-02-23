﻿using Microsoft.EntityFrameworkCore;
using Sam.Application.Default;
using Sam.Application.Interfaces;
using Sam.Domain;

namespace Sam.Application.Offices.Legacy;

public record struct GetOfficeByIdQuery(string OfficeCode) : IQuery<Office?>;

public class GetOfficeByIdQueryHandler : RequestHandlerBase, IQueryHandler<GetOfficeByIdQuery, Office?> {
    public GetOfficeByIdQueryHandler(IApplicationDbContext context) : base(context) { }

    public Office? Handle(GetOfficeByIdQuery query) {
        return _context.Offices
            .Where(o => o.OfficeCode == query.OfficeCode)
            .Include(o => o.Staff)
            .FirstOrDefault();
    }
}
