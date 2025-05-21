using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class PhanQuyenManHinhRepository
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

        private List<PhanQuyenManHinh> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<PhanQuyenManHinh>();
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
                            result.Add(new PhanQuyenManHinh
                            {
                                MaPhanQuyen = Convert.ToInt32(reader["MaPhanQuyen"]),
                                MaTaiKhoan = Convert.ToInt32(reader["MaTaiKhoan"]),
                                MaManHinh = Convert.ToInt32(reader["MaManHinh"]),
                                CoQuyen = Convert.ToBoolean(reader["CoQuyen"])
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<PhanQuyenManHinh> GetAll() => ExecuteQuery("SELECT * FROM PhanQuyenManHinh");

        public PhanQuyenManHinh GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM PhanQuyenManHinh WHERE MaPhanQuyen = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(PhanQuyenManHinh pq)
        {
            ExecuteNonQuery(@"INSERT INTO PhanQuyenManHinh (MaTaiKhoan, MaManHinh, CoQuyen) 
                              VALUES (@MaTaiKhoan, @MaManHinh, @CoQuyen)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaTaiKhoan", pq.MaTaiKhoan);
                cmd.Parameters.AddWithValue("@MaManHinh", pq.MaManHinh);
                cmd.Parameters.AddWithValue("@CoQuyen", pq.CoQuyen);
            });
        }

        public void Update(PhanQuyenManHinh pq)
        {
            ExecuteNonQuery(@"UPDATE PhanQuyenManHinh 
                              SET MaTaiKhoan = @MaTaiKhoan, MaManHinh = @MaManHinh, CoQuyen = @CoQuyen 
                              WHERE MaPhanQuyen = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", pq.MaPhanQuyen);
                cmd.Parameters.AddWithValue("@MaTaiKhoan", pq.MaTaiKhoan);
                cmd.Parameters.AddWithValue("@MaManHinh", pq.MaManHinh);
                cmd.Parameters.AddWithValue("@CoQuyen", pq.CoQuyen);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM PhanQuyenManHinh WHERE MaPhanQuyen = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
