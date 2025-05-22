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
    public class BaoHiemService : IBaoHiemService
    {
        private readonly BaoHiemRepository _repo = new BaoHiemRepository();

        public List<BaoHiem> GetAll()
        {
            return _repo.GetAll();
        }

        public BaoHiem GetById(int id)
        {
            return _repo.GetById(id);
        }

        public bool Add(BaoHiem bh)
        {
            try
            {
                Validate(bh);
                _repo.Add(bh);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm: {ex.Message}");
                throw new Exception($"Lỗi khi thêm bảo hiểm: {ex.Message}");
            }
        }

        public bool Update(BaoHiem bh)
        {
            try
            {
                Validate(bh);
                if (_repo.GetById(bh.MaBaoHiem) == null)
                    throw new Exception("Bảo hiểm không tồn tại.");
                _repo.Update(bh);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật: {ex.Message}");
                throw new Exception($"Lỗi khi cập nhật bảo hiểm: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Bảo hiểm không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa: {ex.Message}");
                throw new Exception($"Lỗi khi xóa bảo hiểm: {ex.Message}");
            }
        }

        private void Validate(BaoHiem bh)
        {
            var errors = new List<string>();
            if (string.IsNullOrWhiteSpace(bh.MaNV))
                errors.Add("Mã nhân viên không được để trống.");
            if (string.IsNullOrWhiteSpace(bh.SoBHXH))
                errors.Add("Số BHXH không được để trống.");
            if (string.IsNullOrWhiteSpace(bh.SoBHYT))
                errors.Add("Số BHYT không được để trống.");
            if (string.IsNullOrWhiteSpace(bh.NhaCungCap))
                errors.Add("Nhà cung cấp không được để trống.");
            if (bh.NgayCap == default)
                errors.Add("Ngày cấp không hợp lệ.");

            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }

        public BaoHiem GetByMaNV(string maNV)
        {
            return _repo.GetByMaNV(maNV);
        }
    }
}
