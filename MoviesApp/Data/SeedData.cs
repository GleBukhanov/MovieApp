using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesApp.Models;

namespace MoviesApp.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MoviesContext(
                       serviceProvider.GetRequiredService<
                           DbContextOptions<MoviesContext>>()))
            {
                // Look for any movies.
                if (context.Actors.Any())
                {
                    return; // DB has been seeded
                }
                context.Actors.AddRange(
                    new Actor
                    {
                        FirstName = "Russell",
                        LastName = "Crow",
                        Birthday = DateTime.Parse("1964-4-7")
                    },
                    new Actor
                    {
                        FirstName ="Leonardo",
                        LastName = "DiCaprio",
                        Birthday = DateTime.Parse("1972-11-11")
                    },
                    new Actor
                    {
                        FirstName ="Readley",
                        LastName= "Scott",
                        Birthday = DateTime.Parse("1937-11-30")
                    },
                    new Actor
                    {
                        FirstName= "Raffy",
                        LastName = "Cassidy",
                        Birthday = DateTime.Parse("2002-8-30")
                    },
                    new Actor
                    {
                        FirstName= "Julia",
                        LastName = "Roberts",
                        Birthday = DateTime.Parse("1967-10-28")
                    }
                );

                context.SaveChanges();
                
                // Look for any movies.
                if (context.Movies.Any())
                {
                    return; // DB has been seeded
                }

                context.Movies.AddRange(
                    new Movie
                    {
                        Title = "When Harry Met Sally",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Genre = "Romantic Comedy",
                        Price = 7.99M
                    },
                    new Movie
                    {
                        Title = "Ghostbusters ",
                        ReleaseDate = DateTime.Parse("1984-3-13"),
                        Genre = "Comedy",
                        Price = 8.99M
                    },
                    new Movie
                    {
                        Title = "Ghostbusters 2",
                        ReleaseDate = DateTime.Parse("1986-2-23"),
                        Genre = "Comedy",
                        Price = 9.99M
                    },
                    new Movie
                    {
                        Title = "Joan Red",
                        ReleaseDate = DateTime.Parse("1959-4-15"),
                        Genre = "Drama",
                        Price = 3.99M
                    }
                );

                context.SaveChanges();
            }
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                roleManager.CreateAsync(new IdentityRole { Name = "Admin" }).Wait();
            }
                
            if (userManager.FindByEmailAsync("leader@movie.com").Result == null)
            {
                var user = new ApplicationUser
                {
                    UserName = "leader@movie.com",
                    Email = "leader@movie.com",
                    FirstName = "Ghost",
                    LastName = "Admin"
                };

                IdentityResult result = userManager.CreateAsync(user, "fljk,fkb").Result;
 
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}