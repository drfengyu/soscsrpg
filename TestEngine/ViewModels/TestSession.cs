using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Engine.ViewModels;
using RestSharp;
namespace TestEngine.ViewModels
{
    [TestClass]
    public class TestSession
    {
        [TestMethod]
        public void TestCreateSession()
        {
           Session session = new Session();
            Assert.IsNotNull(session.CurrentPlayer);
            Assert.AreEqual("Town square",session.CurrentLocation.Name);

        }

        [TestMethod]
        public void TestPlayerMovesHomeAndIsCompletelyHealedOnkilled() {
            Session session = new Session();
            session.CurrentPlayer.TakeDamage(999);
            Assert.AreEqual("Home",session.CurrentLocation.Name);
            Assert.AreEqual(session.CurrentPlayer.Level*10,session.CurrentPlayer.CurrentHitPoints);
        }
    }
}
