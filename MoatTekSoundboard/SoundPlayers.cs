using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoatTekSoundboard
{
    class SoundPlayers
    {
        public ObservableCollection<RanjitSoundPlayer> Players { get; set; }
        public static ObservableCollection<RanjitSoundPlayer> SoundPlayersCollection;

        public SoundPlayers()
        {
            SoundPlayersCollection = new ObservableCollection<RanjitSoundPlayer>();
            foreach (int i in Enumerable.Range(0, 2))
            {
                SoundPlayersCollection.Add(new RanjitSoundPlayer(i));
            }
        }

        public static void StopAll()
        {
            foreach (int i in Enumerable.Range(0, SoundPlayersCollection.Count))
            {
                SoundPlayersCollection[i].StopPls();
            }
        }

        public static SoundPlayers InitialisePlayers()
        {
            return new SoundPlayers() { Players = SoundPlayersCollection };
        }
    }
}
