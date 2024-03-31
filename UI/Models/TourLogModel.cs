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
    internal class TourLogModel
    {
        public int LogId { get; set; }
        public int TourId { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; } = null!;
        public int Difficulty { get; set; }
        public float Distance { get; set; }
        public double Time { get; set; }
        public int Rating { get; set; }

        public List<TourLog> TourLogs { get; set; }

        private IManager<TourLog> manager = new LogManager();

        public TourLogModel()
        {
            TourLogs = manager.GetAll();
        }

        public TourLogModel(int id)
        {
            TourLogs = manager.GetLogsByTourId(id);
        }

        public TourLogModel(int tourId, DateTime date, string comment, int difficulty, float distance, double time, int rating)
        {
            TourLog tourLog = new TourLog()
            {
                TourId = 1, //temp
                Date = date,
                Comment = comment,
                Difficulty = difficulty,
                Distance = distance,
                Time = time,
                Rating = rating
            };

            manager.Create(tourLog);
            TourLogs = manager.GetAll();
        }
    }
}
