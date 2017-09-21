using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppGym.Models
{
    public class WorkoutType
    {
        //vanliga properties
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }

        //Samlingar (listor) för many-to-many
        public virtual ICollection<Staff> Staffs { get; set; }

        //Samlingar (listor) för one-to-many
        public virtual ICollection<Workout> Workouts { get; set; }
    }
}