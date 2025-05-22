using System;
using System.Drawing;

namespace QLCH.GUI.Styles
{
    public static class AppFonts
    {
        // Sự kiện dùng để thông báo cho các control đăng ký theo dõi
        public static event Action FontsUpdated;

        private static Font _titleFont = new Font("Segoe UI", 20, FontStyle.Bold);
        public static Font TitleFont
        {
            get => _titleFont;
            set
            {
                _titleFont = value;
                FontsUpdated?.Invoke();
            }
        }

        private static Font _normalFont = new Font("Segoe UI", 12, FontStyle.Regular);
        public static Font NormalFont
        {
            get => _normalFont;
            set
            {
                _normalFont = value;
                FontsUpdated?.Invoke();
            }
        }

        private static Font _smallFont = new Font("Segoe UI", 10, FontStyle.Italic);
        public static Font SmallFont
        {
            get => _smallFont;
            set
            {
                _smallFont = value;
                FontsUpdated?.Invoke();
            }
        }

        private static Font _buttonFont = new Font("Segoe UI", 12, FontStyle.Bold);
        public static Font ButtonFont
        {
            get => _buttonFont;
            set
            {
                _buttonFont = value;
                FontsUpdated?.Invoke();
            }
        }

        // Nếu cần gọi cập nhật bằng tay
        public static void Reload()
        {
            FontsUpdated?.Invoke();
        }
    }
}
