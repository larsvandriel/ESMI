using Microsoft.AspNetCore.Mvc;
using SpecialistManagementSystem.Logic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SpecialistManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialistController : ControllerBase
    {
        public ISpecialistManager SpecialistManager { get; set; }

        public SpecialistController(ISpecialistManager specialistManager)
        {
            SpecialistManager = specialistManager;
        }

        // GET: api/<SpecialistController>
        [HttpGet]
        public IEnumerable<Specialist> Get()
        {
            return SpecialistManager.GetSpecialists();
        }

        // GET api/<SpecialistController>/5
        [HttpGet("{id}")]
        public Specialist Get(Guid id)
        {
            return SpecialistManager.GetSpecialist(id);
        }

        // POST api/<SpecialistController>
        [HttpPost]
        public Specialist Post([FromBody] Specialist specialist)
        {
            return SpecialistManager.CreateSpecialist(specialist);
        }

        // PUT api/<SpecialistController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [FromBody] Specialist specialist)
        {
            SpecialistManager.EditSpecialist(id, specialist);
        }

        // DELETE api/<SpecialistController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            SpecialistManager.DeleteSpecialist(id);
        }
    }
}
