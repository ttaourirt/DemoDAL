using DemoDAL;
using DemoDAL.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestDemoDAL
{
    /// <summary>
    /// Description résumée pour TestArtist
    /// </summary>
    [TestClass]
    public class TestArtist
    {
        ChinookEntities _ChinookEntities;
        ArtisteRepository _ArtistRepo;

        public TestArtist()
        {
            _ChinookEntities = new ChinookEntities();
            _ArtistRepo = new ArtisteRepository(_ChinookEntities);
        }

        [TestMethod]
        public void TestGetNbAlbumsArtiste()
        {
            var result = _ArtistRepo.GetNbAlbumsArtiste();
        }
    }
}
