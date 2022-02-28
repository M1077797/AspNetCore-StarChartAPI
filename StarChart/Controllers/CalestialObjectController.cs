using Microsoft.AspNetCore.Mvc;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CalestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CalestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }
    }
}
