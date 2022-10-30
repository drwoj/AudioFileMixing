using NAudio.Wave;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioMixer
{
    class Mixer
    {
        WaveFileReader wavReader, wavReader2;
        short[] sampleBuffer, sampleBuffer2;

        public void setReader(string index, string path) {
            if(index == "b1")
                wavReader = new WaveFileReader(path);
            if (index == "b2")
                wavReader2 = new WaveFileReader(path);
        }


        public void initBuffers()
        {
            initBuffer(wavReader, sampleBuffer);
            initBuffer(wavReader2, sampleBuffer2);
        }

        private void initBuffer(WaveFileReader reader, short[] buffer)
        {
            byte[] tempBuffer = new byte[reader.Length];
            int read = reader.Read(tempBuffer, 0, buffer.Length);
            buffer = new short[read / 2];
            Buffer.BlockCopy(buffer, 0, buffer, 0, read);
        }
    }
}
