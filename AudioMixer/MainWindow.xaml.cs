using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace AudioMixer
{
    public partial class MainWindow : Window
    {

        [DllImport(@"C:\Users\Piotr\source\repos\AudioMixer\x64\Debug\Mixing.dll")]
        static extern int MyProc1(int a, int b);
            Mixer mixer = new Mixer();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Wav Files|*.wav";            
            Button btn = sender as Button;

            if (ofd.ShowDialog() == true)
            {
                btn.Content = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                mixer.setReader(btn.Name, ofd.FileName);
            }
        }

        private void btnMixFiles_Click(object sender, RoutedEventArgs e)
        {
            mixer.initBuffers();
        }
    }
}