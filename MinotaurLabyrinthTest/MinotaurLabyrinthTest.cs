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
            GoblinGathering goblinGatheringRoom = new GoblinGathering();
            Hero hero = new Hero();
            Map map = new Map(1, 1);

            goblinGatheringRoom.Activate(hero, map);
            Assert.AreEqual(false, goblinGatheringRoom.IsActive);
            Assert.AreEqual(true, hero.IsAlive);

            hero.HasSword = true;
            goblinGatheringRoom.Activate(hero, map);
            Assert.AreEqual(false, goblinGatheringRoom.IsActive);
            Assert.AreEqual(true, hero.IsAlive);

            GoblinGathering newGoblinGatheringRoom = new GoblinGathering();
            newGoblinGatheringRoom.Activate(hero, map);
            Assert.AreEqual(true, goblinGatheringRoom.IsActive);
            Assert.AreEqual(false, hero.IsAlive);
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
    }
}
