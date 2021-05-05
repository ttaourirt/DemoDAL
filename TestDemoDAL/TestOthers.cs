using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestDemoDAL
{
    [TestClass]
    public class TestOthers
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger("MyLogger");

        [TestMethod]
        public void TestException()
        {
            try
            {
                throw new NotImplementedException();
            }
            catch(Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
