using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppGym.Models;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace WebAppGym.Mapping
{
    public class NHibernateMapper
    {
        private readonly ModelMapper _modelMapper;

        public NHibernateMapper()
        {
            _modelMapper = new ModelMapper();
        }

        public HbmMapping Map()
        {
            //MapStaff();
            return _modelMapper.CompileMappingForAllExplicitlyAddedEntities();
        }

        private void MapPerson()
        {
            _modelMapper.Class<Staff>(e =>
            {
                //e.Id(p => p.Id);
                //e.Property(p => p.FirstName);
                //e.Property(p => p.LastName);
                //e.Property(p => p.Email);
                //e.Property(p => p.Gender);
                //e.Property(p => p.Age);
            });
        }
    }
}