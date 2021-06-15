using System;

namespace OAJ.WebService.Models
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string Program { get; set; }
    }
}
