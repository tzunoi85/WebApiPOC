using Application.Accounts.Dtos;
using Application.Accounts.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Accounts.MappingProfiles
{
    public class AccountDtoProfile
        : Profile
    {
        public AccountDtoProfile()
        {
            CreateMap<Account, AccountDto>()
                .ForMember(d => d.Id, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.Password, o => o.MapFrom(s => s.Password))
                .ForMember(d => d.Groups, o => o.MapFrom(s => s.AccountGroups.Select(ag => ag.Group.Name).AsQueryable<string>()));
        }
    }
}
