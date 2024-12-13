namespace Task.Models.Entities
{
    public class EmployeeTask
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

        // Foreign key for Employee
        public int EmployeeId { get; set; }
        public  Employee Employee { get; set; }
    }
}
