using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Player : LivingCreature
    {
        public int Gold { get; set; }
        public int ExpPoints { get; set; }
        public int Level { get; set; }
        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }
        public Location CurrentLocation { get; set; }

        public Player(int currentHitPoints, int maximumHitPoints,
            int gold, int expPoints, int level): base(currentHitPoints, maximumHitPoints)
        {
            Gold = gold;
            ExpPoints = expPoints;
            Level = level;
            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();
        }

        public bool HasRequiredItemToEnterThisLocation(Location location)
        {
            if(location.ItemRequiredToEnter == null)
            {
                //thereis no required item for this location, 
                // so return "true"
                return true;
            }
            // see if the player has the required item in their inventory
            foreach(InventoryItem ii in Inventory)
            {
                if(ii.Details.ID == location.ItemRequiredToEnter.ID)
                {
                    //we found the required item, so return "true"
                    return true;
                }
            }
            // we didn't find the required item in their inventory, so return "false"
            return false;
        }
    }
}
