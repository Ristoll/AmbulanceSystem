using System;
using System.Collections.Generic;
using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.Core.Entities.Types;
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

    public virtual DbSet<BrigadeMemberRole> BrigadeMemberRoles { get; set; }

    public virtual DbSet<BrigadeState> BrigadeStates { get; set; }

    public virtual DbSet<BrigadeType> BrigadeTypes { get; set; }

    public virtual DbSet<Call> Calls { get; set; }

    public virtual DbSet<ChronicDecease> ChronicDeceases { get; set; }

    public virtual DbSet<Dispatcher> Dispatchers { get; set; }

    public virtual DbSet<Hospital> Hospitals { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<ItemType> ItemTypes { get; set; }

    public virtual DbSet<ActionLog> Logs { get; set; }

    public virtual DbSet<MedicalCard> MedicalCards { get; set; }

    public virtual DbSet<MedicalRecord> MedicalRecords { get; set; }

    public virtual DbSet<MemberSpecializationType> MemberSpecializationTypes { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<PatientAllergy> PatientAllergies { get; set; }

    public virtual DbSet<PatientChronicDecease> PatientChronicDeceases { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<UnitType> UnitTypes { get; set; }

    public virtual DbSet<UsedItem> UsedItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-N27H27E;Initial Catalog=AmbulanceDB;Integrated Security=True;TrustServerCertificate=True;", x => x.UseNetTopologySuite());

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Allergy>(entity =>
        {
            entity.HasKey(e => e.AllergyId).HasName("PK__Allergie__A49EBE62287A0FF1");
        });

        modelBuilder.Entity<Brigade>(entity =>
        {
            entity.HasKey(e => e.BrigadeId).HasName("PK__Brigades__BEF4D27ABB3E4894");

            entity.HasOne(d => d.BrigadeState).WithMany(p => p.Brigades)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Brigades__Brigad__68487DD7");

            entity.HasOne(d => d.BrigadeType).WithMany(p => p.Brigades)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Brigades__Brigad__693CA210");

            entity.HasOne(d => d.CurrentCall).WithMany(p => p.Brigades).HasConstraintName("FK__Brigades__Curren__6B24EA82");

            entity.HasOne(d => d.Hospital).WithMany(p => p.Brigades).HasConstraintName("FK__Brigades__Hospit__6A30C649");
        });

        modelBuilder.Entity<BrigadeItem>(entity =>
        {
            entity.HasOne(d => d.Brigade).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrigadeIt__Briga__797309D9");

            entity.HasOne(d => d.Item).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrigadeIt__ItemI__7A672E12");
        });

        modelBuilder.Entity<BrigadeMember>(entity =>
        {
            entity.HasKey(e => e.BrigadeMemberId).HasName("PK__BrigadeM__C9F8EBE09BDDE5EE");

            entity.HasOne(d => d.Brigade).WithMany(p => p.BrigadeMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrigadeMe__Briga__6EF57B66");

            entity.HasOne(d => d.MemberSpecializationType).WithMany(p => p.BrigadeMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrigadeMe__Membe__70DDC3D8");

            entity.HasOne(d => d.Person).WithMany(p => p.BrigadeMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrigadeMe__Perso__6E01572D");

            entity.HasOne(d => d.Role).WithMany(p => p.BrigadeMembers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BrigadeMe__RoleI__6FE99F9F");
        });

        modelBuilder.Entity<BrigadeMemberRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__BrigadeM__8AFACE3AE2A7509A");
        });

        modelBuilder.Entity<BrigadeState>(entity =>
        {
            entity.HasKey(e => e.BrigadeStateId).HasName("PK__BrigadeS__0D98F412624209F8");
        });

        modelBuilder.Entity<BrigadeType>(entity =>
        {
            entity.HasKey(e => e.BrigadeTypeId).HasName("PK__BrigadeT__05E32278AE70EFC4");
        });

        modelBuilder.Entity<Call>(entity =>
        {
            entity.HasKey(e => e.CallId).HasName("PK__Calls__5180CF8A87396CB6");

            entity.Property(e => e.StartCallTime).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.UrgencyType).IsFixedLength();

            entity.Property(e => e.Address).HasColumnType("geography");

            entity.HasOne(d => d.Caller).WithMany(p => p.Calls)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calls__CallerID__5AEE82B9");

            entity.HasOne(d => d.Dispatcher).WithMany(p => p.Calls)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calls__Dispatche__5CD6CB2B");

            entity.HasOne(d => d.Hospital).WithMany(p => p.Calls).HasConstraintName("FK__Calls__HospitalI__5DCAEF64");

            entity.HasOne(d => d.Patient).WithMany(p => p.Calls)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Calls__PatientID__5BE2A6F2");
        });

        modelBuilder.Entity<ChronicDecease>(entity =>
        {
            entity.HasKey(e => e.ChronicDeceaseId).HasName("PK__ChronicD__19BE9B69A9CDFA44");
        });

        modelBuilder.Entity<Dispatcher>(entity =>
        {
            entity.HasKey(e => e.DispatcherId).HasName("PK__Dispatch__EB9ED16490B52B2B");

            entity.HasOne(d => d.Person).WithMany(p => p.Dispatchers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Dispatche__Perso__5629CD9C");
        });

        modelBuilder.Entity<Hospital>(entity =>
        {
            entity.HasKey(e => e.HospitalId).HasName("PK__Hospital__38C2E58FE76ECE52");

            entity.Property(e => e.Location).HasColumnType("geography");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Items__727E83EB9174E450");

            entity.HasOne(d => d.ItemType).WithMany(p => p.Items)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Items__ItemTypeI__6477ECF3");

            entity.HasOne(d => d.UnitType).WithMany(p => p.Items)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Items__UnitTypeI__656C112C");
        });

        modelBuilder.Entity<ItemType>(entity =>
        {
            entity.HasKey(e => e.ItemTypeId).HasName("PK__ItemType__F51540DB2895E86C");
        });

        modelBuilder.Entity<ActionLog>(entity =>
        {
            entity.HasKey(e => e.ActionLogId).HasName("PK__Logs__5E5499A8BE0AD6AE");

            entity.HasOne(d => d.Person).WithMany(p => p.Logs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Logs__PersonID__7D439ABD");
        });

        modelBuilder.Entity<MedicalCard>(entity =>
        {
            entity.HasKey(e => e.MedicalCardId).HasName("PK__MedicalC__931EC2364A6F4869");

            entity.Property(e => e.CreationDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Patient).WithMany(p => p.MedicalCards)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalCa__Patie__3E52440B");
        });

        modelBuilder.Entity<MedicalRecord>(entity =>
        {
            entity.HasKey(e => e.MedicalRecordId).HasName("PK__MedicalR__4411BBC28333CCED");

            entity.Property(e => e.DataTime).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.BrigadeMember).WithMany(p => p.MedicalRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalRe__Briga__75A278F5");

            entity.HasOne(d => d.MedicalCard).WithMany(p => p.MedicalRecords)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MedicalRe__Medic__74AE54BC");
        });

        modelBuilder.Entity<MemberSpecializationType>(entity =>
        {
            entity.HasKey(e => e.SpecializationTypeId).HasName("PK__MemberSp__69C4C25D0170F360");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.PatientId).HasName("PK__Patients__970EC346FA31F8A8");

            entity.HasOne(d => d.Person).WithMany(p => p.Patients)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Patients__Person__3A81B327");
        });

        modelBuilder.Entity<PatientAllergy>(entity =>
        {
            entity.HasKey(e => e.PatientAllergyId).HasName("PK__PatientA__2844415DFB6706F2");

            entity.HasOne(d => d.Allergy).WithMany(p => p.PatientAllergies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PatientAl__Aller__440B1D61");

            entity.HasOne(d => d.MedicalCard).WithMany(p => p.PatientAllergies)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PatientAl__Medic__4316F928");
        });

        modelBuilder.Entity<PatientChronicDecease>(entity =>
        {
            entity.HasKey(e => e.PatientChronicDeceaseId).HasName("PK__PatientC__8EBED7D73E1A4801");

            entity.HasOne(d => d.ChronicDecease).WithMany(p => p.PatientChronicDeceases)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PatientCh__Chron__49C3F6B7");

            entity.HasOne(d => d.MedicalCard).WithMany(p => p.PatientChronicDeceases)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PatientCh__Medic__48CFD27E");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("PK__People__AA2FFB8539E01ADF");

            entity.Property(e => e.Gender).IsFixedLength();

            entity.Property(e => e.Address).HasColumnType("geography");

            entity.Property(e => e.Role)
            .HasConversion(
                v => v.ToString(), // зберігатиме enum Role як строку, але при цьому залишиться тип Role коли витягнемо з бази
                v => ParseUserRole(v)); // конвертуватиме строку назад в enum Role + виніс як окремий метод, бо Expression-bodied members не підтримують складні вирази
        });

        modelBuilder.Entity<UnitType>(entity =>
        {
            entity.HasKey(e => e.UnitTypeId).HasName("PK__UnitType__1B7AB91494C44223");
        });

        modelBuilder.Entity<UsedItem>(entity =>
        {
            entity.HasKey(e => e.UsedItemId).HasName("PK__UsedItem__0A16E7D8EF129D7C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    private static UserRole ParseUserRole(string? v)
        => !string.IsNullOrEmpty(v) && Enum.TryParse<UserRole>(v, out var result) ? result : UserRole.Unknown;

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
