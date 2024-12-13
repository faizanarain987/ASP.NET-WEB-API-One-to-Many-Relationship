namespace Task.Models
{
    public class EmployeeDto
    {
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public List<EmployeeTaskDto> EmployeeTaskDtos { get; set; }
    }
}
