using CrudMongo.Models;
using CrudMongo.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudMongo.Controllers
{
   
    [ApiController]
    [Route("api/Employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        //[Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _employeeService.GetAllAsync());
        }
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> Get(string id)
        {
            var emp = await _employeeService.GetByIdAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            return Ok(emp);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }   
            await _employeeService.CreateAsync(emp);
            return Ok(emp.id);
        }
        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Employee empData)
        {
            var emp = await _employeeService.GetByIdAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            await _employeeService.UpdateAsync(id, empData);
            return NoContent();
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var emp = await _employeeService.GetByIdAsync(id);
            if (emp == null)
            {
                return NotFound();
            }
            await _employeeService.DeleteAsync(emp.id);
            return NoContent();
        }
    }
}
