using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Transportly.Domain.Models;

namespace Transportly.Infrastructure
{
    public class DataStore
    {
        private ILogger logger;
        public Schedule Schedule { get; private set; }
        private HashSet<Day> days = new HashSet<Day>();
        public IQueryable<Day> Days => this.days.AsQueryable();

        private HashSet<Flight> flights = new HashSet<Flight>();
        public IQueryable<Flight> Flights => this.flights.AsQueryable();
        private HashSet<Order> orders = new HashSet<Order>();
        public IQueryable<Order> Orders => this.orders.AsQueryable();

        // this class is used to simulate a database & read values from JSON FILE.
        // for simplicity I only have one schedule
        public DataStore(ILogger<DataStore> logger)
        {
            this.logger = logger;

            this.Schedule = new Schedule(2);

            try
            {
                this.BuildOrdersDataset();
                this.BuildDaysDataSet();
                this.BuildFlightsDataSet();
                this.BuildScheduleModel();
            }
            catch (Exception e)
            {
                this.logger.LogError(e, e.Message);
            }
        }
        private void BuildOrdersDataset()
        {
            var items = JObject.Parse(File.ReadAllText("orders.json"))
                        .Children<JProperty>()
                        .Select(p => (JObject)p.Value)
                        .ToList();

            for (int i = 0; i < items.Count; i++)
            {
                string orderId = i.ToString().PadLeft(3, '0');
                var order = new Order(orderId, items[i].Value<string>("destination"));

                this.orders.Add(order);
            }
        }
        private void BuildScheduleModel()
        {
            this.Schedule.AssignDays(this.days.ToList());

        }
        private void BuildDaysDataSet()
        {
            var retList = new List<Day>();

            for (int i = 0; i < this.Schedule.MaxDays; i++)
            {
                this.days.Add(new Day(i + 1));
            }
        }
        private void BuildFlightsDataSet()
        {
            BuildYYZFlights();
            BuildYYCFlights();
            BuildYVRFlights();
        }
        private void BuildYYZFlights()
        {
            var yyzOrders = this.orders.GroupBy(o => o.ArrivalCity).Where(g => g.Key.Equals("yyz", StringComparison.InvariantCultureIgnoreCase));

            var dayCount = 0;
            foreach (var day in this.days)
            {
                var flightId = day.Flights.Count() + 1;

                var flight = new Flight(flightId, "YUL", "YYZ");

                var flightOrders = yyzOrders.FirstOrDefault()?.Skip(dayCount * flight.MaxOrdersPerFlight)?.Take(flight.MaxOrdersPerFlight)?.ToList();

                if (flightOrders != null)
                {
                    flight.AssignOrders(flightOrders);

                    day.AssignFlight(flight);

                    this.flights.Add(flight);
                }
                dayCount++;
            }


        }
        private void BuildYYCFlights()
        {
            var yycOrders = this.orders.GroupBy(o => o.ArrivalCity).Where(g => g.Key.Equals("yyc", StringComparison.InvariantCultureIgnoreCase));


            var dayCount = 0;
            foreach (var day in this.days)
            {
                var flightId = day.Flights.Count() + 1;

                var flight = new Flight(flightId, "YUL", "YYC");

                var flightOrders = yycOrders.FirstOrDefault()?.Skip(dayCount * flight.MaxOrdersPerFlight)?.Take(flight.MaxOrdersPerFlight)?.ToList();

                if (flightOrders != null)
                {
                    flight.AssignOrders(flightOrders);

                    day.AssignFlight(flight);

                    this.flights.Add(flight);
                }

                dayCount++;
            }
        }
        private void BuildYVRFlights()
        {
            var yvrOrders = this.orders.GroupBy(o => o.ArrivalCity).Where(g => g.Key.Equals("yvr", StringComparison.InvariantCultureIgnoreCase));

            var dayCount = 0;
            foreach (var day in this.days)
            {
                var flightId = day.Flights.Count() + 1;

                var flight = new Flight(flightId, "YUL", "YVR");

                var flightOrders = yvrOrders.FirstOrDefault()?.Skip(dayCount * flight.MaxOrdersPerFlight)?.Take(flight.MaxOrdersPerFlight)?.ToList();

                if (flightOrders != null)
                {
                    flight.AssignOrders(flightOrders);

                    day.AssignFlight(flight);

                    this.flights.Add(flight);
                }

                dayCount++;
            }
        }

    }
}
