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
        public int Level
        {
            get { return ((ExpPoints / 100) + 1); }
        }
        public List<InventoryItem> Inventory { get; set; }
        public List<PlayerQuest> Quests { get; set; }
        public Location CurrentLocation { get; set; }

        public Player(int currentHitPoints, int maximumHitPoints,
            int gold, int expPoints): base(currentHitPoints, maximumHitPoints)
        {
            Gold = gold;
            ExpPoints = expPoints;
            Inventory = new List<InventoryItem>();
            Quests = new List<PlayerQuest>();
        }

        public bool HasRequiredItemToEnterThisLocation(Location location)
        {
            if(location.ItemRequiredToEnter == null)
            {
                //there is no required item for this location, 
                // so return "true"
                return true;
            }

            //see if the play has the required item in their inventory
            return Inventory.Exists(ii => ii.Details.ID ==
                location.ItemRequiredToEnter.ID);
        }

        public bool HasThisQuest(Quest quest)
        {
            return Quests.Exists(pq => pq.Details.ID == quest.ID);
        }

        public bool CompletedThisQuest(Quest quest)
        {
            foreach (PlayerQuest playerQuest in Quests)
            {
                if(playerQuest.Details.ID == quest.ID)
                {
                    return playerQuest.IsCompleted;
                }
            }

            return false;
        }

        public bool HasAllQuestCompletionItems(Quest quest)
        {
            //see if the player has all the items needed to complete the quest here
            foreach(QuestCompletionItems qci in quest.QuestCompletionItems)
            {
              //check each item in the player's inventory,
              //to see if they have it, and enough of it
              if(!Inventory.Exists(ii => ii.Details.ID ==
                  qci.Details.ID && ii.Quantity >= qci.Quantity))
                {
                    return false;
                }
            }

            //if we got here, then the player must have all the required
            //items, and enough of them, to complete the quest.
            return true;
        }

        public void RemoveQuestCompletionItems(Quest quest)
        {
            foreach (QuestCompletionItems qci in quest.QuestCompletionItems)
            {
               Inv
            }
        }

        public void AddItemToInventory(Item itemToAdd)
        {
            foreach (InventoryItem ii in Inventory)
            {
                if(ii.Details.ID == itemToAdd.ID)
                {
                    //they have the item in their inventory, so increase the quantity by one
                    ii.Quantity++;

                    return; //we addedd the item, and are doen, so get out of this function
                }
            }
            //they didnt have the item,. so add it to their inventory, 
            //with a quantity of 1
            Inventory.Add(new InventoryItem(itemToAdd, 1));
        }

        public void MarkQuestCompleted(Quest quest)
        {
            // find the quest in the player's quest list
            foreach (PlayerQuest pq in Quests)
            {
                if(pq.Details.ID == quest.ID)
                {
                    //mark it as completed
                    pq.IsCompleted = true;

                    //we found the quest, and marked it complete, so get out of this function
                    return;
                }
            }
        }
            
    }
}
