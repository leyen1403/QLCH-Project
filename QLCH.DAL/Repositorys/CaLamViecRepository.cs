using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class CaLamViecRepository
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

        private List<CaLamViec> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<CaLamViec>();
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
                            result.Add(new CaLamViec
                            {
                                MaCa = Convert.ToInt32(reader["MaCa"]),
                                TenCa = reader["TenCa"].ToString(),
                                GioBatDau = (TimeSpan)reader["GioBatDau"],
                                GioKetThuc = (TimeSpan)reader["GioKetThuc"],
                                MoTa = reader["MoTa"]?.ToString()
                            });
                        }
                        return result;
                    }
                }
            }
        }

        public List<CaLamViec> GetAll() => ExecuteQuery("SELECT * FROM CaLamViec");

        public CaLamViec GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM CaLamViec WHERE MaCa = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(CaLamViec ca)
        {
            ExecuteNonQuery(@"INSERT INTO CaLamViec (TenCa, GioBatDau, GioKetThuc, MoTa) 
                              VALUES (@TenCa, @GioBatDau, @GioKetThuc, @MoTa)", cmd =>
            {
                cmd.Parameters.AddWithValue("@TenCa", ca.TenCa);
                cmd.Parameters.AddWithValue("@GioBatDau", ca.GioBatDau);
                cmd.Parameters.AddWithValue("@GioKetThuc", ca.GioKetThuc);
                cmd.Parameters.AddWithValue("@MoTa", (object)ca.MoTa ?? DBNull.Value);
            });
        }

        public void Update(CaLamViec ca)
        {
            ExecuteNonQuery(@"UPDATE CaLamViec SET TenCa = @TenCa, GioBatDau = @GioBatDau, GioKetThuc = @GioKetThuc, MoTa = @MoTa 
                              WHERE MaCa = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", ca.MaCa);
                cmd.Parameters.AddWithValue("@TenCa", ca.TenCa);
                cmd.Parameters.AddWithValue("@GioBatDau", ca.GioBatDau);
                cmd.Parameters.AddWithValue("@GioKetThuc", ca.GioKetThuc);
                cmd.Parameters.AddWithValue("@MoTa", (object)ca.MoTa ?? DBNull.Value);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM CaLamViec WHERE MaCa = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
