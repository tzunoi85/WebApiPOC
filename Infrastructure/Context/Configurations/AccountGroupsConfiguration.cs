using System;
using Application.Accounts.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Context.Configurations
{
    public class AccountGroupsConfiguration
        : IEntityTypeConfiguration<AccountGroups>
    {
        public void Configure(EntityTypeBuilder<AccountGroups> builder)
        {
            builder.ToTable("AccountGroups");

            builder.HasKey(p => new { p.AccountId, p.GroupId });

            builder.HasOne(p => p.Account)
                .WithMany(p => p.AccountGroups)
                .HasForeignKey(p => p.AccountId);

            builder.HasOne(p => p.Group)
                .WithMany(p => p.AccountGroups)
                .HasForeignKey(p => p.GroupId);
        }
    }
}
