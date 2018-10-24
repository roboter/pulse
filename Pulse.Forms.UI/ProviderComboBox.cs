using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pulse.Base;

namespace Pulse.Forms.UI
{
    public class ProviderComboBox : ComboBox
    {
        public ProviderComboBox()
        {
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (this.DesignMode)
            {
                base.OnDrawItem(e); return;
            }

            e.DrawBackground();
            e.DrawFocusRectangle();

            string prov = Items[e.Index].ToString();
            Image img = ProviderManager.Instance.GetProviderIcon(prov);

            // Draw the colored 16 x 16 square
            if(img != null)
                e.Graphics.DrawImage(img, new Rectangle(e.Bounds.Left+3, e.Bounds.Top+1, 16, 16));
            // Draw the value (in this case, the color name)
            e.Graphics.DrawString(prov, e.Font, new
                    SolidBrush(e.ForeColor), e.Bounds.Left + 3 + 16, e.Bounds.Top + 2);
        

            base.OnDrawItem(e);
        }

        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            base.Invalidate();
        }
    }
}
