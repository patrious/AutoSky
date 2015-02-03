using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoSky.TestFolder
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void DirectionCommandTest()
        {
            var direction = AutoSky.UDLR.Up;
            var command = string.Format(CommandStaticStrings.StartMoveCommand, (int)direction);
            Assert.AreEqual("DIR_SET:1;", command);

            direction = AutoSky.UDLR.Down;
            command = string.Format(CommandStaticStrings.StartMoveCommand, (int)direction);
            Assert.AreEqual("DIR_SET:2;", command);

            direction = AutoSky.UDLR.Left;
            command = string.Format(CommandStaticStrings.StartMoveCommand, (int)direction);
            Assert.AreEqual("DIR_SET:3;", command);

            direction = AutoSky.UDLR.Right;
            command = string.Format(CommandStaticStrings.StartMoveCommand, (int)direction);
            Assert.AreEqual("DIR_SET:4;", command);
        }
    }
}
