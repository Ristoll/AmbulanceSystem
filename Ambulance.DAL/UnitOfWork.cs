using Ambulance.Core.Entities;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;

namespace AmbulanceSystem.DAL;

public class UnitOfWork : IUnitOfWork
{
    private bool disposedValue;

    private readonly AmbulanceDbContext context;
    private readonly IGenericRepository<Person>? personRepository;
    private readonly IGenericRepository<UserRole>? userRoleRepository;
    private readonly IGenericRepository<BrigadeMember>? brigadeMemberRepository;
    private readonly IGenericRepository<Allergy>? allergyRepository;
    private readonly IGenericRepository<Brigade>? brigadeRepository;
    private readonly IGenericRepository<BrigadeItem>? brigadeItemRepository;
    private readonly IGenericRepository<Call>? callRepository;
    private readonly IGenericRepository<ChronicDecease>? chronicDeceaseRepository;
    private readonly IGenericRepository<PatientChronicDecease>? patientChronicDeceaseRepository;
    private readonly IGenericRepository<Hospital>? hospitalRepository;
    private readonly IGenericRepository<ActionLog>? logRepository;
    private readonly IGenericRepository<MedicalRecord>? medicalRecordRepository;
    private readonly IGenericRepository<MedicalCard>? medicalCardRepository;
    private readonly IGenericRepository<Item>? itemRepository;

    public IGenericRepository<Person> PersonRepository => personRepository ?? new GenericRepository<Person>(context);
    public IGenericRepository<UserRole> UserRoleRepository => userRoleRepository ?? new GenericRepository<UserRole>(context);
    public IGenericRepository<BrigadeMember> BrigadeMemberRepository => brigadeMemberRepository ?? new GenericRepository<BrigadeMember>(context);
    public IGenericRepository<Allergy> BidRepository => allergyRepository ?? new GenericRepository<Allergy>(context);
    public IGenericRepository<Brigade> BrigadeRepository => brigadeRepository ?? new GenericRepository<Brigade>(context);
    public IGenericRepository<Allergy> AllergyRepository => allergyRepository ?? new GenericRepository<Allergy>(context);
    public IGenericRepository<BrigadeItem> BrigadeItemRepository => brigadeItemRepository ?? new GenericRepository<BrigadeItem>(context);
    public IGenericRepository<Call> CallRepository => callRepository ?? new GenericRepository<Call>(context);
    public IGenericRepository<ChronicDecease> ChronicDeceaseRepository => chronicDeceaseRepository ?? new GenericRepository<ChronicDecease>(context);
    public IGenericRepository<PatientChronicDecease> PatientChronicDeceaseRepository => patientChronicDeceaseRepository ?? new GenericRepository<PatientChronicDecease>(context);
    public IGenericRepository<Hospital> HospitalRepository => hospitalRepository ?? new GenericRepository<Hospital>(context);
    public IGenericRepository<ActionLog> ActionLogRepository => logRepository ?? new GenericRepository<ActionLog>(context);
    public IGenericRepository<MedicalRecord> MedicalRecordRepository => medicalRecordRepository ?? new GenericRepository<MedicalRecord>(context);
    public IGenericRepository<MedicalCard> MedicalCardRepository => medicalCardRepository ?? new GenericRepository<MedicalCard>(context);
    public IGenericRepository<Item> ItemRepository => itemRepository ?? new GenericRepository<Item>(context);

    public UnitOfWork(AmbulanceDbContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));

        this.context = context;
    }

    public void Save()
    {
        context.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                context.Dispose();
            }

            disposedValue = true;
        }
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
