using keyvalstore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.IO;
using Newtonsoft.Json;

namespace keyvalstore.Controllers
{
    public class factsController : ApiController
    {
        public fact[] LoadJson()
        {
            try
            {
                string ip = HttpContext.Current.Request.UserHostAddress;
                string hostname = Dns.GetHostEntry(ip).HostName;

                string path = "~/Hosts/" + hostname + ".json";
                var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(path);
 
                using (StreamReader r = new StreamReader(fullPath))
                {
                    string json = r.ReadToEnd();
                    List<fact> facts = JsonConvert.DeserializeObject<List<fact>>(json);

                    return facts.ToArray();
                }
            }
            catch
            {
                fact[] facts = new fact[]
                {
                    new fact { name = "role", value = "server"},
                    new fact { name = "location", value = "SPB"}
                };
                return facts;
            }
        }

        public IEnumerable<fact> GetAllFacts()
        {
            var facts = LoadJson();
            return facts;
        }

        public IHttpActionResult GetFact(string name)
        {
            var facts = LoadJson();
            var fact = facts.FirstOrDefault((f) => f.name == name);
            if (fact == null)
            {
                return NotFound();
            }
            return Ok(fact);
        }
    }
}
