using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;
using System.Windows.Forms;
//using System.Media.Brush;
using System.Drawing;

namespace Diplomaster
{
    static class OwnTreeView
    {
        public const int TVIF_STATE = 0x8;
        public const int TVIS_STATEIMAGEMASK = 0xF000;
        public const int TV_FIRST = 0x1100;
        public const int TVM_SETITEM = TV_FIRST + 63;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        public struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            //public bool check;
            //[MarshalAs(UnmanagedType.LPTStr)]
            //public String lpszText;
            //public int cchTextMax;
            //public int iImage;
            //public int iSelectedImage;
            //public int cChildren;
            //public IntPtr lParam;
        }

        public static void Initialize(TreeView tree)
        {
            tree.CheckBoxes = true;
            tree.DrawMode = TreeViewDrawMode.OwnerDrawText;
            tree.DrawNode += new DrawTreeNodeEventHandler(tree_DrawNode);
        }

        static void tree_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (((OwnTreeNode)e.Node).hideCheck)
            {
                HideCheckBox(e.Node);
                //e.DrawDefault = true;
                //SolidBrush br = new SolidBrush(Color.Gray);
                Font font = new Font(e.Node.TreeView.Font, FontStyle.Bold);
                //e.Graphics.DrawString(e.Node.Text, font, br, e.Node.Bounds.X, e.Node.Bounds.Y);//Brushes.Black
                TextRenderer.DrawText(e.Graphics, e.Node.Text, font, e.Node.Bounds, Color.Gray);
            }
            else
            {
                //e.DrawDefault = true;
                SolidBrush br = new SolidBrush(Color.Black);
                e.Graphics.DrawString(e.Node.Text, e.Node.TreeView.Font, br, e.Node.Bounds.X, e.Node.Bounds.Y);//Brushes.Black
            }
        }

        private static void HideCheckBox(TreeNode node)
        {
            TVITEM tvi = new TVITEM();
            tvi.hItem = node.Handle;
            tvi.mask = TVIF_STATE;
            tvi.stateMask = TVIS_STATEIMAGEMASK;
            tvi.state = 0;
            IntPtr lparam = Marshal.AllocHGlobal(Marshal.SizeOf(tvi));
            Marshal.StructureToPtr(tvi, lparam, false);
            SendMessage(node.TreeView.Handle, TVM_SETITEM, IntPtr.Zero, lparam);
        }
    }

    public class OwnTreeNode : TreeNode
    {
        public bool hideCheck { get; set; }

        public OwnTreeNode() {
            hideCheck = false;
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
