using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Location
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ItemRequiredToEnter { get; set; }
        public int QuestAvailableHere { get; set; }
        public int MonsterLivingHere { get; set; }
        public int LocationToNorth { get; set; }
        public int LocationToEast { get; set; }
        public int LocationToSouth { get; set; }
        public int LocationToWest { get; set; }


        public Location (int id, string name, string description, 
            Item itemrequiredToEnter = null, Quest questAvailableHere = null,
            Monster monsterLivingHere = null)
        {
            ID = id;
            Name = name;
            Description = description;
            ItemRequiredToEnter = itemrequiredToEnter;
            QuestAvailableHere = questAvailableHere;
            MonsterLivingHere = monsterLivingHere;
        }
    }
}
