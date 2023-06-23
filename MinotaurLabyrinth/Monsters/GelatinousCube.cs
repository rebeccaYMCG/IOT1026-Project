namespace MinotaurLabyrinth
{
    /// <summary>
    /// Represents a Slimy gel monster in the game.
    /// </summary>
    public class GelatinousCube : Monster, IMoveable
    {
        private Location _location;

        public GelatinousCube(Location location)
        {
            _location = location;
        }

        public Location GetLocation()
        {
            return _location;
        }

        public override void Activate(Hero hero, Map map)
        {
            ConsoleHelper.WriteLine("Caught off guard, the gelatinous cube engulfs you in its slimy embrace, restricting your movement.", ConsoleColor.DarkRed);
            hero.Kill("You have been consumed by the gelatinous cube and perished.");
        }

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

        public override bool DisplaySense(Hero hero, int heroDistance)
        {
            if (heroDistance == 1)
            {
                ConsoleHelper.WriteLine("An ominous aura emanates around you filling the air with unease. The gelatinous cube lurks nearby..", ConsoleColor.DarkGreen);
            }
            return false;
        }

        public override DisplayDetails Display()
        {
            return new DisplayDetails("[C]", ConsoleColor.Green);
        }
    }
}
