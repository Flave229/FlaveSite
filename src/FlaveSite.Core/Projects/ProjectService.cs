using System;
using System.Collections;
using System.Collections.Generic;

namespace FlaveSite.Core.Projects
{
    public class ProjectService
    {
        private readonly ProjectRepository _repository;

        public ProjectService(ProjectRepository repository)
        {
            _repository = repository;
        }

        public void AddProject(AddProjectRequest request)
        {
            _repository.AddProject(request);
        }
    }

    public class AddProjectRequest
    {
        public AddProjectRequest()
        {
            TeamMembers = new List<int>();
        }

        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int AuthorId { get; set; }
        public List<int> TeamMembers { get; set; }
    }
}
