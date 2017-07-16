using System;

namespace FlaveSite.Core.Projects.Records
{
    public class ProjectRecord
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
    }
}