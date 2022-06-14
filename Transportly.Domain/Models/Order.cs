using System;

namespace Transportly.Domain.Models
{
    public class Order
    {
        public string Identifier { get; init; }
        public string ArrivalCity { get; init; }
        public string DepartureCity { get; private set; }
        public int? FlightId { get; private set; }
        public int? Day { get; private set; }

        public Order(string id, string arrivalCity)
        {
            this.Identifier = id;
            this.ArrivalCity = arrivalCity ?? throw new ArgumentNullException(nameof(arrivalCity));
        }

        public override string ToString()
        {
            if (!this.FlightId.HasValue)
            {
                return $"Order: order-{this.Identifier}, flight number: Not Scheduled";
            }
            return $"Order: order-{this.Identifier}, flight number:{this.FlightId}, departure:{this.DepartureCity}, arrival:{this.ArrivalCity}, day:{this.Day}";
        }

        public void AssignFlight(int flightId)
        {
            //TODO: add validation as needed
            this.FlightId = flightId;
        }

        public void AssignDay(int day)
        {
            //TODO: add validation as needed
            this.Day = day;
        }
        public void AssignDepartureCity(string departueCity)
        {
            //TODO: add validation as needed
            this.DepartureCity = departueCity;
        }
    }
}
