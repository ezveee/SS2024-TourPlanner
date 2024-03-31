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
        public List<Tour> Tours { get; set; }

        public TourModel()
        {
            IManager<Tour> manager = new TourManager();

            Tours = manager.GetAll();
        }
    }
}
