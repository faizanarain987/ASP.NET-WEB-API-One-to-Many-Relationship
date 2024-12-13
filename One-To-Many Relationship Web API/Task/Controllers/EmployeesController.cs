using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Task.Data;
using Task.Models;
using Task.Models.Entities;

namespace Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult CreateEmployeeWithTasks(EmployeeDto employeeDto)
        {
            var employee = new Employee
            {
                Name = employeeDto.Name,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                EmployeeTasks = employeeDto.EmployeeTaskDtos.Select(t => new EmployeeTask
                {
                    Title = t.Title,
                    Description = t.Description
                }).ToList()
            };

            _context.Employees.Add(employee);
            _context.SaveChanges();

            return Ok(new {Message = "Employee with tasks created successfully!" });
        }
        [HttpGet]
        [Route("GetEmployeeWithTasksById")]
        public IActionResult GetEmployeeById(int id)
        {
            var employee = _context.Employees
                .Include(e => e.EmployeeTasks)
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound(new { Message = "Employee not found" });

            var employeeDto = new EmployeeDto
            {
                Name = employee.Name,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                EmployeeTaskDtos = employee.EmployeeTasks.Select(t => new EmployeeTaskDto
                {
                    Title = t.Title,
                    Description = t.Description
                }).ToList()
            };

            return Ok(employeeDto);
        }
        [HttpGet]
        [Route("GetAllEmployeesWithTasks")]
        public IActionResult GetAllEmployeesWithTasks()
        {
            var employees = _context.Employees
                .Include(e => e.EmployeeTasks)
                .ToList();
            var employeeDtos = employees.Select(e => new EmployeeDto
            {
                Name = e.Name,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                EmployeeTaskDtos = e.EmployeeTasks.Select(t => new EmployeeTaskDto
                {
                    Title = t.Title,
                    Description = t.Description,
                }).ToList()
            }).ToList();

            return Ok(employeeDtos);
        }

        [HttpPut]
        [Route("UpdateAnEmployeeWithTasksById")]
        public IActionResult UpdateEmployeeWithTasks(int id, EmployeeDto employeeDto)
        {
            var employee =  _context.Employees
                .Include(e => e.EmployeeTasks) 
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound(new { Message = "Employee not found" });

            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;
            employee.PhoneNumber = employeeDto.PhoneNumber;

            _context.EmployeeTasks.RemoveRange(employee.EmployeeTasks); 
            employee.EmployeeTasks = (ICollection<EmployeeTask>)employeeDto.EmployeeTaskDtos.Select(t => new EmployeeTaskDto
            {
                Title = t.Title,
                Description = t.Description
            }).ToList();

            _context.SaveChanges();
            return Ok(new { Message = "Employee and tasks updated successfully!" });
        }

        [HttpDelete]
        [Route("DeleteAnEmployeeWithTasks")]
        public IActionResult DeleteEmployee(int id)
        {
            var employee = _context.Employees
                .Include(e => e.EmployeeTasks) 
                .FirstOrDefault(e => e.Id == id);

            if (employee == null)
                return NotFound(new { Message = "Employee not found" });

            _context.EmployeeTasks.RemoveRange(employee.EmployeeTasks); 
            _context.Employees.Remove(employee);        
             _context.SaveChanges();

            return Ok(new { Message = "Employee and tasks deleted successfully!" });
        }
    }
}
