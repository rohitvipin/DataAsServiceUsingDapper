using System.Threading.Tasks;
using DataAsService.DAL.Models;
using DataAsService.DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DataAsService.Controllers
{
    [Route("api/[controller]")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _departmentRespository;

        public DepartmentController(IDepartmentRepository departmentRespository)
        {
            _departmentRespository = departmentRespository;
        }

        // GET api/Department
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var task = _departmentRespository?.Get();
                if (task != null)
                {
                    return Ok(await task);
                }
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception);
            }
            return NoContent();
        }

        // GET api/Department/5
        /// <summary>
        /// Gets a department by the Id
        /// </summary>
        /// <remarks>
        /// Pass the integer Id of the department
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Gets the Department object</returns>
        /// <response code="200">Returns the Department object</response>
        [ProducesResponseType(typeof(Department), 200)]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var task = _departmentRespository?.GetById(id);
                if (task != null)
                {
                    return Ok(await task);
                }
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception);
            }
            return NoContent();
        }

        // POST api/Department
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Department department)
        {
            try
            {
                var task = _departmentRespository?.Insert(department);
                if (task != null)
                {
                    return Ok(await task);
                }
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception);
            }
            return NoContent();
        }

        // POST api/Department
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody]Department department)
        {
            try
            {
                var task = _departmentRespository?.Update(department);
                if (task != null)
                {
                    return Ok(await task);
                }
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception);
            }
            return NoContent();
        }

        // DELETE api/Department/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var task = _departmentRespository?.Delete(id);
                if (task != null)
                {
                    return Ok(await task);
                }
            }
            catch (System.Exception exception)
            {
                return BadRequest(exception);
            }
            return NoContent();
        }
    }
}
