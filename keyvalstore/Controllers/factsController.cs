using keyvalstore.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace keyvalstore.Controllers
{
    public class factsController : ApiController
    {
        public async Task<fact[]> LoadJson()
        {
            try
            {
                string ip = HttpContext.Current.Request.UserHostAddress;
                string hostname = Dns.GetHostEntry(ip).HostName;

                string path = "~/Hosts/" + hostname + ".json";
                var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(path);
 
                using (StreamReader r = new StreamReader(fullPath))
                {
                    string json = await r.ReadToEndAsync();
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

        public async Task<IEnumerable<fact>> GetAllFacts()
        {
            var facts = await LoadJson();
            return facts;
        }

        public async Task<IHttpActionResult> GetFact(string name)
        {
            var facts = await LoadJson();
            var fact = facts.FirstOrDefault((f) => f.name == name);
            if (fact == null)
            {
                return NotFound();
            }
            return Ok(fact);
        }
    }
}
