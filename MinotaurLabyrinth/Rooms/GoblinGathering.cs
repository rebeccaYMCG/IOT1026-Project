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
        /// hero walks into the room, causing the hero to either sneak away, flee, or face the group head on.
        /// </summary>
        public override void Activate(Hero hero, Map map)
        {
            if (IsActive)
            {
                ConsoleHelper.WriteLine("Peering through the narrow gap, you witness a scene that sends a shiver down your spine: a room filled with goblins. The chamber is dimly lit, the air thick with the stench of goblin musk and the echoes of their raucous laughter.", ConsoleColor.DarkGreen);
                if (hero.GetStealth() >= 10)
                {
                    IsActive = false;
                    ConsoleHelper.WriteLine("The goblins are scattered about, engaged in various activities, their beady eyes scanning their surroundings. You realize that you have inadvertently stumbled upon the heart of the goblin lair, and a surge of adrenaline courses through your veins as you prepare to face the imminent challenge that lies ahead.", ConsoleColor.DarkGreen);
                    ConsoleHelper.WriteLine("Silently and swiftly, you turn away, ensuring not a sound betrays your presence. Relief washes over you as you retreat, knowing you avoided an encounter you would have likely lost.", ConsoleColor.Green);
                }
                else
                {
                    ConsoleHelper.WriteLine("As you cautiously approach the door, your heart races with anticipation. But a small misstep gives you away—a creaking floorboard beneath your weight.", ConsoleColor.DarkRed);
                    ConsoleHelper.WriteLine("The goblins inside snap their heads in your direction, their eyes narrowing with malicious intent. Panic sets in as the door swings open, revealing a horde of angry goblins. You find yourself trapped, encircled by their snarling faces, outnumbered and with little hope of escape.", ConsoleColor.DarkRed);

                    if (hero.GetAgility() >= 10)
                    {
                        ConsoleHelper.WriteLine("Realizing you are outnumbered, you turn and run down the way you came, planning on losing the goblins with each turn.", ConsoleColor.DarkRed);
                        ConsoleHelper.WriteLine("As you flee from the relentless goblin pursuit, your heart pounds with fear and determination. The echoes of their war cries fuel your adrenaline as you sprint through the dark corridors, desperately seeking an escape route. Every step is a fight for survival, your mind focused solely on outrunning your pursuers and finding safety.", ConsoleColor.Green);
                    }
                    else
                    {
                        ConsoleHelper.WriteLine("Before you can react, they launch themselves at you with savage aggression, their crude weapons slashing through the air.", ConsoleColor.Red);
                        hero.Kill("With a gasp, you feel an intense pain in your stomach.. your arms become heavier as your eyes begin to shut.");
                    }
                    if (hero.HasSword)
                    {
                        IsActive = false;
                        ConsoleHelper.WriteLine("With adrenaline coursing through your veins, you engage the goblin horde head-on. Swinging your weapon with precision, you parry their attacks and retaliate with swift strikes. Despite sustaining a few wounds, your determination fuels your every move.", ConsoleColor.Red);
                        ConsoleHelper.WriteLine("The clash of steel fills the room as you push back against the goblins, steadily thinning their numbers. With a final swing, the last goblin falls, leaving you victorious but weary. You take a moment to catch your breath before pressing forward, ready to face the next challenge that awaits.", ConsoleColor.Red);
                    }
                    else
                    {
                        ConsoleHelper.WriteLine("In a dire confrontation with the goblins, you bravely face them head-on, but lacking a sword, the odds are against you. The goblins swarm around you, their attacks relentless and unforgiving. Despite your determination and resourcefulness, you struggle to defend yourself.", ConsoleColor.Red);
                        hero.Kill("Overwhelmed by their sheer numbers and ferocity, you eventually succumb to their assault, unable to overcome the formidable challenge.");
                    }
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
                    ConsoleHelper.WriteLine("The scent of goblins lingers in the air, leaving you with an unsettling feeling as you recall your encounter with the group.", ConsoleColor.DarkGray);
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
