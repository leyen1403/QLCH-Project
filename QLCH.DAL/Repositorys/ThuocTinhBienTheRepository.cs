using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class ThuocTinhBienTheRepository
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

        private List<ThuocTinhBienThe> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<ThuocTinhBienThe>();
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
                            result.Add(new ThuocTinhBienThe
                            {
                                MaThuocTinhBienThe = Convert.ToInt32(reader["MaThuocTinhBienThe"]),
                                MaBienThe = Convert.ToInt32(reader["MaBienThe"]),
                                LoaiThuocTinh = reader["LoaiThuocTinh"].ToString(),
                                GiaTri = reader["GiaTri"].ToString()
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<ThuocTinhBienThe> GetAll() => ExecuteQuery("SELECT * FROM ThuocTinhBienThe");

        public ThuocTinhBienThe GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM ThuocTinhBienThe WHERE MaThuocTinhBienThe = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(ThuocTinhBienThe tt)
        {
            ExecuteNonQuery(@"INSERT INTO ThuocTinhBienThe (MaBienThe, LoaiThuocTinh, GiaTri)
                              VALUES (@MaBienThe, @LoaiThuocTinh, @GiaTri)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaBienThe", tt.MaBienThe);
                cmd.Parameters.AddWithValue("@LoaiThuocTinh", tt.LoaiThuocTinh);
                cmd.Parameters.AddWithValue("@GiaTri", tt.GiaTri);
            });
        }

        public void Update(ThuocTinhBienThe tt)
        {
            ExecuteNonQuery(@"UPDATE ThuocTinhBienThe SET 
                                MaBienThe = @MaBienThe, 
                                LoaiThuocTinh = @LoaiThuocTinh, 
                                GiaTri = @GiaTri
                              WHERE MaThuocTinhBienThe = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", tt.MaThuocTinhBienThe);
                cmd.Parameters.AddWithValue("@MaBienThe", tt.MaBienThe);
                cmd.Parameters.AddWithValue("@LoaiThuocTinh", tt.LoaiThuocTinh);
                cmd.Parameters.AddWithValue("@GiaTri", tt.GiaTri);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM ThuocTinhBienThe WHERE MaThuocTinhBienThe = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
