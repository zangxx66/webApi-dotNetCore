using System;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Migrations {
    public class Seed {
        public static void AddUsr (Context db) {
            db.Database.EnsureCreated ();
            
            var admin = new User {
                UserName = "admin",
                Password = "123456",
                NickName = "admin",
                Avatar = @"https://i.pximg.net/img-original/img/2018/05/18/21/20/29/68805417_p0.png",
                Sex = (int) UserSex.保密,
                QQ = 123456,
                RegIp = "127.0.0.1",
                RegDate = DateTime.Now,
                Role = (int) UserRole.Admin
            };
            var user = new User {
                UserName = "aaa",
                Password = "123456",
                NickName = "aaa",
                Avatar = @"https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/d3/d3aca32af230afc296cbc4a4296a02c1adc0e6bc_full.jpg",
                Sex = (int) UserSex.保密,
                QQ = 123456,
                RegIp = "127.0.0.1",
                RegDate = DateTime.Now,
                Role = (int) UserRole.User
            };
            db.User.Add (admin);
            db.User.Add (user);
            db.SaveChanges ();
        }

        public static void AddCategory (Context db) {
            db.Database.EnsureCreated ();
            var categoryItem = new Category[] {
                new Category { Name = "c#", Sort = 0 },
                new Category { Name = "vuejs", Sort = 1 },
                new Category { Name = ".net", Sort = 2 },
                new Category { Name = "cpp", Sort = 3 },
                new Category { Name = "游戏开发", Sort = 4 }
            };
            foreach (var item in categoryItem) {
                db.Category.Add (item);
            }
            db.SaveChanges ();
        }
    }
}