using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StarChart.Data;
using StarChart.Models;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var celestialObject = _context.CelestialObjects.Find(id);

            if (celestialObject == null)
            {
                return NotFound();
            }

            celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == id).ToList();

            return Ok(celestialObject);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var celestialObjects = _context.CelestialObjects.Where(d => d.Name == name).ToList();

            if (!celestialObjects.Any())
            {
                return NotFound();
            }

            foreach (var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == celestialObject.Id).ToList();
            }

            return Ok(celestialObjects);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var celestialObjects = _context.CelestialObjects.ToList();

            foreach (var celestialObject in celestialObjects)
            {
                celestialObject.Satellites = _context.CelestialObjects.Where(e => e.OrbitedObjectId == celestialObject.Id).ToList();
            }

            return Ok(celestialObjects);
        }

        [HttpPost]
        public IActionResult Create([FromBody] CelestialObject celestialObject)
        {
            _context.Add<CelestialObject>(celestialObject);
            _context.SaveChanges();

            return CreatedAtRoute("GetById", new { id = celestialObject.Id }, celestialObject);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id,[FromBody] CelestialObject celestialObject)
        {
            var findCelestialObject = _context.CelestialObjects.Find(id);

            if (findCelestialObject == null)
            {
                return NotFound();
            }

            findCelestialObject.Name = celestialObject.Name;
            findCelestialObject.OrbitalPeriod = celestialObject.OrbitalPeriod;
            findCelestialObject.OrbitedObjectId = celestialObject.OrbitedObjectId;

            _context.Update<CelestialObject>(findCelestialObject);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}/{name}")]
        public IActionResult RenameObject(int id, string name)
        {
            var findCelestialObject = _context.CelestialObjects.Find(id);

            if (findCelestialObject == null)
            {
                return NotFound();
            }

            findCelestialObject.Name = name;

            _context.Update<CelestialObject>(findCelestialObject);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var findCelestialObject = _context.CelestialObjects.Where(e => e.Id == id || e.OrbitedObjectId == id).ToList();

            if (!findCelestialObject.Any())
            {
                return NotFound();
            }

            _context.RemoveRange(findCelestialObject);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
