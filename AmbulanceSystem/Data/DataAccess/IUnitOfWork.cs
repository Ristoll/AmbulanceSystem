using AmbulanceSystem.Core.Entities;
using AmbulanceSystem.Core.Entities.Types;

namespace AmbulanceSystem.Core.Data;

public interface IUnitOfWork : IDisposable
{
    public IGenericRepository<Person> PersonRepository { get; }
    public IGenericRepository<Patient> PatientRepository { get; }
    public IGenericRepository<BrigadeMember> BrigadeMemberRepository { get; }
    public IGenericRepository<Dispatcher> DispatcherRepository { get; }

    public IGenericRepository<Allergy> AllergyRepository { get; }
    public IGenericRepository<Brigade> BrigadeRepository { get; }
    public IGenericRepository<BrigadeItem> BrigadeItemRepository { get; }
    public IGenericRepository<Call> CallRepository { get; }
    public IGenericRepository<ChronicDecease> ChronicDeceaseRepository { get; }
    public IGenericRepository<Hospital> HospitalRepository { get; }
    public IGenericRepository<UsedItem> UsedItemRepository { get; }

    public IGenericRepository<ActionLog> LogRepository { get; }
    public IGenericRepository<MedicalRecord> MedicalRecordRepository { get; }
    public IGenericRepository<MedicalCard> MedicalCardRepository { get; }

    void Save();
}
