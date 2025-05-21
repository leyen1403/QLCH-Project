// PhongBanRepository.cs
using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class PhongBanRepository
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
                        throw new Exception($"Lỗi khi thực thi SQL: {ex.Message}");
                    }
                }
            }
        }

        private List<PhongBan> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<PhongBan>();
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
                            result.Add(new PhongBan
                            {
                                MaPhongBan = Convert.ToInt32(reader["MaPhongBan"]),
                                TenPhong = reader["TenPhong"].ToString(),
                                MoTa = reader["MoTa"]?.ToString()
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<PhongBan> GetAll() => ExecuteQuery("SELECT * FROM PhongBan");

        public PhongBan GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM PhongBan WHERE MaPhongBan = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(PhongBan pb)
        {
            string query = @"INSERT INTO PhongBan (TenPhong, MoTa) VALUES (@TenPhong, @MoTa)";
            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@TenPhong", pb.TenPhong);
                cmd.Parameters.AddWithValue("@MoTa", (object)pb.MoTa ?? DBNull.Value);
            });
        }

        public void Update(PhongBan pb)
        {
            string query = @"UPDATE PhongBan SET TenPhong = @TenPhong, MoTa = @MoTa WHERE MaPhongBan = @id";
            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@id", pb.MaPhongBan);
                cmd.Parameters.AddWithValue("@TenPhong", pb.TenPhong);
                cmd.Parameters.AddWithValue("@MoTa", (object)pb.MoTa ?? DBNull.Value);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM PhongBan WHERE MaPhongBan = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
