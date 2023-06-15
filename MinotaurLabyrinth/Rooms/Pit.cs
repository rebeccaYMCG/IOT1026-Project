namespace MinotaurLabyrinth
{
    /// <summary>
    /// Represents a pit room, which contains a dangerous pit that the hero can fall into.
    /// </summary>
    public class Pit : Room
    {
        static Pit()
        {
            RoomFactory.Instance.Register(RoomType.Pit, () => new Pit());
        }

        /// <inheritdoc/>
        public override RoomType Type { get; } = RoomType.Pit;

        /// <inheritdoc/>
        public override bool IsActive { get; protected set; } = true;

        /// <summary>
        /// Activates the pit, causing the hero to potentially fall in and face consequences.
        /// </summary>
        public override void Activate(Hero hero, Map map)
        {
            if (IsActive)
            {
                ConsoleHelper.WriteLine("You walk into the room and the floor gives way revealing a pit of sharp spikes adorned with other adventurers!", ConsoleColor.Red);
                if (hero.GetAgility() >= 10)
                {
                    IsActive = false;
                    ConsoleHelper.WriteLine("You manage to grab the side of the pit and pull yourself onto safe ground", ConsoleColor.Green);
                    ConsoleHelper.WriteLine("Looking around, you find a mechanism that closes the depresses the spikes in the pit. This room is now safe.", ConsoleColor.Green);
                }
                else
                {
                    ConsoleHelper.WriteLine("Your hand slips and you plummet down towards the spikes.", ConsoleColor.DarkRed);
                    hero.Kill("You have fallen into a pit and died.");
                }
            }
        }

        /// <inheritdoc/>
        public override DisplayDetails Display()
        {
            return IsActive ? new DisplayDetails($"[{Type.ToString()[0]}]", ConsoleColor.Red)
                            : base.Display();
        }

        /// <summary>
        /// Displays sensory information about the pit, based on the hero's distance from it.
        /// </summary>
        /// <param name="hero">The hero sensing the pit.</param>
        /// <param name="heroDistance">The distance between the hero and the pit room.</param>
        /// <returns>Returns true if a message was displayed; otherwise, false.</returns>
        public override bool DisplaySense(Hero hero, int heroDistance)
        {
            if (!IsActive)
            {
                if (base.DisplaySense(hero, heroDistance))
                {
                    return true;
                }
                if (heroDistance == 0)
                {
                    ConsoleHelper.WriteLine("You shudder as you recall your near death experience with the now defunct pit in this room.", ConsoleColor.DarkGray);
                    return true;
                }
            }
            else if (heroDistance == 1 || heroDistance == 2)
            {
                ConsoleHelper.WriteLine(heroDistance == 1 ? "You feel a draft. There is a pit in a nearby room!" : "Your intuition tells you that something dangerous is nearby", ConsoleColor.DarkGray);
                return true;
            }
            return false;
        }
    }
}
