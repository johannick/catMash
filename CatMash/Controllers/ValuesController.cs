using CatMash.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CatMash.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<Cat> Get()
        {
            using (var context = new CatMashDataContext())
            {
                var random = new Random();
                var cats = from cat in context.Cats
                           select cat;
                return cats.ToList().OrderBy(c => random.NextDouble());
            }
        }
    }
}
