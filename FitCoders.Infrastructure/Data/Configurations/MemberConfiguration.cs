using FitCoders.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace FitCoders.Infrastructure.Data.Configurations
{
    public class MemberConfiguration : IEntityTypeConfiguration<Member>
    {
        public void Configure(EntityTypeBuilder<Member> builder)
        {
            builder.ToTable("Members");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnType("char(36)")
                .HasConversion<Guid>()
                .IsRequired();

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(m => m.Cpf)
                .IsRequired()
                .HasMaxLength(11)
                .HasColumnType("varchar(11)")
                .IsFixedLength();

            builder.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)");

            builder.Property(m => m.Weight)
                .IsRequired()
                .HasPrecision(5, 2) 
                .HasColumnType("decimal(5,2)");

            builder.Property(m => m.Age)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(m => m.DateOfBirth)
                .IsRequired()
                .HasConversion<DateOnlyConverter>()
                .HasColumnType("date");

            builder.Property(m => m.MembershipPlan)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(20)
                .HasColumnType("varchar(20)");

            builder.Property(m => m.RenewalDate)
                .IsRequired()
                .HasConversion<DateOnlyConverter>()
                .HasColumnType("date");

            builder.Property(m => m.IsMembershipActive)
                .IsRequired()
                .HasColumnType("tinyint(1)");

            builder.Property(m => m.IsCoached)
                .IsRequired()
                .HasColumnType("tinyint(1)");

            builder.Property(m => m.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(m => m.UpdatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            builder.Property(m => m.InstructorId)
                .HasColumnType("char(36)");

            builder.HasOne(m => m.Instructor)           
               .WithMany(i => i.Clients)              
               .HasForeignKey(m => m.InstructorId)          
               .IsRequired(false)                      
               .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(m => m.Workouts)
                .WithOne(m => m.Member)
                .HasForeignKey("MemberId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(m => m.Cpf)
                .IsUnique()
                .HasDatabaseName("IX_Members_Cpf");

            builder.HasIndex(m => m.Email)
                .IsUnique()
                .HasDatabaseName("IX_Members_Email");

            builder.HasIndex(m => m.MembershipPlan)
                .HasDatabaseName("IX_Members_MembershipPlan");

            builder.HasIndex(m => m.IsMembershipActive)
                .HasDatabaseName("IX_Members_IsMembershipActive");

            builder.HasIndex(m => m.IsCoached)
                .HasDatabaseName("IX_Members_IsCoached");

            builder.HasIndex(m => new { m.IsMembershipActive, m.MembershipPlan })
                .HasDatabaseName("IX_Members_Active_Plan");

            builder.HasIndex(m => new { m.IsCoached, m.InstructorId })
                .HasDatabaseName("IX_Members_Coached_Instructor");
        }
        public class DateOnlyConverter : ValueConverter<DateOnly, DateTime>
        {
            public DateOnlyConverter()
                : base(
                    dateOnly => dateOnly.ToDateTime(TimeOnly.MinValue),
                    dateTime => DateOnly.FromDateTime(dateTime))
            {
            }
        }
    }
}