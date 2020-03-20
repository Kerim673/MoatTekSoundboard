using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoatTekSoundboard
{
    class AudioClip
    {
        public int AudioClipID { get; set; }
        public string ClipName { get; set; }
        public string FilePath { get; set; }

        public AudioClip(string Path)
        {
            AudioClipID = AudioCollection.AudioLibrary.Count;
            int LastBackslash = Path.LastIndexOf(@"\") + 1;
            ClipName = Path.Substring(LastBackslash, Path.Length - LastBackslash - 4);
            FilePath = Path;
        }
    }
}
