using AmbulanceSystem.BLL.Models;
using AmbulanceSystem.Core;
using AmbulanceSystem.Core.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ambulance.BLL.Services
{
    public class DispatcherService
    {
        //брати локацію з моделі бригади

        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DispatcherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public List<TimeSpan> GetArrivalTimesForSelectedTeams(List<BrigadeModel> selectedTeams, double callLat, double callLon)
        {
            var etaList = new List<TimeSpan>();

            foreach (var team in selectedTeams)
            {
                if (team.Latitude.HasValue && team.Longitude.HasValue)
                {
                    var eta = CalculateEta(team.Latitude.Value, team.Longitude.Value, callLat, callLon);
                    etaList.Add(eta);
                }
            }

            return etaList.OrderBy(t => t).ToList();
        }

        public List<BrigadeModel> GetBestTeamsByTypeAndEta(int brigadeTypeId, double callLat, double callLon, int numberOfTeamsToAssign = 1)
        {
            var teams = unitOfWork.BrigadeRepository.GetAll();

            var freeTeams = mapper.Map<List<BrigadeModel>>(teams
                .Where(b => b.BrigadeStateId == 1
                         && b.BrigadeTypeId == brigadeTypeId));

            if (!freeTeams.Any())
                return new List<BrigadeModel>();

            foreach (var team in freeTeams)
            {
                if (team.Latitude.HasValue && team.Longitude.HasValue)
                {
                    team.EstimatedArrival = CalculateEta(team.Latitude.Value, team.Longitude.Value, callLat, callLon);
                }
            }

            return freeTeams
                .OrderBy(t => t.EstimatedArrival)
                .Take(numberOfTeamsToAssign)
                .ToList();
        }

        private double ToRadians(double angle) => angle * Math.PI / 180;

        private double DistanceInKm(double lat1, double lon1, double lat2, double lon2)
        {
            double R = 6371;
            double dLat = ToRadians(lat2 - lat1);
            double dLon = ToRadians(lon2 - lon1);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        public TimeSpan CalculateEta(double teamLat, double teamLon, double callLat, double callLon, double avgSpeedKmH = 50)
        {
            double distance = DistanceInKm(teamLat, teamLon, callLat, callLon);
            double hours = distance / avgSpeedKmH;
            return TimeSpan.FromHours(hours);
        }
    }
}
