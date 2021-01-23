using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Infra.Data.Context
{
    public static class SeedData
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                // Exit if DB has been seeded
                if (await context.NaturalPeople.AnyAsync())
                    return;

                context.NaturalPeople.AddRange(
                    new NaturalPerson(
                        "Mario de Andrade",
                        "719.032.860-26",
                        DateTime.Parse("1893-10-9"),
                        NaturalPerson.GenderType.Male,
                        "mario@andrade.com")
                    {
                        Address = new Address(
                            "Brazil", "São Paulo", "São Paulo", "01045-001", "Praça da República", null)
                    },
                    new NaturalPerson(
                        "Tarsila do Amaral",
                        "114.939.250-91",
                        DateTime.Parse("1886-9-1"),
                        NaturalPerson.GenderType.Female,
                        "tarsila@amaral.com")
                );

                context.LegalPeople.AddRange(
                    new LegalPerson(
                        "Telebrás Ltda.",
                        "Telebrás",
                        "03.309.337/0001-73")
                    {
                        Address = new Address(
                            "Brazil", "Rio de Janeiro", "Rio de Janeiro", "02412-850", "Praça Maua", null)
                    }
                );


                context.SaveChanges();
            }
        }
    }
}
