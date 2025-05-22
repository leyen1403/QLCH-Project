using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class ChiTietBaoCaoTonKhoRepository
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
                        throw new Exception("Lỗi SQL: " + ex.Message);
                    }
                }
            }
        }

        private List<ChiTietBaoCaoTonKho> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<ChiTietBaoCaoTonKho>();
            using (var conn = new SqlConnection(_connectionString))
            {
                using (var cmd = new SqlCommand(query, conn))
                {
                    if (paramMap != null) paramMap(cmd);
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            result.Add(new ChiTietBaoCaoTonKho
                            {
                                MaChiTiet = Convert.ToInt32(reader["MaChiTiet"]),
                                MaBaoCaoTonKho = Convert.ToInt32(reader["MaBaoCaoTonKho"]),
                                MaBienThe = Convert.ToInt32(reader["MaBienThe"]),
                                SoLuongTon = Convert.ToInt32(reader["SoLuongTon"]),
                                TrangThai = reader["TrangThai"].ToString()
                            });
                        }
                    }
                }
            }
            return result;
        }

        public List<ChiTietBaoCaoTonKho> GetAll() => ExecuteQuery("SELECT * FROM ChiTietBaoCaoTonKho");

        public ChiTietBaoCaoTonKho GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM ChiTietBaoCaoTonKho WHERE MaChiTiet = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(ChiTietBaoCaoTonKho ct)
        {
            ExecuteNonQuery(@"INSERT INTO ChiTietBaoCaoTonKho 
                (MaBaoCaoTonKho, MaBienThe, SoLuongTon, TrangThai)
                VALUES (@MaBaoCaoTonKho, @MaBienThe, @SoLuongTon, @TrangThai)", cmd =>
            {
                cmd.Parameters.AddWithValue("@MaBaoCaoTonKho", ct.MaBaoCaoTonKho);
                cmd.Parameters.AddWithValue("@MaBienThe", ct.MaBienThe);
                cmd.Parameters.AddWithValue("@SoLuongTon", ct.SoLuongTon);
                cmd.Parameters.AddWithValue("@TrangThai", string.IsNullOrEmpty(ct.TrangThai) ? "Còn hàng" : ct.TrangThai);
            });
        }

        public void Update(ChiTietBaoCaoTonKho ct)
        {
            ExecuteNonQuery(@"UPDATE ChiTietBaoCaoTonKho SET 
                MaBaoCaoTonKho = @MaBaoCaoTonKho,
                MaBienThe = @MaBienThe,
                SoLuongTon = @SoLuongTon,
                TrangThai = @TrangThai
                WHERE MaChiTiet = @id", cmd =>
            {
                cmd.Parameters.AddWithValue("@id", ct.MaChiTiet);
                cmd.Parameters.AddWithValue("@MaBaoCaoTonKho", ct.MaBaoCaoTonKho);
                cmd.Parameters.AddWithValue("@MaBienThe", ct.MaBienThe);
                cmd.Parameters.AddWithValue("@SoLuongTon", ct.SoLuongTon);
                cmd.Parameters.AddWithValue("@TrangThai", ct.TrangThai);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM ChiTietBaoCaoTonKho WHERE MaChiTiet = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }
    }
}
