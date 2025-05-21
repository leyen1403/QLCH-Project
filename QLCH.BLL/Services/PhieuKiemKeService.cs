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
    public class PhieuKiemKeService : IPhieuKiemKeService
    {
        private readonly PhieuKiemKeRepository _repo = new PhieuKiemKeRepository();

        public List<PhieuKiemKe> GetAll() => _repo.GetAll();

        public PhieuKiemKe GetById(int id) => _repo.GetById(id);

        public bool Add(PhieuKiemKe pk)
        {
            try
            {
                Validate(pk);
                _repo.Add(pk);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm phiếu kiểm kê: " + ex.Message);
            }
        }

        public bool Update(PhieuKiemKe pk)
        {
            try
            {
                Validate(pk);
                if (_repo.GetById(pk.MaPhieuKiemKe) == null)
                    throw new Exception("Phiếu kiểm kê không tồn tại.");
                _repo.Update(pk);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật phiếu kiểm kê: " + ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Phiếu kiểm kê không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa phiếu kiểm kê: " + ex.Message);
            }
        }

        private void Validate(PhieuKiemKe pk)
        {
            var errors = new List<string>();
            if (pk.MaKho <= 0)
                errors.Add("Mã kho không hợp lệ.");
            if (string.IsNullOrWhiteSpace(pk.NguoiKiemKe))
                errors.Add("Người kiểm kê không được để trống.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
