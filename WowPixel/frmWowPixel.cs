using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using WowPixel.Properties;

namespace WowPixel
{


    public partial class frmWowPixel : Form
    {
        [DllImport("user32.dll", EntryPoint = "GetDesktopWindow")]
        private static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("gdi32.dll")]
        static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);
        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);


        private static readonly ITelegramBotClient bot = new TelegramBotClient(Properties.Settings.Default.apikey);
        public frmWowPixel()
        {
            InitializeComponent();
            // -- shadows|borders -- // 
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
            // -- end -- // 

            // run tgbot 18.00 ...
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // receive all update types
            };
            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
         );

        }

        async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken) // an error...
        {
            if (exception is ApiRequestException apiRequestException)
            {
                await botClient.SendTextMessageAsync(123, apiRequestException.ToString());
            }
        }

        // -- shadows.... -- //
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

        string Get;
        public static Color GetColorAt(int x, int y)
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr dc = GetWindowDC(desk);
            int a = (int)GetPixel(dc, x, y);
            ReleaseDC(desk, dc);
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }
        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }
        string path = Directory.GetCurrentDirectory();

        async Task<Task> HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken) // tg updater
        {
            if (chkFireTriggerKey.Checked)
            { 
            var updates = await bot.GetUpdatesAsync();
            foreach (var update2 in updates)
            {
                                                            // MessageBox.Show("Bot: " + update2.Message.Text);
                string prefix = "/rep ";
                int lastMessageId = 1;
                lastMessageId = update2.Message.MessageId;

                if  (update2.Message.Text.Contains(prefix)) // if sended message contain prefix
                    {
                       
                                IntPtr zero = IntPtr.Zero;
                                for (int i2 = 0; (i2 < 60) && (zero == IntPtr.Zero); i2++)
                                {
                                    Thread.Sleep(500);
                                    zero = FindWindow(null, "World Of Warcraft");
                                }

                                if (zero != IntPtr.Zero) // if wow open....
                                {
                                    SetForegroundWindow(zero);
                                // -- remove anything before prefix (123/rep => /rep) -- //
                                string var = "";
                                String orgText = update2.Message.Text;
                                int i = orgText.IndexOf(prefix);
                                    if (i != -1)
                                        {
                                            var = orgText.Remove(0, i);
                                        }
                                // -- end -- //
                                string phrase = var.Replace("/rep ", ""); //remove thoose prefix
                                    SendKeys.SendWait("{ENTER}");
                                    SendKeys.SendWait(phrase);
                                    SendKeys.SendWait("{ENTER}");
                                    SendKeys.Flush();
                                await bot.SendTextMessageAsync(Settings.Default.chatids, "Replyed with: "+phrase); // let user know what he send.
                                bot.DeleteMessageAsync(Settings.Default.chatids, lastMessageId); // delete command
                                break;
                                }
                    }
            }
            }
            return Task.CompletedTask;
        }

        private async void tmrFire_Tick(object sender, EventArgs e) // ticker
        {
            var rect = Settings.Default.rectanlge;
            if (chkFireTriggerKey.Checked) // aka enable
            {
                textBox1.Text = "Enabled";
                Get = HexConverter(GetColorAt(0, 0));
                if (Get == "#FF0000")  // any whishper in addon
                {
                    Thread.Sleep(2500);
                    using (Bitmap bitmap2 = new Bitmap(rect.Width, rect.Height))
                    {
                        using (Graphics graphics2 = Graphics.FromImage(bitmap2))
                        {
                            graphics2.CopyFromScreen(rect.Location, Point.Empty, rect.Size, CopyPixelOperation.SourceCopy);
                        }
                        bitmap2.Save("WowSpotted.Png", ImageFormat.Png); // save around app exe
                    }
                    await Task.Delay(150);

                    using (var stream2 = System.IO.File.Open(path + "\\WowSpotted.Png", FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        await bot.SendPhotoAsync(Settings.Default.chatids, stream2, "Wishper spotted!");
                    }
                }

                if (Get == "#FFFF00") // gm or dev in addon
                {
                    Thread.Sleep(5500);
                    using (Bitmap bitmap2 = new Bitmap(rect.Width, rect.Height))
                    {
                        using (Graphics graphics2 = Graphics.FromImage(bitmap2))
                        {
                            graphics2.CopyFromScreen(rect.Location, Point.Empty, rect.Size, CopyPixelOperation.SourceCopy);
                        }
                        bitmap2.Save("WowSpotted.Png", ImageFormat.Png);  // save around app exe
                    }
                    await Task.Delay(150);

                    using (var stream2 = System.IO.File.Open(path + "\\WowSpotted.Png", FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        await bot.SendPhotoAsync(Settings.Default.chatids, stream2, "GM OR DEV: Wishper spotted, killing WoW!");
                    }
                    foreach (var process in Process.GetProcessesByName("WoW"))
                    {
                        process.Kill(); // bonk
                    }
                }
       

            }
            else
            {
                textBox1.Text = "Disabled";
            }

        }




        private void button1_Click(object sender, EventArgs e) //exit
        {
            this.Close();
        }


        // --- move app --- //
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void frmWowPixel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        // --- end --- //


        private void chkFireTriggerKey_CheckedChanged(object sender, EventArgs e) { } // trash


        // --- some rect. painter i found on stackoverflow. (idk about link if u need search: Draw rectangle with mouse)  --- //
        Form form2;
        Point MD = Point.Empty;
        Rectangle rect = Rectangle.Empty;
        private void button2_Click(object sender, EventArgs e) // rect button
        {
            Hide();
            form2 = new Form();
            form2.BackColor = Color.Wheat;
            form2.TransparencyKey = form2.BackColor;
            form2.ControlBox = false;
            form2.MaximizeBox = false;
            form2.MinimizeBox = false;
            form2.FormBorderStyle = FormBorderStyle.None;
            form2.WindowState = FormWindowState.Maximized;
            form2.MouseDown += form2_MouseDown;
            form2.MouseMove += form2_MouseMove;
            form2.Paint += form2_Paint;
            form2.MouseUp += form2_MouseUp;

            form2.Show();
        }

        void form2_MouseDown(object sender, MouseEventArgs e)
        {
            MD = e.Location;
        }

        void form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            Point MM = e.Location;
            rect = new Rectangle(Math.Min(MD.X, MM.X), Math.Min(MD.Y, MM.Y),
                                 Math.Abs(MD.X - MM.X), Math.Abs(MD.Y - MM.Y));
            form2.Invalidate();
        }

        void form2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Red, rect);
        }

        void form2_MouseUp(object sender, MouseEventArgs e)
        {
            form2.Hide();
            Properties.Settings.Default.rectanlge = rect;
            Properties.Settings.Default.Save();
            form2.Close();
            Show();
        }
        public static bool TryParsePoint(string s, out System.Drawing.Point p)
        {
            p = new System.Drawing.Point();
            string s1 = "{X=";
            string s2 = ",Y=";
            string s3 = "}";
            int x1 = s.IndexOf(s1, StringComparison.OrdinalIgnoreCase);
            int x2 = s.IndexOf(s2, StringComparison.OrdinalIgnoreCase);
            int x3 = s.IndexOf(s3, StringComparison.OrdinalIgnoreCase);
            if (x1 < 0 || x1 >= x2 || x2 >= x3) { return false; }
            s1 = s.Substring(x1 + s1.Length, x2 - x1 - s1.Length);
            s2 = s.Substring(x2 + s2.Length, x3 - x2 - s2.Length); int i = 0;
            if (Int32.TryParse(s1, out i) == false) { return false; }
            p.X = i;
            if (Int32.TryParse(s2, out i) == false) { return false; }
            p.Y = i;
            return true;
        }
        // --- end --- //

        // -- open eo browser -- //
        private void button3_Click(object sender, EventArgs e)
        {
            tgs f2 = new tgs();
            f2.ShowDialog();
        }
       // -- end -- //


        // --- round app corner --- //
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
        // -- end -- //
    }
}

