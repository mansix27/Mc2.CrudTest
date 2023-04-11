using Mc2.CrudTest.Application.Services.Dto;
using Mc2.CrudTest.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Mc2.CrudTest.Domain.Entities;

namespace Mc2.CrudTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ILogger<CustomerController> _logger;
        private readonly ICustomerService _customerService;
        public CustomerController(ILogger<CustomerController> logger, ICustomerService customerService)
        {
            _logger = logger;
            _customerService = customerService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _customerService.GetCustomersAsync());
        }
        [HttpPost("Upsert")]
        public async Task<IActionResult> Upsert(CustomerDto CustomerVm)
        {
            return Ok(await _customerService.UpsertAsync(CustomerVm));
        }


        [HttpGet("DeleteCustomer/{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var resullt = await _customerService.DeleteAsync(id);
            if (resullt.Success)
            {
                return Ok(resullt);
            }
            else
                return BadRequest(resullt);

        }

        [HttpGet("GetSingleCustomer/{id:int}")]
        public async Task<ActionResult<Customer>> GetSingleCustomer(int id)
        {
            return Ok(await _customerService.GetCustomerByIdAsync(id));
        }
    }
}
