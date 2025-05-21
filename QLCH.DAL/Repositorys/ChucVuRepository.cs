using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class ChucVuRepository
    {
        private readonly string _connectionString = GlobalVariables.ConnectionString;

        private void ExecuteNonQuery(string query, Action<SqlCommand> parameterMapper)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn, trans))
                        {
                            parameterMapper(cmd);
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

        private List<ChucVu> ExecuteQuery(string query, Action<SqlCommand> parameterMapper = null)
        {
            var result = new List<ChucVu>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    parameterMapper?.Invoke(cmd);
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ChucVu
                            {
                                MaChucVu = Convert.ToInt32(reader["MaChucVu"]),
                                TenChucVu = reader["TenChucVu"].ToString(),
                                HeSoLuong = Convert.ToDecimal(reader["HeSoLuong"]),
                                MoTa = reader["MoTa"]?.ToString()
                            });
                        }
                    }
                }
            }

            return result;
        }

        public List<ChucVu> GetAll() => ExecuteQuery("SELECT * FROM ChucVu");

        public ChucVu GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM ChucVu WHERE MaChucVu = @MaChucVu",
                cmd => cmd.Parameters.AddWithValue("@MaChucVu", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(ChucVu chucVu)
        {
            string query = @"
                INSERT INTO ChucVu (TenChucVu, HeSoLuong, MoTa)
                VALUES (@TenChucVu, @HeSoLuong, @MoTa)";
            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@TenChucVu", chucVu.TenChucVu);
                cmd.Parameters.AddWithValue("@HeSoLuong", chucVu.HeSoLuong);
                cmd.Parameters.AddWithValue("@MoTa", (object)chucVu.MoTa ?? DBNull.Value);
            });
        }

        public void Update(ChucVu chucVu)
        {
            string query = @"
                UPDATE ChucVu
                SET TenChucVu = @TenChucVu,
                    HeSoLuong = @HeSoLuong,
                    MoTa = @MoTa
                WHERE MaChucVu = @MaChucVu";
            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@TenChucVu", chucVu.TenChucVu);
                cmd.Parameters.AddWithValue("@HeSoLuong", chucVu.HeSoLuong);
                cmd.Parameters.AddWithValue("@MoTa", (object)chucVu.MoTa ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@MaChucVu", chucVu.MaChucVu);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM ChucVu WHERE MaChucVu = @MaChucVu",
                cmd => cmd.Parameters.AddWithValue("@MaChucVu", id));
        }
    }
}
