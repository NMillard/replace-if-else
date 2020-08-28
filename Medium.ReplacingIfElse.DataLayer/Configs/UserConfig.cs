using System;
using System.Collections.Generic;
using Medium.ReplacingIfElse.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medium.ReplacingIfElse.DataLayer.Configs {
    public class UserConfig : IEntityTypeConfiguration<User> {
        public void Configure(EntityTypeBuilder<User> builder) {
            builder.ToTable("Users", "Application");

            builder.HasKey("id"); // Because id is private
            builder.HasIndex(user => user.Email).IsUnique();
            
            builder.Property(user => user.Email)
                .HasMaxLength(150) // optimization
                .IsRequired();

            builder.OwnsOne<Address>(
                navigationName: nameof(User.Address),
                buildAction: addressBuilder => {
                    addressBuilder.Property(address => address.StreetName).HasMaxLength(150).IsRequired();
                    addressBuilder.Property(address => address.StreetNumber).HasMaxLength(10).IsRequired();
                });
            
            
            // Seed some users <- this will be part of the migration script
            builder.HasData(new List<User> {
                new User(Guid.Parse("BE20FF2C-D1E6-47D4-8A2C-40CD0E528C6C"), "someuser@user.dk"),
                new User(Guid.Parse("7D0CC1C1-F97C-479E-9DF2-560C8293E934"), "otheruser@user.dk"),
                new User(Guid.Parse("E8F2BCE5-C55F-4965-89F3-71439FFFFE3A"), "lastuser@user.dk"),
            });
        }
    }
}