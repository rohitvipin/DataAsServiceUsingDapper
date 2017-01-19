using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAsService.DAL.Models;
using DataAsService.DAL.Repositories.Interfaces;
using DataAsService.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace DataAsService.Controllers
{
    [Route("api/[controller]")]
    public class FinanceController : Controller
    {
        private readonly IFinanceRepository _financeRepository;

        public FinanceController(IFinanceRepository financeRepository)
        {
            _financeRepository = financeRepository;
        }

        // GET: api/Finance
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var task = _financeRepository?.Get();
                if (task != null)
                {
                    var serializedXml = XmlSerializeHelper.SerializeToString((await task).ToList());
                    var stringBytes = Encoding.ASCII?.GetBytes(serializedXml);
                    return File(stringBytes, "application/xml", "FinanceDetails.xml");
                }
            }
            catch (System.Exception exception)
            {
                BadRequest(exception);
            }
            return NoContent();
        }

        // GET api/Finance/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                var task = _financeRepository?.GetByAcctBalId(id);
                if (task != null)
                {
                    var financeCombined = await task;
                    if (financeCombined == null)
                    {
                        return NotFound($"id {id} not found.");
                    }

                    var serializedXml = XmlSerializeHelper.SerializeToString(financeCombined.ToList());
                    var stringBytes = Encoding.ASCII?.GetBytes(serializedXml);
                    return File(stringBytes, "application/xml", "FinanceDetails.xml");
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
