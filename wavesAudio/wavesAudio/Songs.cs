using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wavesAudio
{
    class Songs
    {
        public string songName { get; set; }
        public string artist { get; set; }
        public string duration { get; set; }
        public string album { get; set; }
        public string path { get; set; }

        public Songs(string dSongName, string dArtist,string dDuration, string dAlbum, string dpath )
        {
            songName = dSongName;
            artist = dArtist;
            duration = dDuration;
            album = dAlbum;
            path = dpath;
        }
    }
}
