using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebAppGym.Services;

namespace WebAppGym.Controllers
{
    [RoutePrefix("api")]
    public class MyApiController : ApiController
    {
        //skapa tabeller i db Gym (som måste skapats i ssms INNAN), mha nHibernate och mappning
        [Route("createdb"), HttpGet]
        public void CreateDb()
        {
            var cfg = DbService.Configure();
            var schema = new SchemaExport(cfg);

            schema.Create(false, false);
            schema.Drop(false, true);
            schema.Create(false, true);
        }

        //droppa tabeller i db Gym (som måste skapats i ssms INNAN), mha nHibernate och mappning
        [Route("dropdb"), HttpGet]
        public void DropDb()
        {
            var cfg = DbService.Configure();
            var schema = new SchemaExport(cfg);

            //schema.Create(false, false);
            schema.Drop(false, true);
            //schema.Create(false, true);
        }
    }
}