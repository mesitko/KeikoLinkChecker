using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ContentAlignment ca = (ContentAlignment)typeof(ContentAlignment).InvokeMember("BottomLeft",
                                                                         System.Reflection.BindingFlags.GetField, null,
                                                                         new ContentAlignment(), null);
            int k=0;
            k++;
        }
    }

    public enum HorizontalCrop
    {
        Center, Left, Right
    }

    public enum VerticalCrop
    {
        Middle, Top, Bottom
    }
}
