using System;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models {
    public class Context : DbContext {
        public Context (DbContextOptions<Context> options) : base (options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Logs> Logs { get; set; }
    }
}