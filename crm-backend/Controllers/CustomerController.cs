using AutoMapper;
using crm_backend.Entities;
using crm_backend.Helpers;
using crm_backend.Models;
using crm_backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace crm_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _customerService;
        private IMapper _mapper;
        private HandleHttpError _httpError;

        public CustomerController(ICustomerService customerService, IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
            _httpError = new HandleHttpError();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var customers = _customerService.GetAll();
            return Ok(customers);
        }

        [HttpGet("{id:int}", Name = "GetCustomer")]
        public IActionResult GetById(int id)
        {
            Customer customer;

            try
            {
                customer = _customerService.GetById(id);
            }
            catch (Exception error)
            {
                var dataHttpError = _httpError.ApiResponse(error, "Error al Obtener el Cliente");
                if (dataHttpError.status == 400) return BadRequest(dataHttpError);
                if (dataHttpError.status == 404) return NotFound(dataHttpError);
                return StatusCode(500, dataHttpError);
            }
            return Ok(customer);
        }

        [HttpPost]
        public IActionResult Create(CreateRequest model)
        {
            Customer customer;

            try
            {
                customer = _customerService.Create(model);
            }
            catch (Exception error)
            {
                var dataHttpError = _httpError.ApiResponse(error, "Error al crear el Cliente");

                return (dataHttpError.status == 400) ? BadRequest(dataHttpError) : StatusCode(500, dataHttpError);
            }
            return CreatedAtRoute("GetCustomer", new { id = customer.IdCustomer }, customer);
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id, UpdateRequest model)
        {
            Customer customer;

            try
            {
                customer = _customerService.Update(id, model);
            }
            catch (Exception error)
            {
                var dataHttpError = _httpError.ApiResponse(error, "Error al Actualizar el Cliente");
                if (dataHttpError.status == 400) return BadRequest(dataHttpError);
                if (dataHttpError.status == 404) return NotFound(dataHttpError);
                return StatusCode(500, dataHttpError);
            }
            return Ok(customer);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            Customer customer;

            try
            {
                customer = _customerService.Delete(id);
            }
            catch (Exception error)
            {
                var dataHttpError = _httpError.ApiResponse(error, "Error al Eliminar el Cliente");
                if (dataHttpError.status == 400) return BadRequest(dataHttpError);
                if (dataHttpError.status == 404) return NotFound(dataHttpError);
                return StatusCode(500, dataHttpError);
            }

            return Ok(customer);
        }
    }
}
