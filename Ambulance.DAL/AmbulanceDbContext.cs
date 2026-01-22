using System;
using System.Collections.Generic;
using Ambulance.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmbulanceSystem.DAL;

public partial class AmbulanceDbContext : DbContext
{
    public AmbulanceDbContext()
    {
    }

    public AmbulanceDbContext(DbContextOptions<AmbulanceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Allergy> Allergies { get; set; }

    public virtual DbSet<Brigade> Brigades { get; set; }

    public virtual DbSet<BrigadeItem> BrigadeItems { get; set; }

    public virtual DbSet<BrigadeMember> BrigadeMembers { get; set; }

    public virtual DbSet<Call> Calls { get; set; }

    public virtual DbSet<ChronicDecease> ChronicDeceases { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<MedicalCard> MedicalCards { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<PatientAllergy> PatientAllergies { get; set; }

    public virtual DbSet<PatientChronicDecease> PatientChronicDeceases { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<SpecializationType> SpecializationTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.HasKey(e => e.AllergyId).HasName("PK__Allergie__ACDD0692D29233C3");
        });

        modelBuilder.Entity<Brigade>(entity =>
        {
            entity.HasKey(e => e.BrigadeId).HasName("PK__Brigade__54869992ED3C89B4");
        });

        modelBuilder.Entity<BrigadeItem>(entity =>
        {
            entity.HasKey(e => e.BrigadeItemId).HasName("PK__BrigadeI__957E46059F50EA91");

            entity.HasOne(d => d.Brigade).WithMany(p => p.BrigadeItems).HasConstraintName("FK_BrigadeItem_Brigade");

            entity.HasOne(d => d.Item).WithMany(p => p.BrigadeItems)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BrigadeItem_Item");
        });

        modelBuilder.Entity<BrigadeMember>(entity =>
        {
            entity.HasKey(e => e.BrigadeMemberId).HasName("PK__BrigadeM__B3853E0BE3C35E3F");

            entity.HasOne(d => d.Brigade).WithMany(p => p.BrigadeMembers).HasConstraintName("FK_BrigadeMember_Brigade");

            entity.HasOne(d => d.Person).WithMany(p => p.BrigadeMembers).HasConstraintName("FK_BrigadeMember_Person");
            entity.HasOne(d => d.SpecializationType).WithMany(p => p.BrigadeMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BrigadeMember_Specialization");
        });

        modelBuilder.Entity<Call>(entity =>
        {
            entity.HasKey(e => e.CallId).HasName("PK__Call__427DCE68DFFFF39D");

            entity.HasOne(d => d.Dispatcher).WithMany(p => p.Calldispatchers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Call_Dispatcher");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.Calls)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Call_MedicalRecord");

            entity.HasOne(d => d.Person).WithMany(p => p.Callpeople)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Call_Person");
        });

        modelBuilder.Entity<ChronicDecease>(entity =>
        {
            entity.HasKey(e => e.ChronicDeceaseId).HasName("PK__ChronicD__CC454C416F9881EA");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item__52020FDDD807B781");
        });

        modelBuilder.Entity<MedicalCard>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__MedicalC__BDF201DDD6DE50A7");

            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__MedicalR__BFCFB4DDBB8FC0EC");

            entity.Property(e => e.DateTime).HasDefaultValueSql("(sysdatetime())");

            entity.HasOne(d => d.BrigadeMember).WithMany(p => p.MedicalRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalRecord_BrigadeMember");

            entity.HasOne(d => d.Card).WithMany(p => p.MedicalRecords).HasConstraintName("FK_MedicalRecord_Card");
        });

        modelBuilder.Entity<PatientAllergy>(entity =>
        {
            entity.HasKey(e => e.PatientAllergyId).HasName("PK__PatientA__81BD324EE81ADF56");

            entity.HasOne(d => d.Allergy).WithMany(p => p.PatientAllergies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientAllergies_Allergy");

            entity.HasOne(d => d.Card).WithMany(p => p.PatientAllergies).HasConstraintName("FK_PatientAllergies_Card");
        });

        modelBuilder.Entity<PatientChronicDecease>(entity =>
        {
            entity.HasKey(e => e.PatientChronicDeceaseId).HasName("PK__PatientC__3E123485EC545A7A");

            entity.HasOne(d => d.Card).WithMany(p => p.PatientChronicDeceases).HasConstraintName("FK_PatientChronic_Card");

            entity.HasOne(d => d.ChronicDecease).WithMany(p => p.PatientChronicDeceases)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientChronic_Decease");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Person__543848DF56D493C5");

            entity.HasOne(d => d.Card).WithMany(p => p.People)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Person_Card");
        });

        modelBuilder.Entity<SpecializationType>(entity =>
        {
            entity.HasKey(e => e.SpecializationTypeId).HasName("PK__Speciali__81BB5F3F819B8D98");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
