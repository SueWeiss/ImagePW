using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Data.SqlClient;

namespace Library.Data
{

    public class Images
    {
        public string FileName { get; set; }
        public int TimesViewed { get; set; }
        public int Id { get; set; }
        public string Password { get; set; }
    }
    public class Manager
    {
        string _connectionString { get; set; }
        public Manager(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int AddImage(string fileName, string pw)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = " INSERT into Images(FileName,Password,TimesViewed)" +
                "VALUES(@name,@pw,'1') SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@name", fileName);
            cmd.Parameters.AddWithValue("@pw", pw);
            conn.Open();
            var x= cmd.ExecuteScalar();
            conn.Close();
            conn.Dispose();
            return int.Parse(x.ToString());
        }
        public Images Get(int id)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
          //  List<Images> list = new List<Images>();
            cmd.CommandText = "SELECT * FROM Images where Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            var reader = cmd.ExecuteReader();
            Images i = new Images();
            if (reader.Read())
            {
                i.Id = (int)reader["Id"];
                i.Password = (string)reader["Password"];
                i.FileName = (string)reader["FileName"];                
            }
            return i;
        }
        public void SetTimesViewed(int id)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "UPDATE Images SET TimesViewed=(TimesViewed+1) where Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();

        }
        public int GetTimes(int id)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT TimesViewed FROM Images where Id = @id and TimesViewed IS Not Null";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            var reader = cmd.ExecuteReader();
           if (reader.Read())
            {
                return (int)reader["TimesViewed"];
            }
            return 0; ;
        }
    }
}

