using QLCH.GUI.Styles;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLCH.GUI.Custom
{
    public class CustomLabel : Label
    {
        private bool useAppFont = true;

        public CustomLabel()
        {
            this.ForeColor = Color.DarkBlue;
            this.Font = AppFonts.NormalFont;
            this.AutoSize = true;

            AppFonts.FontsUpdated += OnFontsUpdated;
        }

        public CustomLabel(string text, Color color, Font font)
        {
            this.Text = text;
            this.ForeColor = color;
            this.Font = font;
            this.AutoSize = true;

            useAppFont = false; 
        }

        private void OnFontsUpdated()
        {
            if (useAppFont)
            {
                this.Font = AppFonts.NormalFont;
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
