using AutoMapper;
using AmbulanceSystem.Core;
using Ambulance.Core.Entities;

namespace Ambulance.BLL.Commands.AnaliticsCommands
{
    public class CallAnalyticsCommand : AbstrCommandWithDA<Dictionary<int, int>>
    {
        public override string Name => "Аналітика дзвінків";

        private readonly AnalyticsPeriod _period;
        private readonly DateTime _startDate;

        public CallAnalyticsCommand(
            IUnitOfWork operateUnitOfWork,
            IMapper mapper,
            AnalyticsPeriod period,
            DateTime startDate)
            : base(operateUnitOfWork, mapper)
        {
            _period = period;
            _startDate = startDate;
        }

        public override Dictionary<int, int> Execute()
        {
            var range = _period switch
            {
                AnalyticsPeriod.Week => GetWeekRange(_startDate),
                AnalyticsPeriod.Month => GetMonthRange(_startDate),
                AnalyticsPeriod.Year => GetYearRange(_startDate),
                _ => throw new ArgumentOutOfRangeException()
            };

            List<Call> calls = dAPoint.CallRepository.GetAll()
                .Where(c =>
                            c.CallAt.Date >= range.from &&
                            c.CallAt.Date <= range.to)
                .ToList();

            return _period switch
            {
                AnalyticsPeriod.Week => GetWeekAnalytics(calls, range.from),
                AnalyticsPeriod.Month => GetMonthAnalytics(calls, range.from),
                AnalyticsPeriod.Year => GetYearAnalytics(calls),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private (DateTime from, DateTime to) GetWeekRange(DateTime date)
        {
            int delta = DayOfWeek.Monday - date.DayOfWeek;
            if (delta > 0) delta -= 7;

            var monday = date.AddDays(delta).Date;
            var sunday = monday.AddDays(6).Date;

            return (monday, sunday);
        }

        private (DateTime from, DateTime to) GetMonthRange(DateTime date)
        {
            var from = new DateTime(date.Year, date.Month, 1);
            var to = from.AddMonths(1).AddDays(-1);

            return (from, to);
        }

        private (DateTime from, DateTime to) GetYearRange(DateTime date)
        {
            var from = new DateTime(date.Year, 1, 1);
            var to = new DateTime(date.Year, 12, 31);

            return (from, to);
        }

        private Dictionary<int, int> GetWeekAnalytics(List<Call> calls, DateTime weekStart)
        {
            var weekEnd = weekStart.AddDays(6);

            var weekCalls = calls
                .Where(c => c.CallAt!.Date >= weekStart &&
                            c.CallAt!.Date <= weekEnd)
                .ToList();

            var grouped = weekCalls
                .GroupBy(c => (int)c.CallAt!.DayOfWeek)
                .ToDictionary(g => g.Key, g => g.Count());

            var result = new Dictionary<int, int>();

            // ключі: 1..7 (Пн..Нд)
            for (int i = 1; i <= 7; i++)
            {
                result[i] = grouped.ContainsKey(i) ? grouped[i] : 0;
            }

            return result;
        }

        private Dictionary<int, int> GetMonthAnalytics(List<Call> calls, DateTime monthStart)
        {
            var monthEnd = monthStart.AddMonths(1).AddDays(-1);

            var monthCalls = calls
                .Where(c => c.CallAt!.Date >= monthStart &&
                            c.CallAt!.Date <= monthEnd)
                .ToList();

            var grouped = monthCalls
                .GroupBy(c => c.CallAt!.Day)
                .ToDictionary(g => g.Key, g => g.Count());

            var result = new Dictionary<int, int>();

            int daysInMonth = DateTime.DaysInMonth(monthStart.Year, monthStart.Month);

            for (int day = 1; day <= daysInMonth; day++)
            {
                result[day] = grouped.ContainsKey(day) ? grouped[day] : 0;
            }

            return result;
        }

        private Dictionary<int, int> GetYearAnalytics(List<Call> calls)
        {
            var year = DateTime.Today.Year;
            var from = new DateTime(year, 1, 1);
            var to = new DateTime(year, 12, 31);

            var yearCalls = calls
                .Where(c => c.CallAt!.Date >= from &&
                            c.CallAt!.Date <= to)
                .ToList();

            var grouped = yearCalls
                .GroupBy(c => c.CallAt!.Month)
                .ToDictionary(g => g.Key, g => g.Count());

            var result = new Dictionary<int, int>();

            for (int month = 1; month <= 12; month++)
            {
                result[month] = grouped.ContainsKey(month) ? grouped[month] : 0;
            }

            return result;
        }
    }
}
