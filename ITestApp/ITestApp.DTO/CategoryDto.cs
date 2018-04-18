using System.Collections.Generic;

namespace ITestApp.DTO
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<TestDto> Tests { get; set; }
    }
}
