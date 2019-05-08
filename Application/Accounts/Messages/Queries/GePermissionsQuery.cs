using System;
using System.Linq;
using Application.Accounts.Dtos;
using MediatR;

namespace Application.Accounts.Messages.Queries
{
    public class GePermissionsQuery
        :IRequest<IQueryable<PermissionsDto>>
    {
        public GePermissionsQuery()
        {
        }
    }
}
