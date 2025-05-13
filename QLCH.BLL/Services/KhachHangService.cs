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
    public class KhachHangService : IKhachHangService
    {
        private readonly KhachHangRepository _khachHangRepository;
        public KhachHangService()
        {
            _khachHangRepository = new KhachHangRepository();
        }
        public void Add(KhachHang khachHang)
        {
            // Kiểm tra xem mã khách hàng đã tồn tại hay chưa
            var existingKhachHang = _khachHangRepository.GetByID(khachHang.MaKH);
            khachHang.MaKH = AutomaticGenerateMaKH();
            ValidateKhachHang(khachHang);
            if (existingKhachHang != null)
            {
                throw new InvalidOperationException("Mã khách hàng đã tồn tại");
            }
            _khachHangRepository.Add(khachHang);
        }

        public void Delete(string MaKH)
        {
            var exitsingKhachHang = _khachHangRepository.GetByID(MaKH);            
            _khachHangRepository.Delete(MaKH);
        }

        public List<KhachHang> GetAll()
        {
            var khachHangs = _khachHangRepository.GetAll();
            return khachHangs;
        }

        public KhachHang GetByID(string MaKH)
        {
            KhachHang khachHang = _khachHangRepository.GetByID(MaKH);
            if (khachHang == null)
            {
                throw new KeyNotFoundException("Không tìm thấy khách hàng\nMã khách hàng: " + MaKH);
            }
            return khachHang;
        }

        public List<KhachHang> Search(string keyword)
        {
            throw new NotImplementedException();
        }

        public List<KhachHang> Sort(string sortBy)
        {
            throw new NotImplementedException();
        }

        public void Update(KhachHang khachHang)
        {
            ValidateKhachHang(khachHang);
            var existingKhachHang = _khachHangRepository.GetByID(khachHang.MaKH);
            if (existingKhachHang == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy khách hàng với mã: {khachHang.MaKH}");
            }
            khachHang.ThoiGianCapNhat = DateTime.Now;
            _khachHangRepository.Update(khachHang);
        }

        private void ValidateKhachHang(KhachHang khachHang)
        {
            if (khachHang == null)
                throw new ArgumentNullException(nameof(khachHang), "Đối tượng khách hàng không được null");

            var errorList = new List<string>();

            if (string.IsNullOrEmpty(khachHang.MaKH))
                errorList.Add("Mã khách hàng không được để trống");

            if (string.IsNullOrEmpty(khachHang.TenKH))
                errorList.Add("Tên khách hàng không được để trống");

            if (string.IsNullOrEmpty(khachHang.SDT) && string.IsNullOrEmpty(khachHang.Email))
                errorList.Add("Số điện thoại hoặc email không được để trống");

            if (errorList.Count > 0)
            {
                string errorMessage = string.Join("\n", errorList);
                throw new ArgumentException(errorMessage);
            }
        }

        private string AutomaticGenerateMaKH()
        {
            // Lấy mã khách hàng cuối cùng từ database
            var lastMaKH = _khachHangRepository.GetLastMaKH();

            // Nếu chưa có khách hàng nào, bắt đầu từ KH000001
            if (string.IsNullOrEmpty(lastMaKH))
            {
                return "KH000001";
            }

            // Tách phần số từ "KH000001" => "000001"
            string numberPart = lastMaKH.Substring(2);

            // Thử chuyển đổi thành số, nếu thất bại thì trả về KH000001
            if (int.TryParse(numberPart, out int lastNumber))
            {
                int newNumber = lastNumber + 1;
                return "KH" + newNumber.ToString("D6");
            }
            else
            {
                // Nếu lỗi format, quay lại mã mặc định
                return "KH000001";
            }
        }

    }
}
