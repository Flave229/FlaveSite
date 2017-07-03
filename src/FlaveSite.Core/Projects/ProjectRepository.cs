using System.IO;
using Npgsql;

namespace FlaveSite.Core.Projects
{
    public class ProjectRepository
    {
        private readonly NpgsqlConnection _connection;

        public ProjectRepository()
        {
            var connectionString = File.ReadAllText("./wwwroot/config/LIVEConnectionString.config");
            _connection = new NpgsqlConnection(connectionString);
        }

        public void GetProgrammingLanguages()
        {
            _connection.Open();

            var command = new NpgsqlCommand("SELECT * FROM ProgrammingLanguages", _connection);
            var reader = command.ExecuteReader();

            var stringThing = "";

            while (reader.Read())
                stringThing += $"{reader[0]}\t {reader[1]}\n";

            _connection.Close();
        }

        public void AddProject(AddProjectRequest request)
        {
            _connection.Open();

            var command = new NpgsqlCommand($"INSERT INTO projects (title, description, date, authorid) VALUES ({request.Title}, {request.Description}, {request.Date}, {request.AuthorId})");
            var reader = command.ExecuteReader();

            var projectId = 0;

            while (reader.Read())
            {
                projectId = reader.GetInt32(0);
            }

            foreach (var teamMemberId in request.TeamMembers)
            {
                command = new NpgsqlCommand($"INSERT INTO projectTeamMembersLinker (projectId, authorId) VALUES ({projectId}, {teamMemberId}");
                command.ExecuteNonQuery();
            }
        }
    }
}
