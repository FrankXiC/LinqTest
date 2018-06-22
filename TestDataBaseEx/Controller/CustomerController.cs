using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TestDataBaseEx.Model;

namespace TestDataBaseEx.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly Connection _conn;

        public CustomerController(IOptions<Connection> options)
        {
            _conn = options.Value;
        }
        // GET: api/Customer
        [HttpGet]
        public List<Customer> Get()
        {
            TESTDbContext testDbContext=new TESTDbContext(_conn.Default);
            var customerlist = (from cust in testDbContext.Customers select cust).ToList();

            var returnvisitlist = (from retur in testDbContext.ReturnVisitTasks where retur.ActualTime == null select retur).ToList();

            var visitList = (from visit in testDbContext.VisitRecords where visit.VisitTime != null select visit).ToList();

            var returnlist = (from v in visitList
                              join r in returnvisitlist on v.CustomerId equals r.CustomerId
                              select new { r.CustomerId }).Distinct().ToList();

            var returnCustomer = (from c in customerlist
                                  join custId in returnlist on c.Id equals custId.CustomerId
                                  select new { Customer = c }).Distinct();
            return returnCustomer.Select(p => p.Customer).ToList();
        }

        [HttpGet("visitRecord")]
        public IActionResult GetVisitRecord() {
            TESTDbContext testDbContext = new TESTDbContext(_conn.Default);

            var customerList = (from ctlist in testDbContext.Customers select ctlist).ToList();

            var returnVistList = (from relist in testDbContext.ReturnVisitTasks
                                  group relist by relist.CustomerId into g
                                  let maxActual = g.Max(p => p.ActualTime)
                                  select new {
                                      g.Key,
                                      MaxActualTime = maxActual,
                                      intent = g.FirstOrDefault(p => p.ActualTime == maxActual).Intent
                                  }).ToList();

            var visitList = (from v in testDbContext.VisitRecords
                             group v by v.CustomerId into g
                             let maxVisit = g.Max(p => p.VisitTime)
                             select new {
                                 g.Key,
                                 maxVisitTime = maxVisit,
                                 intent = g.FirstOrDefault(p => p.VisitTime == maxVisit).Intent
                             }).ToList();

            var ctReturnList = (from c in customerList
                                join r in returnVistList on c.Id equals r.Key into cr
                                from r in cr.DefaultIfEmpty()
                                join vl in visitList on c.Id equals vl.Key into cvl
                                from vl in cvl.DefaultIfEmpty()
                                select new {
                                    CustomerName = c.CustomerName,
                                    Returnintent = r?.intent,
                                    MaxActualTime = r?.MaxActualTime,
                                    VisitIntent = vl?.intent,
                                    MaxVisitTime = vl?.maxVisitTime
                                }).ToList();

            return Ok(ctReturnList);
        }

        //// GET: api/Customer/5
        //[HttpGet("{id}", Name = "Get")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST: api/Customer
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT: api/Customer/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
