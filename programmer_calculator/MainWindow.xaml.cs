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
using HotKeySample;

namespace programmer_calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private HotKeyHelper _hotkey;
        public MainWindow()
        {
            InitializeComponent();

            this.MouseLeftButtonDown += (sender, e) => { this.DragMove(); };
            this._hotkey = new HotKeyHelper(this);
            this._hotkey.Register(ModifierKeys.Windows, Key.C, (_, __) => { SystemSounds.Beep.Play(); });
            formulaTextBox.Focus();
        }

        public void EscClose(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                SystemSounds.Beep.Play();
                Close();
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // HotKeyの登録解除
            this._hotkey.Dispose();
        }

    }
}
