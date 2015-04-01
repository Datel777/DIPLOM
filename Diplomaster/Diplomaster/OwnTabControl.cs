using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;

namespace Diplomaster
{
    class OwnTabControl
    {
        public static void Initialize(TabControl tabcontrol)
        {
            tabcontrol.DrawMode = TabDrawMode.OwnerDrawFixed;
            tabcontrol.DrawItem += new DrawItemEventHandler(tabControl_DrawItem);
        }
        
        private static void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            TabControl tabcontrol = (TabControl)sender;
            OwnTabPage page = (OwnTabPage)tabcontrol.TabPages[e.Index];
            e.Graphics.FillRectangle(new SolidBrush(page.TabColor), e.Bounds);

            Rectangle paddedBounds = e.Bounds;
            int yOffset = (e.State == DrawItemState.Selected) ? -2 : 1;
            paddedBounds.Offset(1, yOffset);
            TextRenderer.DrawText(e.Graphics, page.Text, page.Font, paddedBounds, page.ForeColor);
        }
    }

    class OwnTabPage : TabPage
    {
        public Color TabColor { get; set; }

        public OwnTabPage()
        {
            TabColor = BackColor;
        }

        public void ResetTabColor()
        {
            TabColor = BackColor;
        }

        //public OwnTreeNode(bool b)
        //{
        //    hideCheck = b;
        //}
        //public OwnTreeNode(string str)
        //{
        //    Text = str;
        //    hideCheck = false;
        //}
        //public OwnTreeNode(string str, bool b)
        //{
        //    Text = str;
        //    hideCheck = b;
        //}
    }
}
