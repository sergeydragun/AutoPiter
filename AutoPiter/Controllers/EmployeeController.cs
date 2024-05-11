using AutoPiter.Application.DTO;
using AutoPiter.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoPiter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class EmployeeController(IEmployeeService employeeService) : Controller
    {
        private readonly IEmployeeService _employeeService = employeeService;

        /// <summary>
        /// Получает список всех сотрудников
        /// </summary>
        /// <returns>Список сотрудников</returns>
        /// <response code="200">Успешно вернул список сотрудников</response>
        [HttpGet]
        public async Task<ActionResult<List<EmployeeDTO>>> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }
    }
}
