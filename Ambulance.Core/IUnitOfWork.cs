using AmbulanceSystem.Core.Entities;

namespace AmbulanceSystem.Core;

public interface IUnitOfWork : IDisposable
{
    public IGenericRepository<ActionLog> ActionLogRepository { get; }
    public IGenericRepository<Allergy> AllergyRepository { get; }
    public IGenericRepository<Brigade> BrigadeRepository { get; }
    public IGenericRepository<BrigadeItem> BrigadeItemRepository { get; }
    public IGenericRepository<BrigadeMember> BrigadeMemberRepository { get; }
    public IGenericRepository<Call> CallRepository { get; }
    public IGenericRepository<ChronicDecease> ChronicDeceaseRepository { get; }
    public IGenericRepository<Hospital> HospitalRepository { get; }
    public IGenericRepository<Item> ItemRepository { get; }
    public IGenericRepository<MedicalCard> MedicalCardRepository { get; }
    public IGenericRepository<MedicalRecord> MedicalRecordRepository { get; }
    public IGenericRepository<Person> PersonRepository { get; }
    void Save();
}