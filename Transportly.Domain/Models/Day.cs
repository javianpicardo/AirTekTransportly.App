using System;
using System.Collections.Generic;
using System.Linq;

namespace Transportly.Domain.Models
{
    public class Day : Entity
    {
        public int? ScheduleId { get; private set; }

        private List<Flight> flights = new List<Flight>();
        public IReadOnlyCollection<Flight> Flights => this.flights;
        public int MaxPlanes { get; init; }

        public Day(int id, int? maxPlanes = 3)
        {
            this.ID = id;
            this.MaxPlanes = (int)maxPlanes;
        }

        private void Validateflight(Flight flight)
        {
            if (flight == null)
            {
                throw new Exception("Invalid flight");
            }
            else if (this.flights.Count() == this.MaxPlanes)
            {
                throw new Exception($"Max avaliable planes {this.MaxPlanes} reached cannot assign flight");
            }

            if (this.flights.GroupBy(f => f.ArrivalCity).Any(g => g.Key.Equals(flight.ArrivalCity)))
            {
                //This will respect the use case as I understand it with the examples provided however
                //nothing explicitly states I cannot have two planes going to the same location
                throw new Exception("Cannot assign flights to the same destination in the same day");
            }
        }
        public void AssignFlight(Flight flight)
        {
            Validateflight(flight);
            flight.AssignDay(this.ID);

            this.flights.Add(flight);
        }

        public void AssignSchedule(int scheduleId)
        {
            this.ScheduleId = scheduleId;
        }
    }
}
