namespace MinotaurLabyrinth
{
    /// <summary>
    /// Represents a goblin room, which contains a gathering that the hero walks into.
    /// </summary>
    public class GoblinGathering : Room
    {
        static GoblinGathering()
        {
            RoomFactory.Instance.Register(RoomType.GoblinGathering, () => new GoblinGathering());
        }

        /// <inheritdoc/>
        public override RoomType Type { get; } = RoomType.GoblinGathering;

        /// <inheritdoc/>
        public override bool IsActive { get; protected set; } = true;

        /// <summary>
        /// do something
        /// </summary>
        public override void Activate(Hero hero, Map map)
        {
            // do something
        }

        /// <inheritdoc/>
        public override DisplayDetails Display()
        {
            return IsActive ? new DisplayDetails($"[{Type.ToString()[0]}]", ConsoleColor.Red)
                            : base.Display();
        }

        /// <summary>
        /// Displays sensory information about the goblin room, based on the hero's distance from it.
        /// </summary>
        /// <param name="hero">The hero sensing the goblins.</param>
        /// <param name="heroDistance">The distance between the hero and the goblin room.</param>
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
                    ConsoleHelper.WriteLine("", ConsoleColor.DarkGray);
                    return true;
                }
            }
            else if (heroDistance == 1 || heroDistance == 2)
            {
                ConsoleHelper.WriteLine(heroDistance == 1 ? "The air grows heavy with the scent of filth and unwashed bodies, something is nearby." : "The scent of dampness and decay fills the air as the faint sound of cackling laughter and raucous chattering echo against the walls.", ConsoleColor.DarkGreen);
                return true;
            }
            return false;
        }
    }
}
