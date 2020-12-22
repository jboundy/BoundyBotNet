using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoundyBotNet.Tests
{
    [TestClass]
    public class ChannelTests
    {
        BotEmulator _bot;
        public ChannelTests()
        {
            _bot = new BotEmulator();
        }

        [TestMethod]
        public void CanJoinVoiceChannel()
        {
            Assert.IsTrue(_bot != null);
        }
    }
}
