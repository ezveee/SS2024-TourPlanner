using Business.Interfaces;
using Business;
using Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Models
{
    internal class testmodel
    {
        public List<Tour> tours { get; set; }

        public string output { get; set; }

        public testmodel()
        {
            IManager<TourLog> manager = new LogManager();

            List<TourLog> tours = manager.GetAll();

            foreach(TourLog tour in tours)
            {
                output += tour.Comment;
            }
        }
    }
}
