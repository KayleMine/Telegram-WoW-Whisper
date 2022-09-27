using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WowPixel
{
    public partial class tgs : Form
    {
        public tgs()
        {
            InitializeComponent();
            // -- round corners|shadows -- //
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
            // -- end -- //

            EO.WebBrowser.Runtime.AddLicense("6A+frfD09uihfsay4Q/lW5f69h3youbyzs2xaqW0s8uud7Pl9Q+frfD09uihfsay6BvlW5f69h3youbyzs2xaaW0s8uud7Pl9Q+frfD09uihfsay6BvlW5f69h3youbyzs2xaqW0s8uud7Oz8hfrqO7CzRrxndz22hnlqJfo8h/kdpm1wNyuaae0ws2frOzm1iPvounpBOzzdpm1wNyucrC9ys2fr9z2BBTup7Smw82faLXABBTmp9j4Bh3kd+T20tbFiajL4fPRoenW2RX4ksbS4hK8drOzBBTmp9j4Bh3kd7Oz/RTinuX39ul14+30EO2s3MLNF+ic3PIEEMidtbXE3rZ1pvD6DuSn6unaD7112PD9GvZ3s+X1D5+t8PT26KF+xrLUE/Go5Omzy/We6ff6Gu12mbbB2a9bl7PP5+Cd26QFJO+etKbW+q183/YAGORbl/r2HfKi5vLOzbFqpbSzy653s+X1D5+t8PT26KF+xrLoEOFbl/r2HfKi5vLOzbFppbSzy653s+X1D5+t8PT26KF+xrLoEOFbl/r2HfKi5vLOzbFqpbSzy653s+X1D5+t8PT26KF+xrLhD+Vbl/r2HfKi5vLOzbFppbSzy653s+X1");
            //P.s key found in google free to use xD

            webView1.Url = "https://kaylemine.github.io/KayleMine//";
                         // WowPixel-master/source/WowPixel/chatid.html <- it's here.

            if (Properties.Settings.Default.apikey != "" | Properties.Settings.Default.apikey != null | Properties.Settings.Default.apikey != " ")
            { textBox1.Text = Properties.Settings.Default.apikey;}   // if saved, set txtbox
            if (Properties.Settings.Default.chatids != "" | Properties.Settings.Default.chatids != null | Properties.Settings.Default.chatids != " ")
            { textBox2.Text = Properties.Settings.Default.chatids; } // if saved, set txtbox

            if (Properties.Settings.Default.chatids == "" | Properties.Settings.Default.chatids == null | Properties.Settings.Default.chatids == " ")
            { textBox2.Text = "Chat ID"; }  // if not
            if (Properties.Settings.Default.apikey == "" | Properties.Settings.Default.apikey == null | Properties.Settings.Default.apikey == " ")
            { textBox1.Text = "Bot Token"; }// if not

        }

        // -- round corners|shadows -- //
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
        private const int CS_DropShadow = 0x00020000;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle = CS_DropShadow;
                return cp;
            }
        }
        // -- end -- //


        private void button1_Click(object sender, EventArgs e) // bye (exit)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) // lit. save button
        {
            Properties.Settings.Default.chatids = textBox2.Text;
            Properties.Settings.Default.Save();
            Properties.Settings.Default.apikey = textBox1.Text;
            Properties.Settings.Default.Save();
        }

    }
}
