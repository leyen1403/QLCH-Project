using Microsoft.VisualStudio.TestTools.UnitTesting;
using QLCH.BLL.Interfaces;
using QLCH.DAL;
using QLCH.DAL.Models;

namespace QLCH.BLL.Services.Tests
{
    [TestClass()]
    public class KhachHangTest
    {
        private IKhachHangService _khachHangService;
        public KhachHangTest()
        {            
            GlobalVariables.IsTestMode = true;
            _khachHangService = new KhachHangService();
        }
        [TestMethod()]
        public void GetByIDTest()
        {
            KhachHang khachHang = _khachHangService.GetByID("KH000003");
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