using System;
using System.Collections.Generic;

namespace Transportly.Domain.Models
{
    public class Schedule : Entity
    {
        private List<Day> days = new List<Day>();
        public IReadOnlyCollection<Day> Days => this.days;

        public int MaxDays { get; init; }

        public Schedule(int id, int? maxDays = 2)
        {
            this.ID = id;
            this.MaxDays = (int)maxDays;
        }

        private void ValidateDays(List<Day> days)
        {
            if (days == null || days.Count <= 0)
            {
                throw new Exception("Invalid days list");
            }
            else if (this.days.Count > this.MaxDays)
            {
                throw new Exception($"Only {this.MaxDays} can be assigned to a Schedule");
            }
        }
        public void AssignDays(List<Day> days)
        {
            ValidateDays(days);

            days.ForEach(d => d.AssignSchedule(this.ID));

            this.days.AddRange(days);
        }
    }
}
