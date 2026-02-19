using FitCoders.Domain.Entities;
using FitCoders.Domain.Enums;
using FitCoders.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitCoders.Infrastructure
{
    public static class DbSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.Exercises.AnyAsync()) return;

            var exercise1 = new Exercise(Guid.NewGuid(), "Push ups", 3, 12, 120);
            var exercise2 = new Exercise(Guid.NewGuid(), "Pull ups", 4, 10, 120);
            var exercise3 = new Exercise(Guid.NewGuid(), "Squats", 4, 10, 120);

            context.Exercises.AddRange(exercise1, exercise2, exercise3);

            await context.SaveChangesAsync();

            var instructor = new Instructor(
                Guid.NewGuid(),
                "Gustavo Fring",
                "12345678901",
                "fringalicious@gmail.com",
                InstructorShift.Morning);

            context.Instructor.Add(instructor);
            
            await context.SaveChangesAsync();

            var member = new Member(
                Guid.NewGuid(),
                "John Member",
                "98765432101",
                "johnismember@gmail.com",
                new DateTime(1990, 5, 15),
                Membership.Monthly,
                75.5m,
                instructor);

            context.Member.Add(member);
            await context.SaveChangesAsync();

            var treinoA = new Workout(
                Guid.NewGuid(),
                "Treino A - Superiores",
                WorkoutType.Strenght,
                member
            );

            var treinoB = new Workout(
                Guid.NewGuid(),
                "Treino B - Inferiores",
                WorkoutType.Strenght,
                member
            );

            context.Workout.AddRange(treinoA, treinoB);
            await context.SaveChangesAsync();

            treinoA.AddExercise(exercise1);
            treinoA.AddExercise(exercise2);
            treinoB.AddExercise(exercise3);

            await context.SaveChangesAsync();

            Console.WriteLine("✅ example data inserted!");
        }
    }
}
