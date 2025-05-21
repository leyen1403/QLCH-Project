// HopDongLaoDongService.cs
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
    public class HopDongLaoDongService : IHopDongLaoDongService
    {
        private readonly HopDongLaoDongRepository _repo = new HopDongLaoDongRepository();

        public List<HopDongLaoDong> GetAll()
        {
            return _repo.GetAll();
        }

        public HopDongLaoDong GetById(int id)
        {
            return _repo.GetById(id);
        }

        public bool Add(HopDongLaoDong hd)
        {
            try
            {
                Validate(hd);
                _repo.Add(hd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm: {ex.Message}");
                throw new Exception($"Lỗi khi thêm: {ex.Message}");
            }
        }

        public bool Update(HopDongLaoDong hd)
        {
            try
            {
                Validate(hd);
                if (_repo.GetById(hd.MaHopDong) == null)
                    throw new Exception("Hợp đồng không tồn tại.");
                _repo.Update(hd);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật: {ex.Message}");
                throw new Exception($"Lỗi khi cập nhật: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Hợp đồng không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa: {ex.Message}");
                throw new Exception($"Lỗi khi xóa: {ex.Message}");
            }
        }

        private void Validate(HopDongLaoDong hd)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(hd.MaNV))
                errors.Add("Mã nhân viên không được để trống.");
            if (string.IsNullOrWhiteSpace(hd.LoaiHopDong))
                errors.Add("Loại hợp đồng không được để trống.");
            if (hd.LuongCoBan <= 0)
                errors.Add("Lương cơ bản phải lớn hơn 0.");
            if (hd.ThoiHanHD <= 0)
                errors.Add("Thời hạn hợp đồng phải lớn hơn 0.");
            if (hd.NgayHieuLuc < hd.NgayKy)
                errors.Add("Ngày hiệu lực không được trước ngày ký.");
            if (hd.NgayKetThuc.HasValue && hd.NgayKetThuc < hd.NgayHieuLuc)
                errors.Add("Ngày kết thúc phải sau ngày hiệu lực.");

            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
