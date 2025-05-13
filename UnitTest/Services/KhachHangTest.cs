using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLCH.BLL.Interfaces;
using QLCH.BLL.Services;
using QLCH.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Services.Tests
{
    [TestClass()]
    public class KhachHangTest
    {
        private IKhachHangService _khachHangService;
        public KhachHangTest()
        {
            _khachHangService = new KhachHangService();
        }
        [TestMethod()]
        public void GetByIDTest()
        {
            KhachHang khachHang = _khachHangService.GetByID("KH000001");
        }

        [TestMethod()]
        public void AddTest()
        {
            KhachHang khachHang = new KhachHang();
            khachHang.TenKH = "Tên nè";
            khachHang.Email = "Email nè";
            _khachHangService.Add(khachHang);
        }

        [TestMethod()]
        public void GetAllTest()
        {
            var list = _khachHangService.GetAll();
        }
    }
}