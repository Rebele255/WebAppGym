using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppGym.Models
{
    public class Room
    {
        //vanliga properties
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }

        //Samlingar (listor) för one-to-many??
        public virtual ICollection<Workout> Workouts { get; set; }
    }
}