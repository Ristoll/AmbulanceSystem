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

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<MedicalCard> MedicalCards { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<PatientAllergy> PatientAllergies { get; set; }

    public virtual DbSet<PatientChronicDecease> PatientChronicDeceases { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<SpecializationType> SpecializationTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost,1433;Database=AmbulanceSystemDB;User Id=sa;Password=StrongPassword123!;TrustServerCertificate=True;", x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.HasKey(e => e.AllergyId).HasName("PK__Allergie__ACDD0692D29233C3");

            entity.HasIndex(e => e.Name, "UQ__Allergie__72E12F1B8D47EE21").IsUnique();

            entity.Property(e => e.AllergyId).HasColumnName("allergy_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Brigade>(entity =>
        {
            entity.HasKey(e => e.BrigadeId).HasName("PK__Brigade__54869992ED3C89B4");

            entity.ToTable("Brigade");

            entity.Property(e => e.BrigadeId).HasColumnName("brigade_id");
            entity.Property(e => e.BrigadeState)
                .HasMaxLength(50)
                .HasColumnName("brigade_state");
            entity.Property(e => e.BrigadeType)
                .HasMaxLength(50)
                .HasColumnName("brigade_type");
            entity.Property(e => e.CurrentCallId).HasColumnName("current_call_id");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");

            entity.HasOne(d => d.Hospital).WithMany(p => p.Brigades)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Brigade_Hospital");
        });

        modelBuilder.Entity<BrigadeItem>(entity =>
        {
            entity.HasKey(e => e.BrigadeItemId).HasName("PK__BrigadeI__957E46059F50EA91");

            entity.ToTable("BrigadeItem");

            entity.Property(e => e.BrigadeItemId).HasColumnName("brigade_item_id");
            entity.Property(e => e.BrigadeId).HasColumnName("brigade_id");
            entity.Property(e => e.ExpiryDate).HasColumnName("expiry_date");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Brigade).WithMany(p => p.BrigadeItems)
                .HasForeignKey(d => d.BrigadeId)
                .HasConstraintName("FK_BrigadeItem_Brigade");

            entity.HasOne(d => d.Item).WithMany(p => p.BrigadeItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BrigadeItem_Item");
        });

        modelBuilder.Entity<BrigadeMember>(entity =>
        {
            entity.HasKey(e => e.BrigadeMemberId).HasName("PK__BrigadeM__B3853E0BE3C35E3F");

            entity.ToTable("BrigadeMember");

            entity.Property(e => e.BrigadeMemberId).HasColumnName("brigade_member_id");
            entity.Property(e => e.BrigadeId).HasColumnName("brigade_id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.SpecializationTypeId).HasColumnName("specialization_type_id");

            entity.HasOne(d => d.Brigade).WithMany(p => p.BrigadeMembers)
                .HasForeignKey(d => d.BrigadeId)
                .HasConstraintName("FK_BrigadeMember_Brigade");

            entity.HasOne(d => d.Person).WithMany(p => p.BrigadeMembers)
                .HasForeignKey(d => d.PersonId)
                .HasConstraintName("FK_BrigadeMember_Person");

            entity.HasOne(d => d.SpecializationType).WithMany(p => p.BrigadeMembers)
                .HasForeignKey(d => d.SpecializationTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BrigadeMember_Specialization");
        });

        modelBuilder.Entity<Call>(entity =>
        {
            entity.HasKey(e => e.CallId).HasName("PK__Call__427DCE68DFFFF39D");

            entity.ToTable("Call");

            entity.Property(e => e.CallId).HasColumnName("call_id");
            entity.Property(e => e.CallAddress)
                .HasMaxLength(255)
                .HasColumnName("call_address");
            entity.Property(e => e.CallAt).HasColumnName("call_at");
            entity.Property(e => e.CallState)
                .HasMaxLength(50)
                .HasColumnName("call_state");
            entity.Property(e => e.DispatcherId).HasColumnName("dispatcher_id");
            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.MedicalRecordId).HasColumnName("medical_record_id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.UrgencyType)
                .HasMaxLength(50)
                .HasColumnName("urgency_type");

            entity.HasOne(d => d.Dispatcher).WithMany(p => p.CallDispatchers)
                .HasForeignKey(d => d.DispatcherId)
                .HasConstraintName("FK_Call_Dispatcher");

            entity.HasOne(d => d.Hospital).WithMany(p => p.Calls)
                .HasForeignKey(d => d.HospitalId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Call_Hospital");

            entity.HasOne(d => d.MedicalRecord).WithMany(p => p.Calls)
                .HasForeignKey(d => d.MedicalRecordId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Call_MedicalRecord");

            entity.HasOne(d => d.Person).WithMany(p => p.CallPeople)
                .HasForeignKey(d => d.PersonId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Call_Person");
        });

        modelBuilder.Entity<ChronicDecease>(entity =>
        {
            entity.HasKey(e => e.ChronicDeceaseId).HasName("PK__ChronicD__CC454C416F9881EA");

            entity.HasIndex(e => e.Name, "UQ__ChronicD__72E12F1B3666CB21").IsUnique();

            entity.Property(e => e.ChronicDeceaseId).HasColumnName("chronic_decease_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(e => e.HospitalId).HasName("PK__Hospital__DFF4167F6CB78A41");

            entity.ToTable("Hospital");

            entity.Property(e => e.HospitalId).HasColumnName("hospital_id");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item__52020FDDD807B781");

            entity.ToTable("Item");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.ItemType)
                .HasMaxLength(50)
                .HasColumnName("item_type");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.UnitType)
                .HasMaxLength(50)
                .HasColumnName("unit_type");
        });

        modelBuilder.Entity<MedicalCard>(entity =>
        {
            entity.HasKey(e => e.CardId).HasName("PK__MedicalC__BDF201DDD6DE50A7");

            entity.ToTable("MedicalCard");

            entity.HasIndex(e => e.PatientId, "UQ__MedicalC__4D5CE4771C91F59C").IsUnique();

            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.BloodType)
                .HasMaxLength(10)
                .HasColumnName("blood_type");
            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("creation_date");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.PatientId).HasColumnName("patient_id");
            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.HasOne(d => d.Patient).WithOne(p => p.MedicalCard)
                .HasForeignKey<MedicalCard>(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Person_Card");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__MedicalR__BFCFB4DDBB8FC0EC");

            entity.ToTable("MedicalRecord");

            entity.Property(e => e.RecordId).HasColumnName("record_id");
            entity.Property(e => e.BrigadeMemberId).HasColumnName("brigade_member_id");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.DateTime)
                .HasDefaultValueSql("(sysdatetime())")
                .HasColumnName("date_time");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Notes).HasColumnName("notes");

            entity.HasOne(d => d.BrigadeMember).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.BrigadeMemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MedicalRecord_BrigadeMember");

            entity.HasOne(d => d.Card).WithMany(p => p.MedicalRecords)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("FK_MedicalRecord_Card");
        });

        modelBuilder.Entity<PatientAllergy>(entity =>
        {
            entity.HasKey(e => e.PatientAllergyId).HasName("PK__PatientA__81BD324EE81ADF56");

            entity.Property(e => e.PatientAllergyId).HasColumnName("patient_allergy_id");
            entity.Property(e => e.AllergyId).HasColumnName("allergy_id");
            entity.Property(e => e.CardId).HasColumnName("card_id");

            entity.HasOne(d => d.Allergy).WithMany(p => p.PatientAllergies)
                .HasForeignKey(d => d.AllergyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientAllergies_Allergy");

            entity.HasOne(d => d.Card).WithMany(p => p.PatientAllergies)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("FK_PatientAllergies_Card");
        });

        modelBuilder.Entity<PatientChronicDecease>(entity =>
        {
            entity.HasKey(e => e.PatientChronicDeceaseId).HasName("PK__PatientC__3E123485EC545A7A");

            entity.Property(e => e.PatientChronicDeceaseId).HasColumnName("patient_chronic_decease_id");
            entity.Property(e => e.CardId).HasColumnName("card_id");
            entity.Property(e => e.ChronicDeceaseId).HasColumnName("chronic_decease_id");

            entity.HasOne(d => d.Card).WithMany(p => p.PatientChronicDeceases)
                .HasForeignKey(d => d.CardId)
                .HasConstraintName("FK_PatientChronic_Card");

            entity.HasOne(d => d.ChronicDecease).WithMany(p => p.PatientChronicDeceases)
                .HasForeignKey(d => d.ChronicDeceaseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PatientChronic_Decease");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__Person__543848DF56D493C5");

            entity.ToTable("Person");

            entity.HasIndex(e => e.Login, "UQ__Person__7838F272A5D00C77").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Person__AB6E616454B41C00").IsUnique();

            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("image_url");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .HasColumnName("middle_name");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .HasColumnName("surname");
            entity.Property(e => e.UserRole)
                .HasMaxLength(50)
                .HasColumnName("user_role");
        });

        modelBuilder.Entity<SpecializationType>(entity =>
        {
            entity.HasKey(e => e.SpecializationTypeId).HasName("PK__Speciali__81BB5F3F819B8D98");

            entity.ToTable("SpecializationType");

            entity.HasIndex(e => e.Name, "UQ__Speciali__72E12F1B6C4E6858").IsUnique();

            entity.Property(e => e.SpecializationTypeId).HasColumnName("specialization_type_id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}