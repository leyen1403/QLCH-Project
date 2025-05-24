using System;
using System.Drawing;
using System.Windows.Forms;
using QLCH.GUI.Styles; // <- import AppFonts

namespace QLCH.GUI.Custom
{
    public class CustomGroupBox : GroupBox
    {
        public Color TitleForeColor { get; set; } = Color.DarkBlue;
        public Font TitleFont { get; set; }

        public CustomGroupBox()
        {
            this.ForeColor = Color.Black;
            this.BackColor = Color.Transparent;
            this.TitleFont = AppFonts.TitleFont;
            this.Font = AppFonts.NormalFont;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.Clear(this.BackColor);

            Size textSize = TextRenderer.MeasureText(this.Text, TitleFont);

            using (Brush textBrush = new SolidBrush(TitleForeColor))
            {
                e.Graphics.DrawString(this.Text, TitleFont, textBrush, new PointF(10, 0));
            }

            int borderTop = textSize.Height / 2;

            using (Pen pen = new Pen(this.ForeColor))
            {
                e.Graphics.DrawLine(pen, 0, borderTop, 10, borderTop); // Trái
                e.Graphics.DrawLine(pen, 10 + textSize.Width + 5, borderTop, this.Width, borderTop); // Phải
                e.Graphics.DrawLine(pen, 0, borderTop, 0, this.Height - 1);
                e.Graphics.DrawLine(pen, this.Width - 1, borderTop, this.Width - 1, this.Height - 1);
                e.Graphics.DrawLine(pen, 0, this.Height - 1, this.Width - 1, this.Height - 1);
            }
        }
    }
}
