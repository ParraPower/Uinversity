namespace UniversityCore.Models.Config
{
    public class SqlOptions
    {
        public SqlOptions() { ConnectionString = string.Empty; }

        public SqlOptions(string connectionString) { ConnectionString = connectionString; }
       
        public string ConnectionString { get; set; }
    }
}
