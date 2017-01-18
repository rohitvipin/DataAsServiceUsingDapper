using System.IO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DataAsService.Controllers
{
    [Route("api/[controller]")]
    public class FinanceController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                const string filename = "financial1.xml";
                const string filepath = @"C:\Projects\DataAsService\src\DataAsService\Content";

                var fileBytes = System.IO.File.ReadAllBytes(Path.Combine(filepath, filename));
                return File(fileBytes, "application/xml", filename);
            }
            catch (System.Exception exception)
            {
                BadRequest(exception);
            }
            return NoContent();
        }
    }
}
