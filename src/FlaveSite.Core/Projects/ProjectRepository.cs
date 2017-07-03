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
            var result = command.ExecuteReader();

            var stringThing = "";

            while (result.Read())
                stringThing += $"{result[0]}\t {result[1]}\n";

            _connection.Close();
        }
    }
}
