using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;

namespace programmer_calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int WM_HOTKEY = 0x0312;

        public MainWindow()
        {
            InitializeComponent();

            this.MouseLeftButtonDown += (sender, e) => { this.DragMove(); };
            Loaded += MainWindow_Loaded;
        }

        public void EscClose(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                SystemSounds.Beep.Play();
                Close();
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // ウィンドウハンドルを取得
            IntPtr hwnd = new WindowInteropHelper(this).Handle;

            // ホットキーを登録 (MOD_CONTROL = 0x0002, MOD_SHIFT = 0x0004)
            RegisterHotKey(hwnd, 1, 0x0006, KeyInterop.VirtualKeyFromKey(Key.C));
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // メッセージループを監視してホットキーを処理
            HwndSource source = HwndSource.FromHwnd(new WindowInteropHelper(this).Handle);
            source.AddHook(WndProc);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY)
            {
                int hotkeyId = wParam.ToInt32();

                // ホットキーが押されたときの処理をここに追加
                if (hotkeyId == 1)
                {
                    // この部分にアプリケーションの起動処理を記述
                    SystemSounds.Beep.Play();
                }
            }
            return IntPtr.Zero;
        }

        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, int vk);

        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    }
}
