using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppGym.Models
{
    public class StaffType
    {
        //vanliga properties
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }

        //Samlingar (listor) för many-to-many
        public virtual ICollection<Staff> Staffs { get; set; }
    }
}