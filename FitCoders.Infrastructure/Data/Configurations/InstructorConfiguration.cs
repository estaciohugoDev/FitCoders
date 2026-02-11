using FitCoders.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCoders.Infrastructure.Data.Configurations
{
    public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> builder)
        {
            builder.ToTable("Instructors");

            builder.HasKey(e => e.GetId());

            builder.Property(e => e.GetId())
                .HasColumnType("char(36)")
                .HasConversion<Guid>()
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasColumnType("varchar(100)")
                .HasMaxLength(100);

            builder.Property(e => e.Cpf)
                .IsRequired()
                .HasColumnType("char(11)")
                .HasMaxLength(11)
                .IsFixedLength();

            builder.Property(e => e.Shift)
                .IsRequired()
                .HasConversion<string>()
                .HasColumnType("varchar(20)")
                .HasMaxLength(20);

            builder.HasMany("_clients")
                .WithOne()
                .HasForeignKey("InstructorId")
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(i => i.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(i => i.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();


            builder.HasIndex(e => e.Cpf)
                .IsUnique()
                .HasDatabaseName("IX_Instructors_Cpf");

            builder.HasIndex(e => e.Email)
                .IsUnique()
                .HasDatabaseName("IX_Instructors_Email");

            builder.HasIndex(e => e.Shift)
                .HasDatabaseName("IX_Instructors_Shift");
        }
    }
}
