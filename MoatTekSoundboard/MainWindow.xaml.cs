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
using System.Media;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Management;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Collections;

namespace MoatTekSoundboard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private WaveOutEvent outputDevice1;
        private WaveOutEvent outputDevice2;
        private AudioFileReader audioFile1;
        private AudioFileReader audioFile2;

        public MainWindow()
        {
            InitializeComponent();
            AudioCollection Clips = AudioCollection.InitialiseLibrary();
            ButtonGrid.ItemsSource = AudioCollection.AudioLibrary;
            //SoundPlayers.CreateSoundPlayers();
            SoundPlayers.InitialisePlayers();

            List<string> Devices = DetectAudioDevices();
            Output1Combo.ItemsSource = Devices;
            Output2Combo.ItemsSource = Devices;

            outputDevice1 = new WaveOutEvent() { DeviceNumber = 1 }; ;
            outputDevice1.PlaybackStopped += OnPlaybackStoppedVBCable;
            outputDevice2 = new WaveOutEvent() { DeviceNumber = 0 }; ;
            outputDevice2.PlaybackStopped += OnPlaybackStopped;
        }

        public List<string> DetectAudioDevices()
        {
            List<string> devices = new List<string>();
            devices.Add("None");
            for (int n = -1; n < WaveOut.DeviceCount; n++)
            {
                var caps = WaveOut.GetCapabilities(n);
                devices.Add($"{n}: {caps.ProductName}");
            }
            devices.RemoveAt(1);
            return devices;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //System.Media.SoundPlayer player = new System.Media.SoundPlayer();

            //player.SoundLocation = "D:/Music/MP3/axels.wav";
            //player.Play();
            if (outputDevice2 == null)
            {
                outputDevice1 = new WaveOutEvent() { DeviceNumber = 1 }; ;
                outputDevice1.PlaybackStopped += OnPlaybackStopped;
                outputDevice2 = new WaveOutEvent() { DeviceNumber = 0 }; ;
                outputDevice2.PlaybackStopped += OnPlaybackStopped;
            }
            if (audioFile2 == null)
            {
                audioFile1 = new AudioFileReader(@"D:/Music/MP3/Lukas.mp3");
                audioFile2 = new AudioFileReader(@"D:/Music/MP3/Lukas.mp3");
                outputDevice1.Init(audioFile1);
                outputDevice2.Init(audioFile2);
            }

            //string devicesstring = "";
            //List<string> devices = new List<string>();
            //for (int n = -1; n < WaveOut.DeviceCount; n++)
            //{
            //    var caps = WaveOut.GetCapabilities(n);
            //    devices.Add($"{n}: {caps.ProductName}");
            //}

            //foreach (int i in Enumerable.Range(0, devices.Count()))
            //{
            //    devicesstring = devicesstring + devices[i] + "        ";
            //}
            //debugjit.Text = devicesstring;
            outputDevice1.Play();
            outputDevice2.Play();
        }

        private void OnPlaybackStopped(object sender, NAudio.Wave.StoppedEventArgs args)
        {
            outputDevice2.Dispose();
            audioFile2.Dispose();
        }

        private void OnPlaybackStoppedVBCable(object sender, NAudio.Wave.StoppedEventArgs args)
        {
            outputDevice1.Dispose();
            audioFile1.Dispose();
        }

        private void SoundButton_Click(object sender, RoutedEventArgs e)
        {
            //outputDevice2.Stop();
            //outputDevice1.Stop();

            var ClipIDObj = ((Button)sender).Tag;
            int ClipID = Convert.ToInt32(ClipIDObj);

            SoundPlayers.StopAll();

            //audioFile1 = new AudioFileReader(AudioCollection.AudioLibrary[ClipID].FilePath);
            //audioFile2 = new AudioFileReader(AudioCollection.AudioLibrary[ClipID].FilePath);

            //outputDevice1.Init(audioFile1);
            //outputDevice2.Init(audioFile2);

            //outputDevice1.Play();
            //outputDevice2.Play();

            foreach (int i in Enumerable.Range(0, SoundPlayers.SoundPlayersCollection.Count))
            {
                SoundPlayers.SoundPlayersCollection[i].PlayFile(AudioCollection.AudioLibrary[ClipID].FilePath);
            }
        }

        private void OutputCombo_SelectionChanged(object sender, SelectionChangedEventArgs e) // this will need to be modified to support scaling
        {
            var WhichBoxObj = ((ComboBox)sender).Tag;
            string WhichBox = WhichBoxObj.ToString();

            if (WhichBox == "1")
            {
                if (Output1Combo.SelectedItem == Output2Combo.SelectedItem)
                {
                    ChangeOutputDevice(0, Output1Combo.SelectedItem.ToString());
                    ChangeOutputDevice(1, "None");
                    Output2Combo.SelectedIndex = 0;
                    // set 1 to the device and 2 to null
                }
                ChangeOutputDevice(0, Output1Combo.SelectedItem.ToString());
            }
            if (WhichBox == "2")
            {
                if (Output1Combo.SelectedItem == Output2Combo.SelectedItem)
                {
                    ChangeOutputDevice(1, Output2Combo.SelectedItem.ToString());
                    ChangeOutputDevice(0, "None");
                    Output1Combo.SelectedIndex = 0;
                    // set 2 to the device and 1 to null
                }
                ChangeOutputDevice(1, Output2Combo.SelectedItem.ToString());
                SoundPlayers.StopAll();
            }
        }

        public void ChangeOutputDevice(int OutputNumber, string AudioDevice)
        {
            SoundPlayers.SoundPlayersCollection[OutputNumber].SwitchOutputDevice(AudioDevice);
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            try
            {
                outputDevice1.Volume = (float)VolumeSlider.Value / 100;
                outputDevice2.Volume = (float)VolumeSlider.Value / 100;
            }
            catch (NullReferenceException)
            {
                // The either of the two output devices dont have a device selected for them, it will cause a NullReferenceException.
                // This is not the best solution.

            }

        }
    }
}
