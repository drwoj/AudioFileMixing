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
using Amib.Threading;
using System.Text.RegularExpressions;
using System.IO;

namespace AudioMixer
{
    public partial class MainWindow : Window
    {
        int threads = 1;
        Mixer mixer = new Mixer();
        SmartThreadPool stp = new SmartThreadPool();

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
                byte[] buffer = new byte[44];
                btn.Content = System.IO.Path.GetFileNameWithoutExtension(ofd.FileName);
                buffer = File.ReadAllBytes(ofd.FileName);
                mixer.initBuffer(buffer, int.Parse(btn.Uid));
      
            }
        }

        private void btnMixFiles_Click(object sender, RoutedEventArgs e)
        {
            int alg = (bool)algC.IsChecked ? 1 : 0;
            byte[] result = mixer.createWav(alg);
        }

        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "wav files (*.wav)|*.wav";
            sfd.DefaultExt = "wav";
            sfd.FileName = "MixedFile";

            if (sfd.ShowDialog() == true)
                File.WriteAllBytes(sfd.FileName, mixer.Result);
            
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            textThreads.Text = slider.Value.ToString();
            threads = int.Parse(slider.Value.ToString());
        }

    }
}