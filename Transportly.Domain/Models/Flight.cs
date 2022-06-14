using System;
using System.Collections.Generic;
using System.Linq;

namespace Transportly.Domain.Models
{
    public class Flight : Entity
    {
        public int? Day { get; private set; }
        public string DepartureCity { get; init; }
        public string ArrivalCity { get; init; }
        private List<Order> orders = new List<Order>();
        public IReadOnlyCollection<Order> Orders => this.orders;
        public int MaxOrdersPerFlight { get; init; }

        public Flight(int id, string departureCity, string arrivalCity, int? maxOrdersPerFlight = 20)
        {
            this.ID = id;
            this.DepartureCity = departureCity ?? throw new ArgumentNullException(nameof(departureCity));
            this.ArrivalCity = arrivalCity ?? throw new ArgumentNullException(nameof(arrivalCity));
            this.MaxOrdersPerFlight = (int)maxOrdersPerFlight;
        }

        public override string ToString()
        {
            return $"Flight:{this.ID}, departure:{this.DepartureCity}, arrival:{this.ArrivalCity}, day:{this.Day}";
        }

        private void ValidateOrders(List<Order> orders)
        {
            //TODO: Add more valiations as needed
            if (orders == null || orders.Count <= 0)
            {
                throw new Exception("Invalid order list");
            }
            if (orders.Count > this.MaxOrdersPerFlight)
            {
                throw new Exception("Only 20 orders can be assigned to a flight");
            }

            ValidateOrderDestinations(orders);
        }
        private void ValidateOrderDestinations(List<Order> orders)
        {
            if (orders.Any(o => !o.ArrivalCity.Equals(this.ArrivalCity, StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new Exception($"Order destinations do not match flight destination: {this.ArrivalCity}");
            }
        }
        public void AssignOrders(List<Order> orders)
        {
            ValidateOrders(orders);

            orders.ForEach(o =>
            {
                o.AssignFlight(this.ID);
                o.AssignDepartureCity(this.DepartureCity);
            });

            this.orders.AddRange(orders);
        }
        public void AssignDay(int day)
        {
            this.Day = day;
            this.orders.ForEach(o => o.AssignDay(day));
        }
    }
}
