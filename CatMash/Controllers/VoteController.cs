using CatMash.Data;
using CatMash.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace CatMash.Controllers
{
    public class VoteController : ApiController
    {
        // GET: api/Vote
        public IEnumerable<Cat> Get()
        {
            using (var context = new CatMashDataContext())
            {
                var cats = from cat in context.Cats select cat;
                return cats.ToList().OrderByDescending(c => c.rank / (c.votes + 1));
            }
        }

        // POST: api/Vote
        public void Post(HttpRequestMessage message)
        {
            var result = message.Content.ReadAsStringAsync().Result;
            var model = JsonConvert.DeserializeObject<VoteModel>(result);

            using (var context = new CatMashDataContext())
            {
                var first = (from cat in context.Cats
                             where cat.id == model.VoteFor
                             select cat).First();

                var second = (from cat in context.Cats
                              where cat.id == model.VoteAgainst
                              select cat).First();

                lock (first.id)
                lock (second.id)
                {
                    var toAdd = second.rank + 400;
                    var toSub = first.rank - 400;

                    first.rank = first.rank + toAdd;
                    second.rank = second.rank + toSub;

                    first.votes = first.votes + 1;
                    ++second.votes;
                    context.SubmitChanges();
                }
            }
        }

    }
}
