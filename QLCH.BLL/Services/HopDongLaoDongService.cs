// HopDongLaoDongService.cs
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
                ValidationHelper.Validate<HopDongLaoDong>(hd);
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
                ValidationHelper.Validate<HopDongLaoDong>(hd);
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

        public HopDongLaoDong GetByMaNV(string maNV)
        {
            return _repo.GetByMaNV(maNV);
        }
    }
}
