using System;
using Application.Accounts.Dtos;
using Application.Accounts.Models;
using AutoMapper;

namespace Infrastructure.Accounts.MappingProfiles
{
    public class PermissionDtoProfile
        : Profile
    {
        public PermissionDtoProfile()
        {
            this.CreateMap<AccountGroups, PermissionsDto>()
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Account.Email))
                .ForMember(d => d.Group, o => o.MapFrom(s => s.Group.Name));
        }
    }
}
