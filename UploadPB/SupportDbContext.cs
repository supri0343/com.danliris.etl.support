using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UploadPB.Models;
using UploadPB.Models.Temporary;

namespace UploadPB.SupporttDbContext
{
    public class SupportDbContext : DbContext
    {
        public DbSet<BeacukaiTemporaryModel> BeacukaiTemporaries { get; set; }
        public DbSet<Beacukai_Temp> BEACUKAI_TEMP { get; set; }
        public DbSet<BeacukaiDocumentsModel> BeacukaiDocuments { get; set; }
        public DbSet<Beacukai40Temporary> beacukai40Temporaries { get; set; }
        public DbSet<Beacukai23Temporary> beacukai23Temporaries { get; set; }
        public DbSet<Beacukai261Temporary> beacukai261Temporaries { get; set; }
        public DbSet<Beacukai262Temporary> beacukai262Temporaries { get; set; }
        public DbSet<BEACUKAI_ADDED> BEACUKAI_ADDED { get; set; }
        public DbSet<BEACUKAI_ADDED_DETAIL> BEACUKAI_ADDED_DETAIL { get; set; }
        public DbSet<Beacukai30HeaderTemporary> beacukai30HeaderTemporaries { get; set; }
        public DbSet<Beacukai30ItemsTemporary> Beacukai30ItemsTemporaries { get; set; }
        public DbSet<Beacukai27Temporary> beacukai27Temporaries { get; set; }
        public DbSet<Beacukai41Temporary> beacukai41Temporaries { get; set; }
        public DbSet<Beacukai25Temporary> beacukai25Temporaries { get; set; }
        public DbSet<Beacukai33HeaderTemporary> beacukai33HeaderTemporaries { get; set; }
        public DbSet<Beacukai33ItemsTemporary> Beacukai33ItemsTemporaries { get; set; }
        public SupportDbContext(DbContextOptions<SupportDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Beacukai40Temporary>(entity => { entity.ToTable("BEACUKAI40_TEMPORARY"); });
            modelBuilder.Entity<BeacukaiDocumentsModel>(entity => { entity.ToTable("BEACUKAI_DOCUMENTS"); });

            modelBuilder.Entity<BeacukaiTemporaryModel>(entity =>
            {
                //entity.HasNoKey();
                entity.ToTable("BEACUKAI_TEMPORARY");

                entity.Property(e => e.ID)
                    .HasColumnName("ID")
                    .HasMaxLength(100)
                    .IsUnicode(true);

                entity.Property(e => e.BCId)
                    .HasColumnName("BCId")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BCNo)
                   .HasColumnName("BCNo")
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.Barang)
                   .HasMaxLength(100)
                   .IsUnicode(false);

                entity.Property(e => e.Bruto).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.CIF)
                   .HasColumnName("CIF")
                   .HasColumnType("decimal(20, 2)");

                entity.Property(e => e.CIF_Rupiah)
                    .HasColumnName("CIF_Rupiah")
                    .HasColumnType("decimal(20, 2)");

                entity.Property(e => e.Keterangan)
                  .HasMaxLength(100)
                  .IsUnicode(false);

                entity.Property(e => e.JumlahSatBarang).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.KodeBarang)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.KodeKemasan)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.NamaKemasan)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.Netto).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.NoAju)
                  .HasMaxLength(50)
                  .IsUnicode(false);

                entity.Property(e => e.NamaSupplier)
                  .HasMaxLength(50)
                  .IsUnicode(false);

                entity.Property(e => e.TglDaftarAju).HasColumnType("datetime");

                entity.Property(e => e.TglBCNO)
                   .HasColumnName("TglBCNo")
                   .HasColumnType("datetimeoffset(7)");

                entity.Property(e => e.Valutta)
                    .HasColumnName("Valuta")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Hari)
                   .HasColumnName("Hari")
                   .HasColumnType("datetime");

                entity.Property(e => e.JenisBC)
                   .HasMaxLength(100)
                   .IsUnicode(false);

                entity.Property(e => e.IDHeader)
                    .HasColumnName("IDHeader")
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.JenisDokumen)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.NomorDokumen)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.TanggalDokumen).HasColumnType("datetimeoffset(7)");

                entity.Property(e => e.JumlahBarang);

                entity.Property(e => e.Sat)
                   .HasMaxLength(5)
                   .IsUnicode(false);

                entity.Property(e => e.KodeSupplier)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.TglDatang).HasColumnType("datetime");

                entity.Property(e => e.CreatedBy)
                   .HasMaxLength(50)
                   .IsUnicode(false);

                entity.Property(e => e.Vendor)
                   .HasMaxLength(100)
                   .IsUnicode(false);

            });
        }
    }
}
