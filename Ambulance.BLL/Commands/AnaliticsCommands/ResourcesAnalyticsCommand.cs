using AutoMapper;
using AmbulanceSystem.Core;

namespace Ambulance.BLL.Commands.AnaliticsCommands
{
    public class ResourcesAnalyticsCommand : AbstrCommandWithDA<Dictionary<string, int>>
    {
        public override string Name => "Аналітика айтемів";

        public ResourcesAnalyticsCommand(IUnitOfWork operateUnitOfWork, IMapper mapper)
            : base(operateUnitOfWork, mapper) { }

        public override Dictionary<string, int> Execute()
        {
            // Беремо всі айтеми
            var items = dAPoint.ItemRepository
                .GetAll()
                .Where(i => i != null && !string.IsNullOrEmpty(i.Name))
                .ToList();

            // Lookup по ID айтема
            var itemLookup = items.ToDictionary(i => i.ItemId, i => i.Name);

            // Записи про кількість айтемів у бригадах
            var brigadeItems = dAPoint.BrigadeItemRepository
                .GetAll()
                .Where(bi => bi != null)
                .ToList();

            // Якщо немає даних, повертаємо порожній словник
            if (!brigadeItems.Any())
                return new Dictionary<string, int>();

            // Групування по назві айтема
            var result = brigadeItems
                .GroupBy(bi => itemLookup.ContainsKey(bi.ItemId) ? itemLookup[bi.ItemId] : "Unknown")
                .ToDictionary(
                    g => g.Key,
                    g => g.Sum(bi => bi.Quantity),
                    StringComparer.OrdinalIgnoreCase
                );

            return result;
        }


    }
}
