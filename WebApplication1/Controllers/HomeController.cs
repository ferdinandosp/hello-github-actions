using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("/home")]
    public class HomeController : Controller
    {
        private sampleContext _context;
        private ILogger<HomeController> _logger;

        public HomeController(sampleContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<String>> Index()
        {
            try
            {
                SampleTable sample = new SampleTable();
                //sample.Number = 1;
                sample.Text = "New";

                await _context.SampleTables.AddAsync(sample);
                await _context.SaveChangesAsync();
                return Json(sample);
            } catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Crew>> CreateCrew(AddCrewSpec spec)
        {
            try
            {
                Crew crew = spec.toCrew();

                await _context.Crews.AddAsync(crew);
                await _context.SaveChangesAsync();
                
                return Json(crew);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
            }

            return Json("Error");            
        }

        [HttpGet("crews")]
        public List<Crew> GetCrew()
        {
            return _context.Crews.ToList();
        }
    }
}
