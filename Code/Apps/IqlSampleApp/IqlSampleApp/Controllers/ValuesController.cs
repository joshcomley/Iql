using System.Collections.Generic;
using IqlSampleApp.Data;
using IqlSampleApp.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using Coordinate = NetTopologySuite.Geometries.Coordinate;

namespace IqlSampleApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("~/seed")]
        public IActionResult Seed()
        {
            var db = new ApplicationDbContext(HttpContext.RequestServices);
            var site = new Site();
            site.Location = CreatePoint(51.4466596, - 0.2050156);
            site.Name = "Home";
            db.Sites.Add(site);
            db.SaveChanges();
            return Content("done");
        }
        public static Point CreatePoint(double lat, double lng)
        {
            var coord = new Coordinate(lat, lng);
            var geomFactory = new GeometryFactory(new PrecisionModel(), 4326);
            return (Point)geomFactory.CreatePoint(coord);
        }
    }
}
