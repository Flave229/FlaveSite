using System;
using System.Collections.Generic;
using FlaveSite.Core.Projects.Records;

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

        public List<ProjectRecord> GetProjects()
        {
            return _repository.GetProjects();
        }

        public ProjectRecord GetProjectDetails(int projectId)
        {
            return _repository.GetProjectDetails(projectId);
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
