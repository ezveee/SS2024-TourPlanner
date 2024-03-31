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
        public List<TourLog> TourLogs { get; set; }

        public TourLogModel()
        {
            IManager<TourLog> manager = new LogManager();

            TourLogs = manager.GetAll();
        }
    }
}
