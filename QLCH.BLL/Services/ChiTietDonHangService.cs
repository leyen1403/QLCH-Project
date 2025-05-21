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
    public class ChiTietDonHangService : IChiTietDonHangService
    {
        private readonly ChiTietDonHangRepository _repo = new ChiTietDonHangRepository();

        public List<ChiTietDonHang> GetAll() => _repo.GetAll();

        public ChiTietDonHang GetById(int id) => _repo.GetById(id);

        public bool Add(ChiTietDonHang ct)
        {
            try
            {
                Validate(ct);
                _repo.Add(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm chi tiết đơn hàng: " + ex.Message);
            }
        }

        public bool Update(ChiTietDonHang ct)
        {
            try
            {
                Validate(ct);
                if (_repo.GetById(ct.MaChiTietDH) == null)
                    throw new Exception("Chi tiết đơn hàng không tồn tại.");
                _repo.Update(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật chi tiết đơn hàng: " + ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Chi tiết đơn hàng không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa chi tiết đơn hàng: " + ex.Message);
            }
        }

        private void Validate(ChiTietDonHang ct)
        {
            var errors = new List<string>();
            if (ct.MaDonHang <= 0) errors.Add("Mã đơn hàng không hợp lệ.");
            if (ct.MaBienThe <= 0) errors.Add("Mã biến thể không hợp lệ.");
            if (ct.SoLuong <= 0) errors.Add("Số lượng phải > 0.");
            if (ct.GiaBan < 0) errors.Add("Giá bán phải >= 0.");
            if (errors.Count > 0) throw new Exception(string.Join("\n", errors));
        }
    }
}
