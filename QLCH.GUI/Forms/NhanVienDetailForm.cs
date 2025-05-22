using QLCH.BLL.Common.Enums;
using QLCH.BLL.Helpers;
using QLCH.BLL.Services;
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

        public NhanVienDetailForm(FormMode mode, string maNV = null)
        {
            InitializeComponent();
            _mode = mode;
            _maNV = maNV;
            _nhanVienSer = new NhanVienService();
        }

        private void NhanVienDetailForm_Load(object sender, EventArgs e)
        {
            if(_mode == FormMode.Insert)
            {
                InitForInsert();
            }
            else if (_mode == FormMode.Update)
            {
                InitForUpdate();
            }
        }

        private void InitForUpdate()
        {
            try
            {
                btnLuu.Text = "Cập nhật";
                var dto = _nhanVienSer.GetNhanVienFull(_maNV);
                Validator.ValidateNhanVienDTO(dto);
            }
            catch(Exception ex)
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

        }

        private void btnDong_Click(object sender, EventArgs e)
        {

        }
    }
}
