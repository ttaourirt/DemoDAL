using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDAL.Repository
{
    public class PlaylistRepository
    {
        ChinookEntities _ChinookEntities;

        public PlaylistRepository(ChinookEntities chinookEntities)
        {
            _ChinookEntities = chinookEntities;
        }

        public void AddPlaylist(Playlist playlist)
        {
            if (playlist.PlaylistId == 0)
                playlist.PlaylistId = _ChinookEntities.Playlist.Max(x => x.PlaylistId) + 1;

            _ChinookEntities.Playlist.Add(playlist);
        }

        public void Delete(Playlist playlist)
        {
            _ChinookEntities.Playlist.Remove(playlist);
        }
    }
}
