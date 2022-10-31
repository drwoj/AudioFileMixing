using NAudio.Wave;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AudioMixer
{
    class Mixer
    {

        [DllImport(@"C:\Users\Piotr\source\repos\AudioMixer\x64\Debug\Mixing.dll")]
        static extern byte MyProc1(byte a, byte b);

        byte[] wav1, wav2;
        byte[] result;
        public byte[] Result
        {
            get => result;
        }

        
        public void initBuffer(byte[] buffer, int index)
        {
            if (index == 1)
                wav1 = buffer;
            else if (index == 2)
                wav2 = buffer;
        }
        public byte[] createWav(int algorithm)
        {
            initResult();
            setHeader();
            mixAudio(algorithm);
            return result;
        }
        private byte mixSamples(byte a, byte b){
            return (byte)(a + b);
        }
        private void MixAudioC(int size)
        {

            for (int i = 44; i < size; ++i)
                result[i] = mixSamples(wav1[i], wav2[i]);
        }
        private void MixAudioAsm(int size)
        {

            for (int i = 44; i < size; ++i)
                result[i] = MyProc1(wav1[i], wav2[i]);

        }
        private void mixAudio(int algorithm)
        {
            int sizeSmallerWav = wav1.Length;
            byte[] biggerWav = wav2;

            if (isFirstWavBigger())
            {
                sizeSmallerWav = wav2.Length;
                biggerWav = wav1;
            }

            if (algorithm == 1)
                MixAudioC(sizeSmallerWav);
            else if (algorithm == 2)
                MixAudioAsm(sizeSmallerWav);

            for (int i = sizeSmallerWav; i < result.Length; ++i)
                result[i] = biggerWav[i];
        }
        private byte[] setHeader()
        {
            byte[] header = new byte[44];
            if (isFirstWavBigger())
                for (int i = 0; i < 44; ++i)
                    result[i] = wav1[i];
            else
                for (int i = 0; i < 44; ++i)
                    result[i] = wav2[i];

            return header;
        }
        private void initResult()
        {
            int resultSize = isFirstWavBigger() ? wav1.Length : wav2.Length;
            result = new byte[resultSize];
        }
        private bool isFirstWavBigger()
        {
            return wav1.Length > wav2.Length;
        }
    }
    
}

