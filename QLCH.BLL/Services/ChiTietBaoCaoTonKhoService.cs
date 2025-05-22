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
    public class ChiTietBaoCaoTonKhoService : IChiTietBaoCaoTonKhoService
    {
        private readonly ChiTietBaoCaoTonKhoRepository _repo = new ChiTietBaoCaoTonKhoRepository();

        public List<ChiTietBaoCaoTonKho> GetAll() => _repo.GetAll();

        public ChiTietBaoCaoTonKho GetById(int id) => _repo.GetById(id);

        public bool Add(ChiTietBaoCaoTonKho ct)
        {
            try
            {
                Validate(ct);
                _repo.Add(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm chi tiết báo cáo: " + ex.Message);
            }
        }

        public bool Update(ChiTietBaoCaoTonKho ct)
        {
            try
            {
                Validate(ct);
                if (_repo.GetById(ct.MaChiTiet) == null)
                    throw new Exception("Chi tiết báo cáo không tồn tại.");
                _repo.Update(ct);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật chi tiết báo cáo: " + ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Chi tiết báo cáo không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa chi tiết báo cáo: " + ex.Message);
            }
        }

        private void Validate(ChiTietBaoCaoTonKho ct)
        {
            var errors = new List<string>();
            if (ct.MaBaoCaoTonKho <= 0)
                errors.Add("Mã báo cáo tồn kho không hợp lệ.");
            if (ct.MaBienThe <= 0)
                errors.Add("Mã biến thể không hợp lệ.");
            if (ct.SoLuongTon < 0)
                errors.Add("Số lượng tồn phải >= 0.");
            if (!string.IsNullOrEmpty(ct.TrangThai) &&
                ct.TrangThai != "Còn hàng" && ct.TrangThai != "Hết hàng")
                errors.Add("Trạng thái không hợp lệ.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
