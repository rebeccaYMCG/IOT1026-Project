using MinotaurLabyrinth;

namespace MinotaurLabyrinthTest
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        public void PitRoomTest()
        {
            Pit pitRoom = new Pit();
            Hero hero = new Hero();
            Map map = new Map(1, 1);

            pitRoom.Activate(hero, map);
            Assert.AreEqual(pitRoom.IsActive, false);
            Assert.AreEqual(hero.IsAlive, true);

            hero.HasSword = true;
            pitRoom.Activate(hero, map);
            Assert.AreEqual(hero.IsAlive, true);

            Pit newPitRoom = new Pit();
            newPitRoom.Activate(hero, map);
            Assert.AreEqual(hero.IsAlive, false);
        }

        [TestMethod]
        public void GoblinGatheringTest()
        {
            GoblinGathering goblinGathering = new GoblinGathering();
            Hero hero = new Hero();
            hero.SetStealth(15);
            Map map = new Map(1, 1);

            // Act
            goblinGathering.Activate(hero, map);

            // Assert
            Assert.IsFalse(goblinGathering.IsActive);
            Assert.IsFalse(hero.IsAlive);

            // Additional assertions for DisplaySense
            bool result = goblinGathering.DisplaySense(hero, 0);
            Assert.IsTrue(result);

            result = goblinGathering.DisplaySense(hero, 1);
            Assert.IsFalse(result);
        }
    }

    [TestClass]
    public class MonsterTests
    {
        [TestMethod]
        public void MinotaurTest()
        {
            Hero hero = new Hero();
            Minotaur minotaur = new Minotaur();
            Map map = new Map(4, 4);
            hero.HasSword = true;
            Assert.AreEqual(hero.HasSword, true);

            minotaur.Activate(hero, map);
            // Charge moves the hero 1 room east and 2 rooms north
            // -1 is off the map so hero position should be (0, 2)
            Assert.AreEqual(hero.Location, new Location(0, 2));
            Assert.AreEqual(hero.HasSword, false);

            minotaur.Activate(hero, map);
            Assert.AreEqual(hero.Location, new Location(0, 3));

            hero.Location = new Location(3, 1);
            minotaur.Activate(hero, map);
            Assert.AreEqual(hero.Location, new Location(2, 3));
        }

        [TestMethod]
        public void GelMoveTest()
        {
            Hero hero = new Hero();
            var gelLocation = new Location(1, 1);
            GelatinousCube gel = new(gelLocation);
            Map map = new Map(4, 4);
            map.GetRoomAtLocation(gelLocation).AddMonster(gel);
            gel.Move(hero, map);

            var expectedLocation = new Location(2, 1);
            Assert.AreEqual(expectedLocation, gel.GetLocation());
            gel.Move(hero, map);
            expectedLocation = new Location(3, 1);
            Assert.AreEqual(expectedLocation, gel.GetLocation());
            gel.Move(hero, map);
            expectedLocation = new Location(3, 1);
            Assert.AreEqual(expectedLocation, gel.GetLocation());
        }

        [TestMethod]
        public void GelMoveToActiveTest()
        {
            Hero hero = new Hero();
            var gelLocation = new Location(1, 1);
            GelatinousCube gel = new(gelLocation);
            Map map = new Map(4, 4);
            map.GetRoomAtLocation(gelLocation).AddMonster(gel);
            map.SetRoomAtLocation(new Location(2, 1), RoomType.Pit);
            gel.Move(hero, map);
            // New location should be (1,1)
            var expectedLocation = new Location(1, 1);
            Assert.AreEqual(expectedLocation, gel.GetLocation());
        }
    }
}
