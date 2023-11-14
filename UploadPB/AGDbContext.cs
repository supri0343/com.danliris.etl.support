using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UploadPB.Models.AGSupport;
using UploadPB.Models.Temporary.AGSupport;

namespace UploadPB.SupporttDbContext.AG
{
    public class AGDbContext : DbContext
    {
      
        public DbSet<BEACUKAI_ADDED> BEACUKAI_ADDED { get; set; }
        public DbSet<BEACUKAI_ADDED_DETAIL> BEACUKAI_ADDED_DETAIL { get; set; }
        public DbSet<Beacukai30HeaderTemporary> beacukai30HeaderTemporaries { get; set; }
        public DbSet<Beacukai30ItemsTemporary> Beacukai30ItemsTemporaries { get; set; }

        public AGDbContext(DbContextOptions<AGDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BeacukaiDocumentsModel>(entity => { entity.ToTable("BEACUKAI_DOCUMENTS"); });
        }
    }
}
