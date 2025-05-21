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
    public class ChiTietKhuyenMaiService : IChiTietKhuyenMaiService
    {
        private readonly ChiTietKhuyenMaiRepository _repo = new ChiTietKhuyenMaiRepository();

        public List<ChiTietKhuyenMai> GetAll() => _repo.GetAll();

        public ChiTietKhuyenMai GetById(int id) => _repo.GetById(id);

        public bool Add(ChiTietKhuyenMai ct)
        {
            try
            {
                Validate(ct);
                _repo.Add(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm chi tiết khuyến mãi: " + ex.Message);
            }
        }

        public bool Update(ChiTietKhuyenMai ct)
        {
            try
            {
                Validate(ct);
                if (_repo.GetById(ct.MaChiTietKM) == null)
                    throw new Exception("Chi tiết khuyến mãi không tồn tại.");
                _repo.Update(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật: " + ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Chi tiết khuyến mãi không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa: " + ex.Message);
            }
        }

        private void Validate(ChiTietKhuyenMai ct)
        {
            var errors = new List<string>();
            if (ct.MaDonHang <= 0)
                errors.Add("Mã đơn hàng không hợp lệ.");
            if (ct.MaKhuyenMai <= 0)
                errors.Add("Mã khuyến mãi không hợp lệ.");
            if (ct.GiaTriGiam < 0)
                errors.Add("Giá trị giảm không được âm.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
