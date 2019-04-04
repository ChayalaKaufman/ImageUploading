using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageUploading.Data
{
    public class ImageManager
    {
        private string _connectionString;

        public ImageManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int UploadImage(Image image)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"INSERT INTO Images (FileName,Password, Views)
                        VALUES (@fileName, @password,@views) SELECT SCOPE_IDENTITY()";
            cmd.Parameters.AddWithValue("@fileName", image.FileName);
            cmd.Parameters.AddWithValue("@password", image.Password);
            cmd.Parameters.AddWithValue("@views", 0);
            conn.Open();
            return (int)(decimal)cmd.ExecuteScalar();

        }

        public Image GetImage(int id)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            var cmd = conn.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Images WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            conn.Open();
            var reader = cmd.ExecuteReader();
            reader.Read();
            int num = (int)reader["Views"] + 1;
            Image image = new Image
            {
                Id = id,
                Password = (string)reader["Password"],
                FileName = (string)reader["FileName"],
                Views = num
            };
            reader.Close();
            
            cmd.Parameters.Clear();
            cmd.CommandText = @"UPDATE Images SET Views = @num WHERE Id = @id";
            cmd.Parameters.AddWithValue("@num", num);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.ExecuteNonQuery();
            return image;
        }
    }
}
