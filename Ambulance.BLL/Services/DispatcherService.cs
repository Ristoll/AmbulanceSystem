using AmbulanceSystem.Core;
using AmbulanceSystem.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Ambulance.BLL.Services
{
    public class DispatcherService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly HttpClient httpClient;

        public DispatcherService(IUnitOfWork unitOfWork, IMapper mapper, HttpClient httpClient)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.httpClient = httpClient;
        }

        // Геокодування адреси хворого
        public async Task<(double Lat, double Lon)> GeocodeAddressAsync(string address)
        {
            string url = $"https://nominatim.openstreetmap.org/search?format=json&q={Uri.EscapeDataString(address)}";
            var results = await httpClient.GetFromJsonAsync<List<NominatimResult>>(url);

            if (results != null && results.Count > 0)
            {
                return (double.Parse(results[0].Lat), double.Parse(results[0].Lon));
            }

            throw new Exception("Адреса не знайдена");
        }

        // Розрахунок маршруту через OSRM
        public async Task<(double DistanceKm, TimeSpan Duration)> CalculateRouteAsync(double fromLat, double fromLon, double toLat, double toLon)
        {
            string url = $"https://router.project-osrm.org/route/v1/driving/{fromLon},{fromLat};{toLon},{toLat}?overview=false";
            var response = await httpClient.GetFromJsonAsync<OSRMResponse>(url);

            if (response != null && response.Routes != null && response.Routes.Any())
            {
                var route = response.Routes.First();
                return (route.Distance / 1000, TimeSpan.FromSeconds(route.Duration));
            }

            throw new Exception("Не вдалося розрахувати маршрут");
        }

        // Вибір кращих бригад за типом та ETA
        public async Task<List<BrigadeDto>> GetBestTeamsByTypeAndEtaAsync(int brigadeTypeId, double callLat, double callLon, int numberOfTeamsToAssign = 1)
        {
            var teams = unitOfWork.BrigadeRepository.GetAll();
            var freeTeams = mapper.Map<List<BrigadeDto>>(teams
                .Where(b => b.BrigadeTypeId == brigadeTypeId));

            var tasks = freeTeams.Select(async team =>
            {
                if (team.Latitude.HasValue && team.Longitude.HasValue)
                {
                    var eta = await CalculateRouteAsync(team.Latitude.Value, team.Longitude.Value, callLat, callLon);
                    team.EstimatedArrival = eta.Duration;
                    team.EstimatedDistanceKm = eta.DistanceKm;
                }
            }).ToList();

            await Task.WhenAll(tasks);

            return freeTeams
                .OrderBy(t => t.EstimatedArrival)
                .Take(numberOfTeamsToAssign)
                .ToList();
        }
    }

    // DTO для десеріалізації Nominatim
    public class NominatimResult
    {
        public string Lat { get; set; } = string.Empty;
        public string Lon { get; set; } = string.Empty;
    }

    // DTO для десеріалізації OSRM
    public class OSRMResponse
    {
        public List<OSRMRoute> Routes { get; set; } = new();
    }

    public class OSRMRoute
    {
        public double Distance { get; set; }
        public double Duration { get; set; }
    }
}
