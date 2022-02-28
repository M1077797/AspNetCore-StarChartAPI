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
            List<CelestialObject> celestialObject = new List<CelestialObject>();

            if(!celestialObject.Any(r => r.Id == id))
            {
                return NotFound();
            }

            return Ok(celestialObject.Where(r => r.Id == id).First());
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            CelestialObject celestialObject = new CelestialObject();

            if (celestialObject == null)
            {
                return NotFound();
            }

            return Ok(celestialObject);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<CelestialObject> celestialObject = new List<CelestialObject>();
            return Ok(celestialObject);
        }
    }
}
