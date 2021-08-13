using System.Drawing;
using System.Windows.Forms;

namespace HomeCinema
{
    class ToolStripRender : ToolStripProfessionalRenderer
    {
        protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
        {
            if (!e.Item.Selected)
            {
                base.OnRenderButtonBackground(e);
            }
            else
            {
                Rectangle rectangle = new Rectangle(0, 0, e.Item.Size.Width - 1, e.Item.Size.Height - 1);
                e.Graphics.FillRectangle(Brushes.DarkCyan, rectangle);
                e.Graphics.DrawRectangle(Pens.Olive, rectangle);
            }
        }
    }
}
