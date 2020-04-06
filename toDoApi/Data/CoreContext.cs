using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

using toDoApi.Data.Entities;

namespace toDoApi.Data
{
    public partial class CoreContext : DbContext
    {
        public CoreContext () { }

        public CoreContext (DbContextOptions<CoreContext> options) : base (options) { }

        public virtual DbSet<ToDo> ToDo { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql ("Host=localhost;Database=postgres;Username=postgres;Password=mysecretpassword");
            }
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo> (entity =>
            {
                entity.Property (e => e.Id).HasDefaultValueSql ("nextval('\"untitled_table_Id_seq\"'::regclass)");

                entity.Property (e => e.Name).HasColumnType ("character varying");
            });

            modelBuilder.Entity<User> (entity =>
            {
                entity.Property (e => e.Id).HasDefaultValueSql ("nextval('\"untitled_table_Id_seq\"'::regclass)");

                entity.Property (e => e.Email).HasColumnType ("character varying");

                entity.Property (e => e.Password).HasColumnType ("bytea");

                entity.Property (e => e.Salt).HasColumnType ("bytea");
            });

            OnModelCreatingPartial (modelBuilder);
        }

        partial void OnModelCreatingPartial (ModelBuilder modelBuilder);
    }
}