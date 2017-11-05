using System;
using System.Collections.Generic;

namespace FlaveSite.Core.Projects.Records
{
    public class ProjectRecord
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        public List<string> Images { get; internal set; }

        public ProjectRecord()
        {
            Images = new List<string>();
        }
    }
}