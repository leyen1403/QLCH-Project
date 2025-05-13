using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using QLCH.BLL.Services;
using QLCH.DAL;
using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Services.Tests
{
    [TestClass()]
    public class NhanVienServiceTests
    {
        private readonly NhanVienService _nhanVienService;
        public NhanVienServiceTests()
        {
            GlobalVariables.IsTestMode = true;
            _nhanVienService = new NhanVienService();
        }
        [TestMethod()]
        public void AddTest()
        {
            var nhanVien = new NhanVien
            {
                MaNV = "NV001",
                TenNV = "Nguyen Van A",
                DiaChi = "123 Đường ABC",
                SDT = "0987654321",
                Email = "vana@gmail.com",
                ChucVu = "Quản lý",
                MucLuong = 15000000,
                ThoiGianTao = DateTime.Now,
                ThoiGianCapNhat = DateTime.Now,
                TrangThai = true
            };

            // Act
            _nhanVienService.Add(nhanVien);
        }

        [TestMethod()]
        public void DeleteTest()
        {

        }

        [TestMethod()]
        public void GetAllTest()
        {

        }

        [TestMethod()]
        public void GetByIDTest()
        {

        }

        [TestMethod()]
        public void UpdateTest()
        {

        }
    }
}