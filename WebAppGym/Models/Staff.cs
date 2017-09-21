﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppGym.Models
{
    public class Staff
    {
        //vanliga properties
        public virtual Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string SSN { get; set; }

        //Samlingar (listor) för many-to-many
        public virtual ICollection<StaffType> StaffTypes { get; set; }
        public virtual ICollection<WorkoutType> WorkoutTypes { get; set; }

        //Samlingar (listor) för one-to-many
        public virtual ICollection<Workout> Workouts { get; set; }
    }
}