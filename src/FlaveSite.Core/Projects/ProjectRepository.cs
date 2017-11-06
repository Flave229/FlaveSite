using System.Collections.Generic;
using System.IO;
using FlaveSite.Core.Projects.Records;
using Npgsql;
using NpgsqlTypes;
using System;

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

            var command = new NpgsqlCommand($"INSERT INTO projects (title, description, date, authorid) VALUES ('{request.Title}', '{request.Description}', @date, 1) RETURNING id", _connection);
            command.Parameters.AddWithValue("@date", NpgsqlDbType.Date, request.Date);
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

        public List<ProjectRecord> GetProjects()
        {
            _connection.Open();

            try
            {
                const string query = @"SELECT 
                                        Projects.id,
                                        Projects.title,
                                        Projects.description,
                                        Projects.date,
                                        Authors.firstname,
                                        Authors.lastname,
                                        Media.url
                                    FROM Projects
                                    LEFT JOIN Authors ON Authors.id = Projects.authorId
                                    LEFT JOIN Media ON Media.projectid = Projects.id
                                        AND Media.isprimary = true
                                    ORDER BY Projects.date DESC";
                var command = new NpgsqlCommand(query, _connection);

                var reader = command.ExecuteReader();

                var projects = new List<ProjectRecord>();

                while (reader.Read())
                {
                    var imageUrl = reader[6] == DBNull.Value ? "" : reader.GetString(6);

                    var project = new ProjectRecord
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Description = reader.GetString(2),
                        Date = reader.GetDateTime(3),
                        Author = reader.GetString(4) + " " + reader.GetString(5),
                        ImageUrl = imageUrl
                    };

                    projects.Add(project);
                }

                reader.Close();
                return projects;
            }
            finally
            {
                _connection.Close();
            }
        }

        public ProjectRecord GetProjectDetails(int projectId)
        {
            _connection.Open();

            try
            {
                var query = $@"SELECT 
                                Projects.id,
                                Projects.title,
                                Projects.description,
                                Projects.date,
                                Authors.firstname,
                                Authors.lastname,
                                Media.url,
                                Media.isprimary,
                                Media.mediatypeid,
                                Authors.id
                            FROM Projects
                            LEFT JOIN Authors ON Authors.id = Projects.authorId
                            LEFT JOIN Media ON Media.projectid = Projects.id
                            WHERE Projects.id = {projectId}";
                var command = new NpgsqlCommand(query, _connection);

                var reader = command.ExecuteReader();

                ProjectRecord project = null;

                while (reader.Read())
                {
                    var mediaUrl = reader[6] == DBNull.Value ? "" : reader.GetString(6);

                    if (project == null)
                    {
                        project = new ProjectRecord
                        {
                            Id = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Description = reader.GetString(2),
                            Date = reader.GetDateTime(3),
                            AuthorId = reader.GetInt32(9),
                            Author = reader.GetString(4) + " " + reader.GetString(5)
                        };
                    }

                    var isprimary = reader[7] != DBNull.Value && reader.GetBoolean(7);
                    var mediaType = reader[8] != DBNull.Value ? reader.GetInt32(8) : 0;

                    if (isprimary && mediaType == 1)
                        project.ImageUrl = mediaUrl;
                    else switch (mediaType)
                    {
                        case 1:
                            project.Images.Add(mediaUrl);
                            break;
                        case 2:
                            project.VideoUrl = mediaUrl;
                            break;
                        case 3:
                            project.Files.Add(mediaUrl);
                            break;
                    }
                }

                reader.Close();

                query = $@"SELECT 
                                Authors.firstname,
                                Authors.lastname,
                                Authors.url
                            FROM ProjectTeamMembersLinker
                            LEFT JOIN Authors ON Authors.id = ProjectTeamMembersLinker.authorId
                            WHERE ProjectTeamMembersLinker.projectId = {projectId}";
                command = new NpgsqlCommand(query, _connection);
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    project?.AdditionalMembers.Add(new Author
                    {
                        Name = reader.GetString(0) + " " + reader.GetString(1),
                        Url = reader.GetString(2)
                    });
                }

                return project;
            }
            finally
            {
                _connection.Close();
            }
        }
    }
}