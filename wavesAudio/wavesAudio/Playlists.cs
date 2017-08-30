using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wavesAudio
{
    class Playlists
    {
        public List<Songs> newPlaylist = new List<Songs>();
        public int songCount { get; set; }// for next version;
        public string name { get; set; }
    }
}
