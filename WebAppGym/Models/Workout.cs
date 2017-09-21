using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppGym.Models
{
    public class Workout
    {
        public virtual Guid Id { get; set; }
        public virtual WorkoutType WorkoutType { get; set; }
        public virtual TimeSpan TimeStart { get; set; }
        public virtual TimeSpan TimeEnd { get; set; }
        public virtual Staff Staff { get; set; }
        public virtual Room Room { get; set; }
    }
}