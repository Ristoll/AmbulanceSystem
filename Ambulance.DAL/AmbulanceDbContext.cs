using System;
using System.Collections.Generic;
using AmbulanceSystem.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Ambulance.Core.Entities;

public partial class AmbulanceDbContext : DbContext
{
    public AmbulanceDbContext()
    {
    }

    public AmbulanceDbContext(DbContextOptions<AmbulanceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ActionLog> ActionLogs { get; set; }

    public virtual DbSet<Allergy> Allergies { get; set; }

    public virtual DbSet<Brigade> Brigades { get; set; }

    public virtual DbSet<BrigadeItem> BrigadeItems { get; set; }

    public virtual DbSet<BrigadeMember> BrigadeMembers { get; set; }

    public virtual DbSet<BrigadeMemberRole> BrigadeMemberRoles { get; set; }

    public virtual DbSet<BrigadeState> BrigadeStates { get; set; }

    public virtual DbSet<BrigadeType> BrigadeTypes { get; set; }

    public virtual DbSet<Call> Calls { get; set; }

    public virtual DbSet<ChronicDecease> ChronicDeceases { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemType> ItemTypes { get; set; }

    public virtual DbSet<MedicalCard> MedicalCards { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<MemberSpecializationType> MemberSpecializationTypes { get; set; }

    public virtual DbSet<PatientAllergy> PatientAllergies { get; set; }

    public virtual DbSet<PatientChronicDecease> PatientChronicDeceases { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-N27H27E;Initial Catalog=AmbulanceDB;Integrated Security=True;TrustServerCertificate=True;", x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActionLog>(entity =>
        {
            entity.HasKey(e => e.ActionLogId).HasName("PK__ActionLo__428D61A2FD8D203F");

            entity.HasOne(d => d.Person).WithMany(p => p.ActionLogs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ActionLog__Perso__74AE54BC");
        });

        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.HasKey(e => e.AllergyId).HasName("PK__Allergie__A49EBE62A43266C6");
        });

        modelBuilder.Entity<Brigade>(entity =>
        {
            entity.HasKey(e => e.BrigadeId).HasName("PK__Brigades__BEF4D27AB40797CC");

            entity.HasOne(d => d.BrigadeState).WithMany(p => p.Brigades)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Brigades__Brigad__619B8048");

            entity.HasOne(d => d.BrigadeType).WithMany(p => p.Brigades)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Brigades__Brigad__628FA481");

            entity.HasOne(d => d.CurrentCall).WithMany(p => p.Brigades).HasConstraintName("FK__Brigades__Curren__6477ECF3");

            entity.HasOne(d => d.Hospital).WithMany(p => p.Brigades).HasConstraintName("FK__Brigades__Hospit__6383C8BA");
        });

        modelBuilder.Entity<BrigadeItem>(entity =>
        {
            entity.HasOne(d => d.Brigade).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrigadeIt__Briga__70DDC3D8");

            entity.HasOne(d => d.Item).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrigadeIt__ItemI__71D1E811");
        });

        modelBuilder.Entity<BrigadeMember>(entity =>
        {
            entity.HasKey(e => e.BrigadeMemberId).HasName("PK__BrigadeM__C9F8EBE06D601AF5");

            entity.HasOne(d => d.Brigade).WithMany(p => p.BrigadeMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrigadeMe__Briga__68487DD7");

            entity.HasOne(d => d.BrigadeMemberRole).WithMany(p => p.BrigadeMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrigadeMe__Briga__693CA210");

            entity.HasOne(d => d.MemberSpecializationType).WithMany(p => p.BrigadeMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrigadeMe__Membe__6A30C649");

            entity.HasOne(d => d.Person).WithMany(p => p.BrigadeMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrigadeMe__Perso__6754599E");
        });

        modelBuilder.Entity<BrigadeMemberRole>(entity =>
        {
            entity.HasKey(e => e.BrigadeMemberRoleId).HasName("PK__BrigadeM__2D54FB89AA9530CE");

            entity.HasOne(d => d.Person).WithMany(p => p.BrigadeMemberRoles).HasConstraintName("FK__BrigadeMem__Name__5165187F");
        });

        modelBuilder.Entity<BrigadeState>(entity =>
        {
            entity.HasKey(e => e.BrigadeStateId).HasName("PK__BrigadeS__0D98F4124B1241FE");
        });

        modelBuilder.Entity<BrigadeType>(entity =>
        {
            entity.HasKey(e => e.BrigadeTypeId).HasName("PK__BrigadeT__05E32278C58BFB09");
        });

        modelBuilder.Entity<Call>(entity =>
        {
            entity.HasKey(e => e.CallId).HasName("PK__Calls__5180CF8A70EFE4A7");

            entity.Property(e => e.StartCallTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UrgencyType).IsFixedLength();

            entity.HasOne(d => d.Dispatcher).WithMany(p => p.CallDispatchers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calls__Dispatche__59063A47");

            entity.HasOne(d => d.Hospital).WithMany(p => p.Calls).HasConstraintName("FK__Calls__HospitalI__59FA5E80");

            entity.HasOne(d => d.Patient).WithMany(p => p.CallPatients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calls__PatientID__5812160E");
        });

        modelBuilder.Entity<ChronicDecease>(entity =>
        {
            entity.HasKey(e => e.ChronicDeceaseId).HasName("PK__ChronicD__19BE9B6916B29BE5");
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(e => e.HospitalId).HasName("PK__Hospital__38C2E58FDE64DAC2");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Items__727E83EBA35769A6");

            entity.HasOne(d => d.ItemType).WithMany(p => p.Items)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Items__ItemTypeI__5EBF139D");
        });

        modelBuilder.Entity<ItemType>(entity =>
        {
            entity.HasKey(e => e.ItemTypeId).HasName("PK__ItemType__F51540DB163B591A");
        });

        modelBuilder.Entity<MedicalCard>(entity =>
        {
            entity.HasKey(e => e.MedicalCardId).HasName("PK__MedicalC__931EC2360894FD4D");

            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Person).WithMany(p => p.MedicalCards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalCa__Perso__3D5E1FD2");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.MedicalRecordId).HasName("PK__MedicalR__4411BBC27CC55CD7");

            entity.Property(e => e.DataTime).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.BrigadeMember).WithMany(p => p.MedicalRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalRe__Briga__6EF57B66");

            entity.HasOne(d => d.MedicalCard).WithMany(p => p.MedicalRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalRe__Medic__6E01572D");
        });

        modelBuilder.Entity<MemberSpecializationType>(entity =>
        {
            entity.HasKey(e => e.SpecializationTypeId).HasName("PK__MemberSp__69C4C25D7081A584");
        });

        modelBuilder.Entity<PatientAllergy>(entity =>
        {
            entity.HasKey(e => e.PatientAllergyId).HasName("PK__PatientA__2844415DD694106F");

            entity.HasOne(d => d.Allergy).WithMany(p => p.PatientAllergies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PatientAl__Aller__4316F928");

            entity.HasOne(d => d.MedicalCard).WithMany(p => p.PatientAllergies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PatientAl__Medic__4222D4EF");
        });

        modelBuilder.Entity<PatientChronicDecease>(entity =>
        {
            entity.HasKey(e => e.PatientChronicDeceaseId).HasName("PK__PatientC__8EBED7D74ED10B33");

            entity.HasOne(d => d.ChronicDecease).WithMany(p => p.PatientChronicDeceases)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PatientCh__Chron__48CFD27E");

            entity.HasOne(d => d.MedicalCard).WithMany(p => p.PatientChronicDeceases)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PatientCh__Medic__47DBAE45");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__People__AA2FFB85F329FA5E");

            entity.HasOne(d => d.UserRole).WithMany(p => p.People).HasConstraintName("FK__People__Image_Ur__398D8EEE");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.UserRoleId).HasName("PK__UserRole__3D978A35E8922A2D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
