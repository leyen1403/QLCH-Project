// ChucVuService.cs
using QLCH.BLL.Helpers;
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
    public class ChucVuService : IChucVuService
    {
        private readonly ChucVuRepository _repo = new ChucVuRepository();

        public List<ChucVu> GetAll()
        {
            return _repo.GetAll();
        }

        public ChucVu GetById(int id)
        {
            return _repo.GetById(id);
        }

        public bool Add(ChucVu chucVu)
        {
            try
            {
                ValidationHelper.Validate<ChucVu>(chucVu);
                _repo.Add(chucVu);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm chức vụ: {ex.Message}");
                throw new Exception($"Lỗi khi thêm chức vụ: {ex.Message}");
            }
        }

        public bool Update(ChucVu chucVu)
        {
            try
            {
                ValidationHelper.Validate<ChucVu>(chucVu);
                if (_repo.GetById(chucVu.MaChucVu) == null)
                    throw new Exception("Chức vụ không tồn tại.");
                _repo.Update(chucVu);
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
                    throw new Exception("Chức vụ không tồn tại.");
                _repo.Delete(id);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi xóa: {ex.Message}");
                throw new Exception($"Lỗi khi xóa: {ex.Message}");
            }
        }
    }
}
