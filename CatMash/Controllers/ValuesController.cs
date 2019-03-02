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

        // GET api/values/id
        public Cat Get(string id)
        {
            using (var context = new CatMashDataContext())
            {
                var cats = from cat in context.Cats
                           where cat.id == id
                           select cat;

                return cats.FirstOrDefault();
            }
        }

        // POST api/values/id
        public void Post(string id, [FromBody] string against)
        {
            using (var context = new CatMashDataContext())
            {
                var first = (from cat in context.Cats
                             where cat.id == id
                             select cat).First();

                var second = (from cat in context.Cats
                              where cat.id == against
                              select cat).First();

                var toAdd = second.rank + 400;
                var toSub = first.rank - 400;

                first.rank += toAdd;
                second.rank += toSub;

                ++first.votes;
                ++second.votes;
                context.SubmitChanges();
            }
        }
        
    }
}
