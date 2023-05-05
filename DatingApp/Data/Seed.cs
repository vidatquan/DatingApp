﻿using DatingApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DatingApp.Data
{
    public class Seed
    {
        //public static async Task SeedUsers(DataContext context)
        public static async Task SeedUsers(UserManager<AppUser> userManager
            ,RoleManager<AppRole> roleManager)
        {
            //if (await context.Users.AnyAsync()) return;
            if (await userManager.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("Data/UserSeedData.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var users = JsonSerializer.Deserialize<List<AppUser>>(userData, options);
            //foreach (var user in users)
            //{
            //using var hmac = new HMACSHA512();

            //user.UserName = user.UserName.ToLower();
            //user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("1"));
            //user.PasswordSalt = hmac.Key;

            //context.Users.Add(user);
            //}

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"},
                new AppRole{Name = "Moderator"},
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Aq123456");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Aq123456");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Moderator" });

            // await context.SaveChangesAsync();

        }
    }
}

