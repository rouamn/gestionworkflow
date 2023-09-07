using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using gestionworkflow.Models;

namespace gestionworkflow.Context;

public partial class DbContextName : DbContext
{
    public DbContextName()
    {
    }

    public DbContextName(DbContextOptions<DbContextName> options)
        : base(options)
    {
    }

    public virtual DbSet<DemandeAvance> DemandeAvances { get; set; }

    public virtual DbSet<DemandeConge> DemandeConges { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-4557G2R\\SQLEXPRESS;Initial Catalog=GestionWorkflow;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DemandeAvance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DemandeAvance");

            entity.ToTable("DemandeAvance");

            //entity.Property(e => e.Id)
            //    .ValueGeneratedNever()
               // .HasColumnName("id");
            entity.Property(e => e.DateDemande)
                .HasColumnType("date")
                .HasColumnName("dateDemande");
            entity.Property(e => e.Montant).HasColumnName("montant");
            entity.Property(e => e.Statut)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("statut");
            entity.Property(e => e.UtilisateurId).HasColumnName("utilisateur_id");

          //  entity.HasOne(d => d.Utilisateur).WithMany(p => p.DemandeAvances)
              //  .HasForeignKey(d => d.UtilisateurId)
               // .HasConstraintName("FK__DemandeAv__utili__3D5E1FD2");
        });

        modelBuilder.Entity<DemandeConge>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DemandeConge");
    
            entity.ToTable("DemandeConge");

            //entity.Property(e => e.Id)
            //   // .ValueGeneratedNever()
            //    .HasColumnName("id");
            entity.Property(e => e.Commentaire)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("commentaire");
            entity.Property(e => e.DateDebut)
                .HasColumnType("date")
                .HasColumnName("dateDebut");
            entity.Property(e => e.DateFin)
                .HasColumnType("date")
                .HasColumnName("dateFin");
            entity.Property(e => e.Statut)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("statut");
            entity.Property(e => e.Email)
    .HasMaxLength(50)
    .IsUnicode(false)
    .HasColumnName("Email");
            entity.Property(e => e.UtilisateurId).HasColumnName("utilisateur_id");

            //entity.HasOne(d => d.Utilisateur).WithMany(p => p.DemandeConges)
                //.HasForeignKey(d => d.UtilisateurId)
              //  .HasConstraintName("FK__DemandeCo__utili__3A81B327");
        });

        modelBuilder.Entity<User>(entity =>
        {
           
            entity.HasKey(e => e.Id).HasName("PK__users__3213E83F8709FF5E");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ__users__AB6E6164E3709AA8").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
