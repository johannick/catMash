using CatMash.Core;
using CatMash.Data;
using CatMash.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
                             select cat).SingleOrDefault();

                var second = (from cat in context.Cats
                              where cat.id == model.VoteAgainst
                              select cat).SingleOrDefault();

                lock (model.VoteFor)
                {
                    lock (model.VoteAgainst)
                    {
                        var toAdd = second.rank + 400;
                        var toSub = first.rank - 400;

                        first.rank = first.rank + toAdd;
                        second.rank = second.rank + toSub;

                        ++first.votes;
                        ++second.votes;
                        UpdateDatabase(model.VoteFor, first.rank, first.votes);
                        UpdateDatabase(model.VoteAgainst, second.rank, second.votes);
                        context.SubmitChanges();
                    }
                }
            }
        }

        private void UpdateDatabase(string voteFor, int rank, int votes)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["modelConnectionString"].ConnectionString;

            using (var connection = new OpenConnection(connectionString))
            using (var command = connection.Connection.CreateCommand())
            {
                command.CommandText = "UPDATE Cat SET votes=@votes, rank=@rank WHERE id=@id";
                command.Parameters.AddWithValue("@votes", votes);
                command.Parameters.AddWithValue("@rank", rank);
                command.Parameters.AddWithValue("@id", voteFor);
                command.ExecuteNonQuery();
            }
        }
    }
}
