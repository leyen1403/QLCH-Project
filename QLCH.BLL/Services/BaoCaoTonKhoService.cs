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
    public class BaoCaoTonKhoService : IBaoCaoTonKhoService
    {
        private readonly BaoCaoTonKhoRepository _repo = new BaoCaoTonKhoRepository();

        public List<BaoCaoTonKho> GetAll() => _repo.GetAll();

        public BaoCaoTonKho GetById(int id) => _repo.GetById(id);

        public bool Add(BaoCaoTonKho bc)
        {
            try
            {
                Validate(bc);
                _repo.Add(bc);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm báo cáo: " + ex.Message);
            }
        }

        public bool Update(BaoCaoTonKho bc)
        {
            try
            {
                Validate(bc);
                if (_repo.GetById(bc.MaBaoCaoTonKho) == null)
                    throw new Exception("Báo cáo tồn kho không tồn tại.");
                _repo.Update(bc);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật báo cáo: " + ex.Message);
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Báo cáo tồn kho không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi xóa báo cáo: " + ex.Message);
            }
        }

        private void Validate(BaoCaoTonKho bc)
        {
            var errors = new List<string>();
            if (bc.MaKho <= 0)
                errors.Add("Mã kho không hợp lệ.");
            if (bc.TongSanPham < 0)
                errors.Add("Tổng sản phẩm không được âm.");
            if (bc.TongSoLuong < 0)
                errors.Add("Tổng số lượng không được âm.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
