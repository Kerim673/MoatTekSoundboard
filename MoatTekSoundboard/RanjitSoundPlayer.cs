using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave.SampleProviders;
using NAudio.Wave;

namespace MoatTekSoundboard
{
    class RanjitSoundPlayer
    {
        public int PlayerID { get; set; }
        public WaveOutEvent OutputDevice { get; set; }
        public AudioFileReader AudioFile { get; set; }

        public RanjitSoundPlayer(int id)
        {
            PlayerID = id;
            OutputDevice = null;
            AudioFile = null;
        }

        public void SwitchOutputDevice(string AudioDevice)
        {
            if (OutputDevice != null)
            {
                OutputDevice.Stop();
            }
            if (AudioDevice == "None")
            {
                //OutputDevice = null;
            }
            else
            {
                OutputDevice = new WaveOutEvent() { DeviceNumber = Convert.ToInt32(AudioDevice.Substring(0, 1)) }; ;
                OutputDevice.PlaybackStopped += OnPlaybackStopped;
            }

        }

        public void StopPls()
        {
            if (OutputDevice != null)
            {
                OutputDevice.Stop();
            }
        }

        public void PlayFile(string FilePath)
        {
            if (OutputDevice != null)
            {
                AudioFile = new AudioFileReader(FilePath);
                OutputDevice.Init(AudioFile);
                //OutputDevice.Volume = 0.1F;
                OutputDevice.Play();
            }

        }

        private void OnPlaybackStopped(object sender, NAudio.Wave.StoppedEventArgs args)
        {
            if (OutputDevice != null)
            {
                OutputDevice.Dispose();
                AudioFile.Dispose(); // crashes when it the first
                //
                //                      ^^ 
                //                      What tf does this even mean?? Leave better comments please.
            }

        }
    }
}
