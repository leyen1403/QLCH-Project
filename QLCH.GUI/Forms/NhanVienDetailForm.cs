using QLCH.BLL.Common.Enums;
using QLCH.BLL.Helpers;
using QLCH.BLL.Services;
using QLCH.DAL;
using QLCH.DAL.Models;
using QLCH.DAL.Repositorys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCH.GUI.Forms
{
    public partial class NhanVienDetailForm : Form
    {
        private FormMode _mode;
        private string _maNV;
        private readonly NhanVienService _nhanVienSer;
        private readonly ChucVuService _chucVuSer;
        private readonly PhongBanService _phongBanSer;
        private readonly CuaHangService _cuaHangSer;

        public NhanVienDetailForm(FormMode mode, string maNV = null)
        {
            InitializeComponent();
            _mode = mode;
            _maNV = maNV;
            _nhanVienSer = new NhanVienService();
            _chucVuSer = new ChucVuService();
            _phongBanSer = new PhongBanService();
            _cuaHangSer = new CuaHangService();
        }

        private void NhanVienDetailForm_Load(object sender, EventArgs e)
        {
            GlobalVariables.g_listChucVu = _chucVuSer.GetAll();
            GlobalVariables.g_listPhongBan = _phongBanSer.GetAll();
            GlobalVariables.g_listCuaHang = _cuaHangSer.GetAll();
            InitCombobox();
            if (_mode == FormMode.Insert)
            {
                InitForInsert();
            }
            else if (_mode == FormMode.Update)
            {
                InitForUpdate();
            }
        }

        private void InitCombobox()
        {
            cmbChucVu.DataSource = GlobalVariables.g_listChucVu;
            cmbCuaHang.DataSource = GlobalVariables.g_listCuaHang;
            cmbPhongBan.DataSource = GlobalVariables.g_listPhongBan;

            cmbChucVu.DisplayMember = "TenChucVu";
            cmbChucVu.ValueMember = "MaChucVu";
            cmbCuaHang.DisplayMember = "TenCuaHang";
            cmbCuaHang.ValueMember = "MaCuaHang";
            cmbPhongBan.DisplayMember = "TenPhong";
            cmbPhongBan.ValueMember = "MaPhongBan";
        }

        private void InitForUpdate()
        {
            try
            {
                btnLuu.Text = "Cập nhật";
                var dto = _nhanVienSer.GetNhanVienFull(_maNV);
                txtMaNV.Text = _maNV;
                txtHoTen.Text = dto.HoTen;
                txtcmnd.Text = dto.CMND;
                txtMaSoThue.Text = dto.MaSoThue;
                txtSdt.Text = dto.SoDienThoai;
                txtEmail.Text = dto.Email;
                txtDiaChi.Text = dto.DiaChi;
                dtpNgaySinh.Value = dto.NgaySinh;
                EnumHelper.BindEnumToComboBox<GioiTinhNhanVien>(cmbGioiTinh);
                cmbGioiTinh.SelectedValue = EnumHelper.GetEnumValueFromDescription<GioiTinhNhanVien>(dto.GioiTinh);
                EnumHelper.BindEnumToComboBox<LoaiHopDong>(cmbLoaiHopDong);
                cmbLoaiHopDong.SelectedValue = EnumHelper.GetEnumValueFromDescription<LoaiHopDong>(dto.LoaiHopDong);
                EnumHelper.BindEnumToComboBox<TrangThaiNhanVien>(cmbTrangThaiLamViec);
                cmbTrangThaiLamViec.SelectedValue = EnumHelper.GetEnumValueFromDescription<TrangThaiNhanVien>(dto.TrangThai);
                dtpNgayVaoLam.Value = dto.NgayVaoLam;
                dtpNgayNghiViec.Value = dto.NgayNghiViec;
                txtMaHopDong.Text = dto.MaHopDong.ToString();
                txtLuongCoBan.Text = dto.LuongCoBan.ToString();
                txtThoiHan.Text = dto.ThoiHanHD.ToString();
                EnumHelper.BindEnumToComboBox<TrangThaiHopDongLD>(cmbTrangThaiHopDong);
                cmbTrangThaiHopDong.SelectedValue = EnumHelper.GetEnumValueFromDescription<TrangThaiHopDongLD>(dto.TrangThaiHopDong);
                dtpNgayKy.Value = dto.NgayKy;
                dtpNgayHieuLuc.Value = dto.NgayHieuLuc;
                dtpNgayKetThuc.Value = dto.NgayKetThuc;
                txtMaBaoHiem.Text = dto.MaBH.ToString();
                txtSoBHXH.Text = dto.MaBHXH;
                txtSoBHYT.Text = dto.MaBHYT;
                txtNhaCungCap.Text = dto.NhaCungCap;
                dtpNgayCap.Value = dto.NgayCap;
                EnumHelper.BindEnumToComboBox<TrangThaiBaoHiem>(cmbTrangThaiBaoHiem);
                cmbTrangThaiBaoHiem.SelectedValue = EnumHelper.GetEnumValueFromDescription<TrangThaiBaoHiem>(dto.TrangThaiBaoHiem);                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitForInsert()
        {
            try
            {
                btnLuu.Text = "Thêm mới";
                txtMaNV.Text = "";
                EnumHelper.BindEnumToComboBox<GioiTinhNhanVien>(cmbGioiTinh);
                EnumHelper.BindEnumToComboBox<LoaiHopDong>(cmbLoaiHopDong);
                EnumHelper.BindEnumToComboBox<TrangThaiNhanVien>(cmbTrangThaiLamViec);
                EnumHelper.BindEnumToComboBox<TrangThaiHopDongLD>(cmbTrangThaiHopDong);
                EnumHelper.BindEnumToComboBox<TrangThaiBaoHiem>(cmbTrangThaiBaoHiem);
                cmbGioiTinh.SelectedIndex = 0;
                cmbLoaiHopDong.SelectedIndex = 0;
                cmbTrangThaiLamViec.SelectedIndex = 0;
                cmbTrangThaiHopDong.SelectedIndex = 0;
                cmbTrangThaiBaoHiem.SelectedIndex = 0;
                dtpNgaySinh.Value = DateTime.Now - TimeSpan.FromDays(365 * 20);
                dtpNgayVaoLam.Value = DateTime.Now;
                dtpNgayNghiViec.Value = DateTime.Now;
                dtpNgayKy.Value = DateTime.Now;
                dtpNgayHieuLuc.Value = DateTime.Now;
                dtpNgayKetThuc.Value = DateTime.Now;
                dtpNgayCap.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                var nv = new NhanVien();
                nv.MaNV = txtMaNV.Text;
                nv.HoTen = txtHoTen.Text;
                nv.NgaySinh = dtpNgaySinh.Value;
                nv.GioiTinh = cmbGioiTinh.Text;
                nv.CMND_CCCD = txtcmnd.Text;
                nv.MaSoThue = txtMaSoThue.Text;
                nv.SoDienThoai = txtSdt.Text;
                nv.Email = txtEmail.Text;
                nv.DiaChi = txtDiaChi.Text;
                nv.MaChucVu = Convert.ToInt32(cmbChucVu.SelectedValue);
                nv.MaPhongBan = Convert.ToInt32(cmbPhongBan.SelectedValue);
                nv.MaCuaHang = Convert.ToInt32(cmbCuaHang.SelectedValue);
                nv.LoaiHopDong = cmbLoaiHopDong.Text;
                nv.TrangThai = cmbTrangThaiLamViec.Text;
                nv.NgayVaoLam = dtpNgayVaoLam.Value;
                nv.NgayNghiViec = dtpNgayNghiViec.Value;
                nv.CreatedAt = DateTime.Now;
                nv.UpdatedAt = DateTime.Now;

                var hd = new HopDongLaoDong();
                hd.MaNV = txtMaNV.Text;
                hd.LoaiHopDong = cmbLoaiHopDong.Text;
                hd.NgayKy = dtpNgayKy.Value;
                hd.NgayHieuLuc = dtpNgayHieuLuc.Value;
                hd.NgayKetThuc = dtpNgayKetThuc.Value;
                hd.LuongCoBan = txtLuongCoBan.Text == "" ? 0 : Convert.ToDecimal(txtLuongCoBan.Text);
                hd.ThoiHanHD = txtThoiHan.Text == "" ? 1 : Convert.ToInt32(txtThoiHan.Text);  
                hd.TrangThai = cmbTrangThaiHopDong.Text;

                var bh = new BaoHiem();               
                bh.MaNV = txtMaNV.Text;
                bh.SoBHXH = txtSoBHXH.Text;
                bh.SoBHYT = txtSoBHYT.Text;
                bh.NgayCap = dtpNgayCap.Value;
                bh.NhaCungCap = txtNhaCungCap.Text;
                bh.TrangThai = cmbTrangThaiBaoHiem.Text;

                if (_mode == FormMode.Insert)
                {
                    _nhanVienSer.AddNhanVienFull(nv, hd, bh);
                    MessageBox.Show("Thêm mới thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (_mode == FormMode.Update)
                {
                    hd.MaHopDong = Convert.ToInt32(txtMaHopDong.Text);
                    bh.MaBaoHiem = Convert.ToInt32(txtMaBaoHiem.Text);
                    hd.LuongCoBan = Convert.ToDecimal(txtLuongCoBan.Text);
                    hd.ThoiHanHD = Convert.ToInt32(txtThoiHan.Text);
                    _nhanVienSer.UpdateNhanVienFull(nv, hd, bh);
                    MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
