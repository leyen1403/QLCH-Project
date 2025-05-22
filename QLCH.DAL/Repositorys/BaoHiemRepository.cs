using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.DAL.Repositorys
{
    public class BaoHiemRepository
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

        private List<BaoHiem> ExecuteQuery(string query, Action<SqlCommand> paramMap = null)
        {
            var result = new List<BaoHiem>();
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(query, conn))
            {
                paramMap?.Invoke(cmd);
                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(new BaoHiem
                        {
                            MaBaoHiem = Convert.ToInt32(reader["MaBaoHiem"]),
                            MaNV = reader["MaNV"].ToString(),
                            SoBHXH = reader["SoBHXH"].ToString(),
                            SoBHYT = reader["SoBHYT"].ToString(),
                            NgayCap = Convert.ToDateTime(reader["NgayCap"]),
                            NhaCungCap = reader["NhaCungCap"].ToString(),
                            TrangThai = reader["TrangThai"].ToString()
                        });
                    }
                }
            }
            return result;
        }

        public List<BaoHiem> GetAll() => ExecuteQuery("SELECT * FROM BaoHiem");

        public BaoHiem GetById(int id)
        {
            var list = ExecuteQuery("SELECT * FROM BaoHiem WHERE MaBaoHiem = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
            return list.Count > 0 ? list[0] : null;
        }

        public void Add(BaoHiem bh)
        {
            string query = @"
                INSERT INTO BaoHiem (MaNV, SoBHXH, SoBHYT, NgayCap, NhaCungCap, TrangThai)
                VALUES (@MaNV, @SoBHXH, @SoBHYT, @NgayCap, @NhaCungCap, @TrangThai)";
            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@MaNV", bh.MaNV);
                cmd.Parameters.AddWithValue("@SoBHXH", bh.SoBHXH);
                cmd.Parameters.AddWithValue("@SoBHYT", bh.SoBHYT);
                cmd.Parameters.AddWithValue("@NgayCap", bh.NgayCap);
                cmd.Parameters.AddWithValue("@NhaCungCap", bh.NhaCungCap);
                cmd.Parameters.AddWithValue("@TrangThai", bh.TrangThai ?? "Còn hiệu lực");
            });
        }

        public void Update(BaoHiem bh)
        {
            string query = @"
                UPDATE BaoHiem SET 
                    MaNV = @MaNV, SoBHXH = @SoBHXH, SoBHYT = @SoBHYT,
                    NgayCap = @NgayCap, NhaCungCap = @NhaCungCap, TrangThai = @TrangThai
                WHERE MaBaoHiem = @id";
            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@id", bh.MaBaoHiem);
                cmd.Parameters.AddWithValue("@MaNV", bh.MaNV);
                cmd.Parameters.AddWithValue("@SoBHXH", bh.SoBHXH);
                cmd.Parameters.AddWithValue("@SoBHYT", bh.SoBHYT);
                cmd.Parameters.AddWithValue("@NgayCap", bh.NgayCap);
                cmd.Parameters.AddWithValue("@NhaCungCap", bh.NhaCungCap);
                cmd.Parameters.AddWithValue("@TrangThai", bh.TrangThai);
            });
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM BaoHiem WHERE MaBaoHiem = @id",
                cmd => cmd.Parameters.AddWithValue("@id", id));
        }

        public BaoHiem GetByMaNV(string maNV)
        {
            var list = ExecuteQuery("SELECT * FROM BaoHiem WHERE MaNV = @maNV",
                cmd => cmd.Parameters.AddWithValue("@maNV", maNV));
            return list.Count > 0 ? list[0] : null;
        }

        public void UpdateByMaNV(BaoHiem bh)
        {
            string query = @"
                UPDATE BaoHiem SET 
                    MaBaoHiem = @id, SoBHXH = @SoBHXH, SoBHYT = @SoBHYT,
                    NgayCap = @NgayCap, NhaCungCap = @NhaCungCap, TrangThai = @TrangThai
                WHERE MaNV = @MaNV";
            ExecuteNonQuery(query, cmd =>
            {
                cmd.Parameters.AddWithValue("@id", bh.MaBaoHiem);
                cmd.Parameters.AddWithValue("@MaNV", bh.MaNV);
                cmd.Parameters.AddWithValue("@SoBHXH", bh.SoBHXH);
                cmd.Parameters.AddWithValue("@SoBHYT", bh.SoBHYT);
                cmd.Parameters.AddWithValue("@NgayCap", bh.NgayCap);
                cmd.Parameters.AddWithValue("@NhaCungCap", bh.NhaCungCap);
                cmd.Parameters.AddWithValue("@TrangThai", bh.TrangThai);
            });
        }
    }
}
