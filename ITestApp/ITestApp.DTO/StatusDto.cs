using System.Collections.Generic;

namespace ITestApp.DTO
{
    public class StatusDto
    {
        public string Name { get; set; }

        public ICollection<TestDto> Tests { get; set; }
    }
}
