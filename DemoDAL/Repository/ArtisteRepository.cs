using DemoDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDAL.Repository
{
    public class ArtisteRepository
    {
        ChinookEntities _ChinookEntities;

        public ArtisteRepository(ChinookEntities chinookEntities)
        {
            _ChinookEntities = chinookEntities;
        }

        /// <summary>
        /// Retourne la liste des artistes et leur nombre d'albums
        /// </summary>
        /// <returns></returns>
        public List<ArtisteNbAlbums> GetNbAlbumsArtiste()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select T1.NbAlbums, T2.* from(SELECT Artist.ArtistId, count(Album.AlbumId) as NbAlbums ");
            sb.Append("FROM[Chinook].[dbo].[Artist] ");
            sb.Append("JOIN Album On Artist.ArtistId = Album.ArtistId ");
            sb.Append("Group by Artist.ArtistId) T1 JOIN Artist T2 ON T1.ArtistId = T2.ArtistId; ");
            
            return _ChinookEntities.Database.SqlQuery<ArtisteNbAlbums>(sb.ToString()).ToList();
        }
    }
}
