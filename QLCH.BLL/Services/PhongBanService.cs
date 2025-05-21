using QLCH.BLL.Interfaces;
using QLCH.DAL.Models;
using QLCH.DAL.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Services
{
    public class PhongBanService : IPhongBanService
    {
        private readonly PhongBanRepository _repo = new PhongBanRepository();

        public List<PhongBan> GetAll()
        {
            var data = _repo.GetAll();
            if (data.Count == 0)
                throw new Exception("Không có phòng ban nào.");
            return data;
        }

        public PhongBan GetById(int id)
        {
            var pb = _repo.GetById(id);
            if (pb == null)
                throw new Exception("Phòng ban không tồn tại.");
            return pb;
        }

        public bool Add(PhongBan pb)
        {
            try
            {
                Validate(pb);
                _repo.Add(pb);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm phòng ban: {ex.Message}");
                return false;
            }
        }

        public bool Update(PhongBan pb)
        {
            try
            {
                Validate(pb);
                if (_repo.GetById(pb.MaPhongBan) == null)
                    throw new Exception("Phòng ban không tồn tại.");
                _repo.Update(pb);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Phòng ban không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa: {ex.Message}");
                return false;
            }
        }

        private void Validate(PhongBan pb)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(pb.TenPhong))
                errors.Add("Tên phòng không được để trống.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
