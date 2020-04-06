using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace MoatTekSoundboard
{
    class AudioClip
    {
        public int AudioClipID { get; set; }
        public string ClipName { get; set; }
        public string FilePath { get; set; }
        public SolidColorBrush BgColour { get; set; }

        public AudioClip(string Path)
        {
            AudioClipID = AudioCollection.AudioLibrary.Count;
            int LastBackslash = Path.LastIndexOf(@"\") + 1;
            ClipName = Path.Substring(LastBackslash, Path.Length - LastBackslash - 4); // (LastBackslash + 7, Path.Length - LastBackslash - 4) maybe???
            FilePath = Path;
            if (ClipName.Length > 7 && CheckIfRRGGBB(ClipName) == true)
            {
                BgColour = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#" + ClipName.Substring(0,6)));
                ClipName = ClipName.Substring(7, ClipName.Length - 7);
            }
            else
            {
                BgColour = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#d9d9d9"));
            }
        }

        public bool CheckIfRRGGBB(string FileName)
        {
            foreach (int i in Enumerable.Range(0,6))
            {
                if (!FileName.Substring(0, i).All("0123456789abcdefABCDEF".Contains))
                {
                    return false;
                }
            }
            if (FileName.Substring(6,1) == "_")
            {
                return true;
            }
            return false;
        }
    }
}
