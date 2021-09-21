using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeCinema
{
    public static class Themes
    {
        public static void SetTheme(Form parent, List<Control> ctrls)
        {
            // Form properties
            parent.BackColor = Settings.ColorBg;
            parent.ForeColor = Settings.ColorFont;
            // Controls
            foreach (Control item in ctrls)
            {
                item.BackColor = Settings.ColorBg;
                item.ForeColor = Settings.ColorFont;
            }
        }
        public static void SetTheme(Form parent)
        {
            SetTheme(parent, new List<Control>() { });
        }
        public static void SetThemeAndBtns(Form parent, List<Control> ctrls)
        {
            // Form properties
            var listBtn = new List<Button>();
            parent.BackColor = Settings.ColorBg;
            parent.ForeColor = Settings.ColorFont;
            // Controls
            foreach (Control item in ctrls)
            {
                if (item is Button)
                {
                    listBtn.Add((Button)item);
                }
                else if (!(item is TextBox))
                {
                    item.BackColor = Settings.ColorBg;
                    item.ForeColor = Settings.ColorFont;
                }
                else { }
            }
            SetButtons(listBtn);
        }
        public static void SetThemeAndBtns(Form parent, Control.ControlCollection coll)
        {
            var list = new List<Control>();
            foreach (Control item in coll)
            {
                list.Add(item);
            }
            SetThemeAndBtns(parent, list);
        }
        public static void SetButtons(List<Button> btns)
        {
            foreach (Button item in btns)
            {
                item.BackColor = Color.DarkGray;
                item.ForeColor = Color.Black;
            }
        }
    }
}
