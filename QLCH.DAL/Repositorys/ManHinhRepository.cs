using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class ManHinhRepository
    {
        private readonly string _connectionString = GlobalVariables.ConnectionString;

        private void ExecuteNonQuery(string query, Action<SqlCommand> paramMap)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new SqlCommand(query, conn, trans))
                        {
                            paramMap(cmd);
                            cmd.ExecuteNonQuery();
                            trans.Commit();
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception($"Lỗi SQL: {ex.Message}");
                    }
                }
            }
        }

        private List<ManHinh> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<ManHinh>();
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    paramMap?.Invoke(cmd);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ManHinh
                            {
                                MaManHinh = Convert.ToInt32(reader["MaManHinh"]),
                                TenManHinh = reader["TenManHinh"].ToString(),
                                MoTa = reader["MoTa"]?.ToString(),
                                URL = reader["URL"].ToString()
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<ManHinh> GetAll() => ExecuteQuery("SELECT * FROM ManHinh");

        public ManHinh GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM ManHinh WHERE MaManHinh = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(ManHinh m)
        {
            ExecuteNonQuery(@"INSERT INTO ManHinh (TenManHinh, MoTa, URL) 
                              VALUES (@TenManHinh, @MoTa, @URL)", cmd =>
            {
                cmd.Parameters.AddWithValue("@TenManHinh", m.TenManHinh);
                cmd.Parameters.AddWithValue("@MoTa", (object)m.MoTa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@URL", m.URL);
            });
        }

        public void Update(ManHinh m)
        {
            ExecuteNonQuery(@"UPDATE ManHinh 
                              SET TenManHinh = @TenManHinh, MoTa = @MoTa, URL = @URL 
                              WHERE MaManHinh = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", m.MaManHinh);
                cmd.Parameters.AddWithValue("@TenManHinh", m.TenManHinh);
                cmd.Parameters.AddWithValue("@MoTa", (object)m.MoTa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@URL", m.URL);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM ManHinh WHERE MaManHinh = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
