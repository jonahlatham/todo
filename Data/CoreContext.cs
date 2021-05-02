using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace todo.data
{
    public partial class CoreContext : DbContext
    {
        public CoreContext() { }

        public CoreContext(DbContextOptions<CoreContext> options) : base(options) { }

        public virtual DbSet<ToDo> ToDo { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=ec2-54-211-176-156.compute-1.amazonaws.com;Port=5432;Username=ktgxysykisgywk;Password=403e02b5d816f58da47424d5ab65faa5a7e55365d22ba252586155ff27198e78;Database=deq666fe1agmvm; Pooling=true; SSL Mode=Require;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>(entity =>
           {
               entity.Property(e => e.Id);
               entity.Property(e => e.Item);
               entity.Property(e => e.IsComplete);
           });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
