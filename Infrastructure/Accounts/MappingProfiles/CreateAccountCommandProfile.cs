using System;
using Application.Accounts.Messages.Commands;
using Application.Accounts.Models;
using AutoMapper;

namespace Infrastructure.Accounts.MappingProfiles
{
    public class CreateAccountCommandProfile
        :Profile
    {
        public CreateAccountCommandProfile()
        {
            CreateMap<CreateAccountCommand, Account>()
              .ForMember(d => d.Email, o => o.MapFrom(s => s.Email))
              .ForMember(d => d.Password, o => o.MapFrom(s => s.Password));
        }
    }
}
