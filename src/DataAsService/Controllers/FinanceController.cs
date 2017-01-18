using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860
namespace DataAsService.Controllers
{
    [Route("api/[controller]")]
    public class FinanceController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public FinanceController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                const string fileName = "financial1.xml";
                const string folderName = "Content";

                var fileBytes = System.IO.File.ReadAllBytes(Path.Combine(_hostingEnvironment?.ContentRootPath, folderName, fileName));
                return File(fileBytes, "application/xml", fileName);
            }
            catch (System.Exception exception)
            {
                BadRequest(exception);
            }
            return NoContent();
        }
    }
}
