namespace MinotaurLabyrinth
{
    /// <summary>
    /// Represents a Slimy gel monster in the game.
    /// </summary>
    public class GelatinousCube : Monster, IMoveable
    {
        private Location _location;

        /// <summary>
        /// Initializes a new instance of the <see cref="GelatinousCube"/> class with the specified starting location.
        /// </summary>
        /// <param name="location">The starting location of the gelatinous cube.</param>
        public GelatinousCube(Location location)
        {
            _location = location;
        }

        /// <summary>
        /// Gets the current location of the gelatinous cube.
        /// </summary>
        /// <returns>The current location of the gelatinous cube.</returns>
        public Location GetLocation()
        {
            return _location;
        }

        /// <summary>
        /// Activates the gelatinous cube encounter when the hero enters its room.
        /// </summary>
        /// <param name="hero">The hero entering the room.</param>
        /// <param name="map">The map containing the room and the gelatinous cube.</param>
        public override void Activate(Hero hero, Map map)
        {
            ConsoleHelper.WriteLine("You cautiously step into the room, your senses on high alert. A shimmering, translucent figure comes into view—a gelatinous cube, silently pulsating in the darkness.", ConsoleColor.DarkGreen);

            if (hero.GetStealth() >= 10)
            {
                ConsoleHelper.WriteLine("Your stealthy movements allow you to blend seamlessly into the shadows. The gelatinous cube remains unaware of your presence as you silently slip away, avoiding a potentially dangerous encounter.", ConsoleColor.Green);
                ConsoleHelper.WriteLine("Your heart races with relief as you navigate out of the room, grateful for your stealth and quick thinking.", ConsoleColor.Green);
            }
            else if (hero.GetAgility() >= 10)
            {
                ConsoleHelper.WriteLine("Reacting swiftly, you channel your agility and nimbleness to outmaneuver the gelatinous cube's slow advances.", ConsoleColor.DarkGreen);
                ConsoleHelper.WriteLine("With each graceful leap and agile dodge, you manage to stay one step ahead of the gelatinous cube's reach.", ConsoleColor.Green);
            }
            else
            {
                ConsoleHelper.WriteLine("Your movements betray you, and the gelatinous cube detects your presence. Its slimy form surges forward, attempting to engulf you.", ConsoleColor.DarkRed);

                ConsoleHelper.WriteLine("You desperately struggle to free yourself from the gelatinous cube's slimy grasp, but its grip tightens around you, restricting your movements.", ConsoleColor.Red);
                ConsoleHelper.WriteLine("The cube's acidic enzymes start dissolving your skin, causing excruciating pain and sapping your strength.", ConsoleColor.Red);
                ConsoleHelper.WriteLine("With each passing moment, your consciousness fades, succumbing to the relentless and consuming nature of the gelatinous cube.", ConsoleColor.Red);

                hero.Kill("You have been consumed by the gelatinous cube and perished, your body dissolving into its gelatinous form.");
            }
        }

        /// <summary>
        /// Moves the gelatinous cube towards the hero's location on the map.
        /// </summary>
        /// <param name="hero">The hero whose location is being targeted by the gelatinous cube.</param>
        /// <param name="map">The map containing the gelatinous cube.</param>
        public void Move(Hero hero, Map map)
        {
            var heroLocation = hero.Location;
            int dx = heroLocation.Column - _location.Column;
            int dy = heroLocation.Row - _location.Row;

            Location? newLocation;
            if (Math.Abs(dx) <= 1 && Math.Abs(dy) <= 1)
            {
                newLocation = SwapLocation(map, _location, heroLocation);
            }
            // gel not adjacent to player
            else
            {
                // if gel is closer in the y direction -> lets move it closer in the x direction
                if (Math.Abs(dx) > Math.Abs(dy))
                {
                    if (dx > 0)
                    {
                        var swapLocation = new Location(_location.Row, _location.Column + 1);
                        newLocation = SwapLocation(map, _location, swapLocation);
                    }
                    else
                    {
                        var swapLocation = new Location(_location.Row, _location.Column - 1);
                        newLocation = SwapLocation(map, _location, swapLocation);
                    }
                }
                else
                {
                    if (dy > 0)
                    {
                        var swapLocation = new Location(_location.Row - 1, _location.Column);
                        newLocation = SwapLocation(map, _location, swapLocation);
                    }
                    else
                    {
                        var swapLocation = new Location(_location.Row + 1, _location.Column);
                        newLocation = SwapLocation(map, _location, swapLocation);
                    }
                }
                if (newLocation == null)
                {
                    ConsoleHelper.WriteLine("The gelatinous cube squirms and pulsates in frustration, unable to move from its current position. It seems to be stuck, its efforts to close in on you thwarted for now.", ConsoleColor.Red);
                }
            }
        }

        /// <summary>
        /// Swaps the current location of the gelatinous cube with a new location on the map.
        /// </summary>
        /// <param name="map">The map containing the gelatinous cube.</param>
        /// <param name="currentLocation">The current location of the gelatinous cube.</param>
        /// <param name="newLocation">The new location to which the gelatinous cube will be moved.</param>
        /// <returns>The new location if the swap is successful; otherwise, null.</returns>
        private Location? SwapLocation(Map map, Location currentLocation, Location newLocation)
        {
            if (map.IsOnMap(newLocation) && !map.GetRoomAtLocation(newLocation).IsActive)
            {
                map.GetRoomAtLocation(currentLocation).RemoveMonster();
                map.GetRoomAtLocation(newLocation).AddMonster(this);
                _location = newLocation;
                return newLocation;
            }
            return null;
        }

        /// <summary>
        /// Displays the sense of the gelatinous cube when the hero is nearby.
        /// </summary>
        /// <param name="hero">The hero in the game.</param>
        /// <param name="heroDistance">The distance between the gelatinous cube and the hero.</param>
        /// <returns>True if the gelatinous cube's sense is displayed; otherwise, false.</returns>
        public override bool DisplaySense(Hero hero, int heroDistance)
        {
            if (heroDistance == 1)
            {
                ConsoleHelper.WriteLine("An ominous aura emanates around you filling the air with unease. The gelatinous cube lurks nearby..", ConsoleColor.DarkGreen);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Displays the representation of the gelatinous cube in the game.
        /// </summary>
        /// <returns>The display details of the gelatinous cube.</returns>
        public override DisplayDetails Display()
        {
            return new DisplayDetails("[C]", ConsoleColor.Green);
        }
    }
}
