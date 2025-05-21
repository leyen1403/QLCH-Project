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
    public class PhanQuyenManHinhService : IPhanQuyenManHinhService
    {
        private readonly PhanQuyenManHinhRepository _repo = new PhanQuyenManHinhRepository();

        public List<PhanQuyenManHinh> GetAll() => _repo.GetAll();

        public PhanQuyenManHinh GetById(int id) => _repo.GetById(id);

        public bool Add(PhanQuyenManHinh pq)
        {
            try
            {
                Validate(pq);
                _repo.Add(pq);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm phân quyền: {ex.Message}");
            }
        }

        public bool Update(PhanQuyenManHinh pq)
        {
            try
            {
                Validate(pq);
                if (_repo.GetById(pq.MaPhanQuyen) == null)
                    throw new Exception("Phân quyền không tồn tại.");
                _repo.Update(pq);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật phân quyền: {ex.Message}");
            }
        }

        public bool Delete(int id)
        {
            try
            {
                if (_repo.GetById(id) == null)
                    throw new Exception("Phân quyền không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa phân quyền: {ex.Message}");
            }
        }

        private void Validate(PhanQuyenManHinh pq)
        {
            var errors = new List<string>();
            if (pq.MaTaiKhoan <= 0)
                errors.Add("Mã tài khoản không hợp lệ.");
            if (pq.MaManHinh <= 0)
                errors.Add("Mã màn hình không hợp lệ.");
            if (errors.Count > 0)
                throw new Exception(string.Join("\n", errors));
        }
    }
}
