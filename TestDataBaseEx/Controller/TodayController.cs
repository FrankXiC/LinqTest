using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TestDataBaseEx.Model;

namespace TestDataBaseEx.Controller {
    [Route("api/[controller]")]
    [ApiController]
    public class TodayController : ControllerBase {
        private readonly Connection _conn;

        public TodayController(IOptions<Connection> options) {
            _conn = options.Value;
        }

        // GET: api/Today
        [HttpGet]
        public List<Customer> Get() {
            TESTDbContext testDbContext = new TESTDbContext(_conn.Default);
            var customerlist = (from cust in testDbContext.Customers select cust).ToList();

            var returnvisitlist = (from retur in testDbContext.ReturnVisitTasks select retur).ToList();

            var returnlist = (from c in customerlist
                              join r in returnvisitlist on c.Id equals r.CustomerId
                              where (c.CreateTime.ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                                   ||
                                   (Convert.ToDateTime(r.ActualTime).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd"))
                              select new { Customer = c }).Distinct().ToList();
            return returnlist.Select(p => p.Customer).ToList();
        }

        // GET: api/Today/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id) {
            return "value";
        }

        // POST: api/Today
        [HttpPost]
        public void Post([FromBody] string value) {
        }

        // PUT: api/Today/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
