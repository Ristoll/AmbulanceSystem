using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.Core.Data;

namespace AmbulanceSystem.DAL;

public class UnitOfWork : IUnitOfWork
{
    private bool disposedValue;

    private readonly AmbulanceDbContext context;
    private readonly IGenericRepository<Person> personRepository;
    private readonly IGenericRepository<Patient> patientRepository;
    private readonly IGenericRepository<BrigadeMember> brigadeMemberRepository;
    private readonly IGenericRepository<Dispatcher> dispatcherRepository;

    private readonly IGenericRepository<Allergy> allergyRepository;
    private readonly IGenericRepository<Brigade> brigadeRepository;
    private readonly IGenericRepository<BrigadeItem> brigadeItemRepository;
    private readonly IGenericRepository<Call> callRepository;
    private readonly IGenericRepository<ChronicDecease> chronicDeceaseRepository;
    private readonly IGenericRepository<Hospital> hospitalRepository;
    private readonly IGenericRepository<UsedItem> usedItemRepository;
    private readonly IGenericRepository<ActionLog> logRepository;

    public IGenericRepository<Person> PersonRepository => personRepository ?? new GenericRepository<Person>(context);
    public IGenericRepository<Patient> PatientRepository => patientRepository ?? new GenericRepository<Patient>(context);
    public IGenericRepository<BrigadeMember> BrigadeMemberRepository => brigadeMemberRepository ?? new GenericRepository<BrigadeMember>(context);
    public IGenericRepository<Dispatcher> DispatcherRepository => dispatcherRepository ?? new GenericRepository<Dispatcher>(context);
    public IGenericRepository<Allergy> BidRepository => allergyRepository ?? new GenericRepository<Allergy>(context);
    public IGenericRepository<Brigade> BrigadeRepository => brigadeRepository ?? new GenericRepository<Brigade>(context);
    public IGenericRepository<Allergy> AllergyRepository => allergyRepository ?? new GenericRepository<Allergy>(context);
    public IGenericRepository<BrigadeItem> BrigadeItemRepository => brigadeItemRepository ?? new GenericRepository<BrigadeItem>(context); 
    public IGenericRepository<Call> CallRepository => callRepository ?? new GenericRepository<Call>(context);
    public IGenericRepository<ChronicDecease> ChronicDeceaseRepository => chronicDeceaseRepository ?? new GenericRepository<ChronicDecease>(context);
    public IGenericRepository<Hospital> HospitalRepository => hospitalRepository ?? new GenericRepository<Hospital>(context);
    public IGenericRepository<UsedItem> UsedItemRepository => usedItemRepository ?? new GenericRepository<UsedItem>(context);
    public IGenericRepository<ActionLog> LogRepository => logRepository ?? new GenericRepository<ActionLog>(context);

    // public IGenericRepository<AbstractSecretCodeRealizator> SecretCodeRealizatorRepository => secretCodeRealizatorRepository ?? new GenericRepository<AbstractSecretCodeRealizator>(context);


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

