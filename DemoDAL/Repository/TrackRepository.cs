using DemoDAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoDAL.Repository
{
    public class TrackRepository
    {
        ChinookEntities _ChinookEntities;

        public TrackRepository(ChinookEntities chinookEntities)
        {
            _ChinookEntities = chinookEntities;
        }

        /// <summary>
        /// •	Créer une méthode qui renvoie une liste d’objets contenant uniquement des informations spécifiques sur des morceaux à partir d’un numéro de client 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TrackCustom GetTrackCustomById(int id)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Select Track.Name TrackName, Album.Title AlbumName, Composer, Artist.Name ArtisteName, MediaType.Name MediaTypeName FROM Track ");
            sb.Append("JOIN Album ON Album.AlbumId = Track.TrackId ");
            sb.Append("JOIN Artist ON Artist.ArtistId = Album.ArtistId ");
            sb.Append("LEFT JOIN MediaType ON MediaType.MediaTypeId = Track.MediaTypeId ");
            sb.Append("WHERE Track.TrackId = {0}");

            string query = string.Format(sb.ToString(), id);

            return _ChinookEntities.Database.SqlQuery<TrackCustom>(query).FirstOrDefault();
        }

        /// <summary>
        /// méthode qui renvoie une liste de tracks en fonction d’un compositeur
        /// </summary>
        /// <param name="composer"></param>
        /// <returns></returns>
        public List<Track> GetTrackByComposer(string composer)
        {
            return _ChinookEntities.Track.Where(x => x.Composer == composer).ToList();
        }
    }
}
