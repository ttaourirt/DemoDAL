using DemoDAL;
using DemoDAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace TestDemoDAL
{
    [TestClass]
    public class TestTrack
    {
        ChinookEntities _ChinookEntities;
        TrackRepository _TrackRepo;

        public TestTrack()
        {
            _ChinookEntities = new ChinookEntities();
            _TrackRepo = new TrackRepository(_ChinookEntities);
        }


        /// <summary>
        /// •	Créer une méthode qui renvoie une liste d’objets contenant uniquement des informations spécifiques sur des morceaux à partir d’un numéro de client 
        /// </summary>
        [TestMethod]
        public void TestGetTrackCustomById()
        {
            var track = _ChinookEntities.Track.FirstOrDefault();

            var trackcustom = _TrackRepo.GetTrackCustomById(track.TrackId);
        }
    }
}
