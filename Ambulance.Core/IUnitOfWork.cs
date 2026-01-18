using Ambulance.Core.Entities;

namespace AmbulanceSystem.Core;

public interface IUnitOfWork : IDisposable
{
    public IGenericRepository<Allergy> AllergyRepository { get; }
    public IGenericRepository<Brigade> BrigadeRepository { get; }
    public IGenericRepository<BrigadeItem> BrigadeItemRepository { get; }
    public IGenericRepository<BrigadeMember> BrigadeMemberRepository { get; }
    public IGenericRepository<BrigadeMemberRole> BrigadeMemberRoleRepository { get; }
    public IGenericRepository<MemberSpecializationType> BrigadeMemberSpecializationTypeRepository { get; }
    public IGenericRepository<BrigadeType> BrigadeTypeRepository { get; }
    public IGenericRepository<Call> CallRepository { get; }
    public IGenericRepository<ChronicDecease> ChronicDeceaseRepository { get; }
    public IGenericRepository<PatientChronicDecease> PatientChronicDeceaseRepository { get; }
    public IGenericRepository<PatientAllergy> PatientAllergyRepository { get; }
    public IGenericRepository<Hospital> HospitalRepository { get; }
    public IGenericRepository<Item> ItemRepository { get; }
    public IGenericRepository<ItemType> ItemTypeRepository { get; }
    public IGenericRepository<MedicalCard> MedicalCardRepository { get; }
    public IGenericRepository<MedicalRecord> MedicalRecordRepository { get; }
    public IGenericRepository<Person> PersonRepository { get; }
    void Save();
}