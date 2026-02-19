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
    public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
    {
        public void Configure(EntityTypeBuilder<Workout> builder)
        {
            builder.ToTable("Workouts");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnType("char(36)")
                .HasConversion<Guid>()
                .IsRequired();

            builder.Property(e => e.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(e => e.Type)
                .HasConversion<string>()
                .HasColumnType("varchar(40)")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property<Guid>("MemberId")
                .HasColumnType("char(36)")
                .IsRequired();

            builder.HasOne(w => w.Member)
                .WithMany(m => m.Workouts)
                .HasForeignKey(w => w.MemberId)
                .IsRequired() 
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex("MemberId")
                .HasDatabaseName("IX_Workouts_MemberId");

            builder.HasMany(w => w.Exercises)
                .WithMany() // Exercise não tem referência de volta
                .UsingEntity<Dictionary<string, object>>(
                    "WorkoutExercises", // Nome da tabela de junção
                    j => j
                        .HasOne<Exercise>() // Lado Exercise da relação
                        .WithMany() // Exercise não tem coleção de Workouts
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade), // Se deletar Workout, remove associação
                    j => j
                        .HasOne<Workout>() // Lado Workout da relação
                        .WithMany() // Workout já tem a coleção Exercises
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade), // Se deletar Exercise, remove associação
                    j =>
                    {
                        j.ToTable("WorkoutExercises"); // Tabela de junção
                        j.HasKey("WorkoutId", "ExerciseId"); // Chave composta

                        // Índices para performance
                        j.HasIndex("ExerciseId");
                        j.HasIndex("WorkoutId");
                    });

            builder.Property(i => i.CreatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            builder.Property(i => i.UpdatedAt)
                .HasColumnType("datetime")
                .HasDefaultValueSql("CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

        }
    }
}
