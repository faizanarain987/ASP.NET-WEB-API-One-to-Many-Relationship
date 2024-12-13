namespace Task.Models.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }

        public ICollection<EmployeeTask> EmployeeTasks { get; set; }
    }
}
