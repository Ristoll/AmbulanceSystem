using Ambulance.Core.Entities;
using AmbulanceSystem.Core;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ambulance.BLL.Commands.AnaliticsCommands;

//public class CallAnalyticsCommand : AbstrCommandWithDA<Dictionary<DateTime, int>>
//{
//    public override string Name => "Аналітика дзвінків";

//    public CallAnalyticsCommand (IUnitOfWork operateUnitOfWork, IMapper mapper)
//        : base(operateUnitOfWork, mapper)
//    {
//    }

//    public override Dictionary<DateTime, int> Execute()
//{
//    var calls = dAPoint.CallRepository.GetAll().ToList();

//    // 1. Знаходимо понеділок поточного тижня
//    var today = DateTime.Today;
//    int delta = DayOfWeek.Monday - today.DayOfWeek;
//    if (delta > 0) delta -= 7; // якщо сьогодні неділя — сюди зайде
//    var monday = today.AddDays(delta).Date;

//    var sunday = monday.AddDays(6).Date;

//    //// 2. Фільтруємо лише виклики цього тижня
//    //var weekCalls = calls
//    //    .Where(c => c.StartCallTime.HasValue &&
//    //                c.StartCallTime.Value.Date >= monday &&
//    //                c.StartCallTime.Value.Date <= sunday)
//    //    .ToList();

//    //// 3. Групуємо за датою
//    //var grouped = weekCalls
//    //    .GroupBy(c => c.StartCallTime!.Value.Date)
//    //    .ToDictionary(
//    //        g => g.Key,
//    //        g => g.Count()
//    //    );

//    // 4. Створюємо словник **на всі 7 днів**, навіть якщо 0 викликів
//    var result = new Dictionary<DateTime, int>();

//    for (int i = 0; i < 7; i++)
//    {
//        var date = monday.AddDays(i);
//        result[date] = grouped.ContainsKey(date) ? grouped[date] : 0;
//    }

//    return result;
//}
//}
