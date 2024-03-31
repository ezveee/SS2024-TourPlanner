using Business.Interfaces;
using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Models;

namespace UI.Models
{
    internal class TourModel
    {
        public int TourId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string From { get; set; } = null!;
        public string To { get; set; } = null!;
        public string TransportType { get; set; } = null!;
        public float Distance { get; set; }
        public double EstimatedTime { get; set; }
        public string RouteInformation { get; set; } = null!;

        public List<Tour> Tours { get; set; }

        public TourModel()
        {
            IManager<Tour> manager = new TourManager();

            Tours = manager.GetAll();
        }

        public TourModel(string name, string desc, string from, string to, string transportType, float distance, double time, string info)
        {
            IManager<Tour> manager = new TourManager();

            Tour tour = new Tour()
            {
                Name = name,
                Description = desc,
                From = from,
                To = to,
                TransportType = transportType,
                Distance = distance,
                EstimatedTime = time,
                RouteInformation = info
            };

            manager.Create(tour);

            Tours = manager.GetAll();
        }
    }
}
