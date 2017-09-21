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
            MapStaff();
            MapStaffType();
            MapWorkout();
            MapWorkoutType();
            MapRoom();
            return _modelMapper.CompileMappingForAllExplicitlyAddedEntities();
        }

        private void MapStaff()
        {
            _modelMapper.Class<Staff>(e =>
            {
                e.Id(p => p.Id, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.FirstName);
                e.Property(p => p.LastName);
                e.Property(p => p.SSN);

                //Many-to-Many: Staff - StaffType, dennes lista med StaffType heter StaffTypes
                e.Set(x => x.StaffTypes, collectionMapping =>
                {
                    collectionMapping.Table("Staff_StaffType"); //namn på mellanliggande tabell
                    collectionMapping.Inverse(true); //får bara finnas på ena!! 
                    collectionMapping.Cascade(Cascade.None); // Sätt alltid "Cascade.None" vid en många-till-många-relation
                    collectionMapping.Key(keyMap => keyMap.Column("StaffId")); // kopplingstabell (mellanliggande)
                }, map => map.ManyToMany(p => p.Column("StaffTypeId")));

                //Many-to-Many: Staff - WorkoutType, dennes lista med WorkoutType heter WorkoutTypes
                e.Set(x => x.WorkoutTypes, collectionMapping =>
                {
                    collectionMapping.Table("Staff_WorkoutType"); //namn på mellanliggande tabell
                    collectionMapping.Inverse(true); //får bara finnas på ena!! 
                    collectionMapping.Cascade(Cascade.None); // Sätt alltid "Cascade.None" vid en många-till-många-relation
                    collectionMapping.Key(keyMap => keyMap.Column("StaffId")); // kopplingstabell (mellanliggande)
                }, map => map.ManyToMany(p => p.Column("WorkoutTypeId")));

                // En Staff kan ansvara för flera Workout
                // "Set" används på "en-sidan" av en "en-till-många-relation".
                e.Set(p => p.Workouts, p =>
                {
                    p.Inverse(true); // skall finnas med på one to many-sidan, ej den andra. Detta påverkar vilken sqlkod som genereras
                    p.Cascade(Cascade.All); // hur barnen hanteras då förälder tas bort
                    p.Key(k => k.Column(col => col.Name("Staff"))); // id-namnet hos barnet  som referar till föräldern
                }, p => p.OneToMany());

            });

        }

        private void MapStaffType()
        {
            _modelMapper.Class<StaffType>(e =>
            {
                e.Id(p => p.Id, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.Title);

                //Many-to-Many: StaffType-Staff, dennes lista med Staff heter Staffs
                e.Set(x => x.Staffs, collectionMapping =>
                {
                    collectionMapping.Table("Staff_StaffType"); //namn på mellanliggande tabell
                    //collectionMapping.Inverse(true); //får bara finnas på ena!! 
                    collectionMapping.Cascade(Cascade.None); // Sätt alltid "Cascade.None" vid en många-till-många-relation
                    collectionMapping.Key(keyMap => keyMap.Column("StaffTypeId")); // kopplingstabell (mellanliggande)
                }, map => map.ManyToMany(p => p.Column("StaffId")));
            });
        }

        private void MapWorkoutType()
        {
            _modelMapper.Class<WorkoutType>(e =>
            {
                e.Id(p => p.Id, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.Name);

                //Many-to-Many: WorkoutType - Staff, dennes lista med Staff heter Staffs
                e.Set(x => x.Staffs, collectionMapping =>
                {
                    collectionMapping.Table("Staff_WorkoutType"); //namn på mellanliggande tabell
                    //collectionMapping.Inverse(true); //får bara finnas på ena!! 
                    collectionMapping.Cascade(Cascade.None); // Sätt alltid "Cascade.None" vid en många-till-många-relation
                    collectionMapping.Key(keyMap => keyMap.Column("WorkoutTypeId")); // kopplingstabell (mellanliggande)
                }, map => map.ManyToMany(p => p.Column("StaffId")));

                // En WorkoutType kan finnas i flera Workout
                // "Set" används på "en-sidan" av en "en-till-många-relation".
                e.Set(p => p.Workouts, p =>
                {
                    p.Inverse(true); // skall finnas med på one to many-sidan, ej den andra. Detta påverkar vilken sqlkod som genereras
                    p.Cascade(Cascade.All); // hur barnen hanteras då förälder tas bort
                    p.Key(k => k.Column(col => col.Name("WorkoutType"))); 
                }, p => p.OneToMany());
            });
        }

        private void MapWorkout()
        {
            _modelMapper.Class<Workout>(e =>
            {
                e.Id(p => p.Id, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.TimeStart);
                e.Property(p => p.TimeEnd);


                // Många pass kan finnas i samma sal 
                e.ManyToOne(p => p.Room, mapper =>
                {
                    //mapper.Column("Room"); // Bestämmer vad kolmnen heter i db - ej nödvändig
                    mapper.Cascade(Cascade.None); // None vid ManyToOne annars tas förälder bort när ett barn tas bort
                });

                // Många Workout kan finnas av samma workoutType
                e.ManyToOne(p => p.WorkoutType, mapper =>
                {
                    mapper.Cascade(Cascade.None); // None vid ManyToOne annars tas förälder bort när ett barn tas bort
                });

                //Många Workout kan hållas av en person
                e.ManyToOne(p => p.Staff, mapper =>
                {
                    mapper.Cascade(Cascade.None);
                });

            });
        }

        private void MapRoom()
        {
            _modelMapper.Class<Room>(e =>
            {
                e.Id(p => p.Id, p => p.Generator(Generators.GuidComb));
                e.Property(p => p.Name);

                // I ett rum kan flera pass hållas
                // "Set" används på "en-sidan" av en "en-till-många-relation".
                e.Set(p => p.Workouts, p =>
                {
                    p.Inverse(true); // skall finnas med på one to many-sidan, ej den andra. Detta påverkar vilken sqlkod som genereras
                    p.Cascade(Cascade.All); // hur barnen hanteras då förälder tas bort
                    p.Key(k => k.Column(col => col.Name("Room"))); // id-namnet hos barnet  som referar till föräldern
                }, p => p.OneToMany());
            });
        }
    }
}