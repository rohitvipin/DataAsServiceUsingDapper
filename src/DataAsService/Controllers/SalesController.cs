﻿using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAsService.DAL.Repositories.Interfaces;
using DataAsService.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace DataAsService.Controllers
{
    /// <summary>
    /// SalesController
    /// </summary>
    [Route("api/[controller]")]
    public class SalesController : Controller
    {
        private readonly ISalesRepository _salesRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="salesRepository"></param>
        public SalesController(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        // GET: api/values
        /// <summary>
        /// Get all sales records
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var task = _salesRepository?.Get();
                if (task != null)
                {
                    return File(Encoding.ASCII?.GetBytes(XmlSerializeHelper.SerializeToString((await task).ToList())), "application/xml", "SalesDetails.xml");
                }
            }
            catch (System.Exception exception)
            {
                BadRequest(exception);
            }
            return NoContent();
        }
    }
}
