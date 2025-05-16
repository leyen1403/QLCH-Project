using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCH.GUI.UserControls
{
    public class UserButton : Button
    {
        private bool isSelected;
        public UserButton()
        {
            this.Anchor = AnchorStyles.None;
            this.BackColor = Color.White;
            this.Cursor = Cursors.Hand;
            this.FlatStyle = FlatStyle.Flat;
            this.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            this.ImageAlign = ContentAlignment.MiddleLeft;
            this.Location = new Point(0, 140);
            this.Margin = new Padding(0, 3, 0, 3);
            this.Name = "customButton";
            this.Size = new Size(290, 57);
            this.TabIndex = 0;
            this.Text = "Button";
            this.UseVisualStyleBackColor = false;

            this.FlatAppearance.BorderColor = Color.Black;
            this.FlatAppearance.BorderSize = 0;
            this.FlatAppearance.MouseDownBackColor = Color.FromArgb(124, 151, 221);
            this.FlatAppearance.MouseOverBackColor = Color.FromArgb(233, 239, 255);
            this.Click += (s, e) => ToggleSelection();
        }

        private void ToggleSelection()
        {
            IsSelected = !IsSelected;
        }
        public string ButtonText
        {
            get => this.Text;
            set => this.Text = value;
        }
        public EventHandler OnClick
        {
            set => this.Click += value;
        }
        public bool IsSelected
        {
            get => isSelected;
            set
            {
                isSelected = value;
                UpdateButtonState();
            }
        }
        private void UpdateButtonState()
        {
            this.BackColor = IsSelected ? Color.FromArgb(124, 151, 221) : Color.White;
            this.ForeColor = IsSelected ? Color.White : Color.Black;
        }

    }
}
