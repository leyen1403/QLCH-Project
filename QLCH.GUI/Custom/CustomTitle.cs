using System;
using System.Drawing;
using System.Windows.Forms;
using QLCH.GUI.Styles;

namespace QLCH.GUI.Custom
{
    public class CustomTitle : Label
    {
        private bool useAppFont = true;

        public CustomTitle()
        {
            this.ForeColor = Color.DarkBlue;
            this.Font = AppFonts.TitleFont;
            this.AutoSize = true;
            this.TextAlign = ContentAlignment.MiddleCenter;

            AppFonts.FontsUpdated += OnFontsUpdated;
        }

        public CustomTitle(string text, Color color, Font font)
        {
            this.Text = text;
            this.ForeColor = color;
            this.Font = font;
            this.AutoSize = true;
            this.TextAlign = ContentAlignment.MiddleCenter;

            useAppFont = false;
        }

        private void OnFontsUpdated()
        {
            if (useAppFont)
            {
                this.Font = AppFonts.TitleFont;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                AppFonts.FontsUpdated -= OnFontsUpdated;
            }
            base.Dispose(disposing);
        }
    }
}
