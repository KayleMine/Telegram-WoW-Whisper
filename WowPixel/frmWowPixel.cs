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
using Siticone.UI.WinForms;
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

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;


        // --- move app --- //
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        private static readonly ITelegramBotClient bot = new TelegramBotClient(Properties.Settings.Default.apikey);

 
        string Get;
        string path = Directory.GetCurrentDirectory();
        private void LogDebug(string message)
        {

            string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            AppendTextToRichTextBox(logMessage);
        }

        private void AppendTextToRichTextBox(string text)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(new Action(() => AppendTextToRichTextBox(text)));
            }
            else
            {
                richTextBox1.AppendText(text + Environment.NewLine);
                richTextBox1.ScrollToCaret();
            }
        }

        public frmWowPixel()
        {
            InitializeComponent();
            // -- shadows|borders -- // 
            InitializeFormAppearance();
            // run tgbot 18.00 ...
            StartTelegramBot();

        }
        private void InitializeFormAppearance()
        {
            new SiticoneShadowForm(this);
            new SiticoneDragControl(this);
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 15, 15));
        }
        private void StartTelegramBot()
        {
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions { AllowedUpdates = { } };

            bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiverOptions, cancellationToken);
        }
        async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken) // an error...
        {
            if (exception is ApiRequestException apiRequestException)
            {
                await botClient.SendTextMessageAsync(123, apiRequestException.ToString());
            }
        }

        public static Color GetColorAtTopLeft()
        {
            IntPtr screenDC = GetDC(IntPtr.Zero);

            int screenWidth = GetSystemMetrics(SM_CXSCREEN);
            int screenHeight = GetSystemMetrics(SM_CYSCREEN);

            int a = (int)GetPixel(screenDC, 0, 0);

            ReleaseDC(IntPtr.Zero, screenDC);

            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }

        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        async Task<Task> HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (!EnableKey.Checked)
                return Task.CompletedTask;

            var updates = await bot.GetUpdatesAsync();

            foreach (var update2 in updates)
            {
                if (!update2.Message.Text.Contains("/rep "))
                    continue;

                int lastMessageId = update2.Message.MessageId;

                IntPtr wowWindowHandle = await WaitForWowWindowAsync();

                if (wowWindowHandle != IntPtr.Zero)
                {
                    SetForegroundWindow(wowWindowHandle);

                    string orgText = update2.Message.Text;
                    string phrase = orgText.Substring(orgText.IndexOf("/rep ") + 5);
                    LogDebug(orgText+" :: My telegram message");
                    LogDebug(phrase + " :: phrase it self");
                    SendKeys.SendWait("{ENTER}");
                    SendKeys.SendWait(phrase);
                    SendKeys.SendWait("{ENTER}");
                    SendKeys.Flush();

                    await bot.SendTextMessageAsync(Settings.Default.chatids, "Replied with: " + phrase);
                    LogDebug("Reply happen");
                    await bot.DeleteMessageAsync(Settings.Default.chatids, lastMessageId);
                    LogDebug("Reply message deleted");
                    break;
                }
            }

            return Task.CompletedTask;
        }
        async Task<IntPtr> WaitForWowWindowAsync()
        {
            IntPtr wowWindowHandle = IntPtr.Zero;

            for (int i = 0; i < 60; i++)
            {
                await Task.Delay(500);
                wowWindowHandle = FindWindow(null, "World Of Warcraft");

                if (wowWindowHandle != IntPtr.Zero)
                {
                    if (!IsWindowInFocus(wowWindowHandle))
                    {
                        LogDebug("Wow window is set in focus.");
                        SetForegroundWindow(wowWindowHandle);
                    }
                    else
                    {
                        LogDebug("Wow window is already in focus.");
                        await Task.Delay(1500); // Wait an additional 1.5 seconds
                        break;
                    }
                }
            }

            return wowWindowHandle;
        }

        bool IsWindowInFocus(IntPtr windowHandle)
        {
            IntPtr focusedWindowHandle = GetForegroundWindow();
            return focusedWindowHandle == windowHandle;
        }

        private async void tmrFire_Tick(object sender, EventArgs e) // ticker
        {
            var rect = Settings.Default.rectanlge;
            if (EnableKey.Checked) // aka enable
            {
                textBox1.Text = "Enabled";
                Get = HexConverter(GetColorAtTopLeft());

                if (Get == "#FB0000" || Get == "#FF0000")
                {
                    LogDebug("Found whishper.");
                    Thread.Sleep(2500);
                    // Assuming rect is properly defined
                    using (Bitmap bitmap2 = new Bitmap(rect.Width, rect.Height))
                    {
                        using (Graphics graphics2 = Graphics.FromImage(bitmap2))
                        {
                            LogDebug("Making screenshot.");
                            graphics2.CopyFromScreen(rect.Location, Point.Empty, rect.Size, CopyPixelOperation.SourceCopy);
                        }
                        LogDebug("Saving screenshot.");
                        bitmap2.Save("WowSpotted.Png", ImageFormat.Png);
                    }

                    await Task.Delay(150);

                    using (var stream2 = System.IO.File.Open("WowSpotted.Png", FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        LogDebug("Sending screenshot.");
                        await bot.SendPhotoAsync(Settings.Default.chatids, stream2, "Wishper spotted!");
                    }
                }


                if (Get == "#FFFF00") // gm or dev in addon
                {
                    LogDebug("Found GM\\Dev message.");
                    Thread.Sleep(5500);
                    using (Bitmap bitmap2 = new Bitmap(rect.Width, rect.Height))
                    {
                        using (Graphics graphics2 = Graphics.FromImage(bitmap2))
                        {
                            LogDebug("Making screenshot.");
                            graphics2.CopyFromScreen(rect.Location, Point.Empty, rect.Size, CopyPixelOperation.SourceCopy);
                        }
                        LogDebug("Saving screenshot.");
                        bitmap2.Save("WowSpotted.Png", ImageFormat.Png);
                    }
                    await Task.Delay(150);

                    using (var stream2 = System.IO.File.Open(path + "\\WowSpotted.Png", FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        LogDebug("Sending screenshot.");
                        await bot.SendPhotoAsync(Settings.Default.chatids, stream2, "GM OR DEV: Wishper spotted!");
                    }

                }
            }
            else
            {
                textBox1.Text = "Disabled";
            }
        }
 
 
        private void chkFireTriggerKey_CheckedChanged(object sender, EventArgs e) { } // trash
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

        private void button3_Click(object sender, EventArgs e)
        {
            tgs f2 = new tgs();
            f2.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) //exit
        {
            this.Close();
        }
    }
}

