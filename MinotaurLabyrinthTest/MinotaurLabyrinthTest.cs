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
        }
    }
}
