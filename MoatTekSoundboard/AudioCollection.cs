using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;

namespace MoatTekSoundboard
{
    class AudioCollection
    {
        public ObservableCollection<AudioClip> Clips { get; set; }
        public static ObservableCollection<AudioClip> AudioLibrary;

        public AudioCollection()
        {
            AudioLibrary = new ObservableCollection<AudioClip>();

            string folder = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\SoundFiles\";
            string filter = "*.mp3";
            string[] files = Directory.GetFiles(folder, filter);
            foreach (int i in Enumerable.Range(0, files.Length))
            {
                AudioLibrary.Add(new AudioClip(files[i]));
            }
        }

        public static AudioCollection InitialiseLibrary()
        {
            return new AudioCollection() { Clips = AudioLibrary };
        }
    }
}
