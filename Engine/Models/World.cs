using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class World
    {
        private readonly List<Location> Locations = new List<Location>();

        public void AddLocation(int XCoordinate,int YCoordinate, string Name, string Description, string ImageName) {
            Locations.Add(new Location(XCoordinate,YCoordinate,Name,Description,$"/Engine;component/Images/Locations/{ImageName}"));
        }

        public Location LocationAt(int xCoordinate, int yCoordinate) {
            foreach (Location location in Locations) {
                if (location.XCoordinate == xCoordinate && location.YCoordinate == yCoordinate) { 
                    return location;
                }
            }
            return null;
        }
    }
}
