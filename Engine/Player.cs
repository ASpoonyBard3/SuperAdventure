﻿using System;
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

        public bool HasThisQuest(Quest quest)
        {
            foreach(PlayerQuest playerQuest in Quests)
            {
                if(playerQuest.Details.ID == quest.ID)
                {
                    return true;
                }
            }

            return false;
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
                bool foundItemInPlayersInventory = false;

                //check each item in the player's inventory
                //to see if they have it, and enough of it
                foreach(InventoryItem ii in Inventory)
                {
                    //the player has the item in their inventory
                    if(ii.Details.ID == qci.Details.ID)
                    {
                        foundItemInPlayersInventory = true;
                        //the player does not have enough of this item to compelte the quest
                        if(ii.Quantity < qci.Quantity)
                        {
                            return false;
                        }
                    }
                }

                //the player does not have any of this 
                //quest completion item in their inventory
                if (!foundItemInPlayersInventory)
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
                foreach (InventoryItem ii in Inventory)
                {
                    if(ii.Details.ID == qci.Details.ID)
                    {
                        //subtract the quantity from the player's
                        //inventory that was needed to complete the quest
                        ii.Quantity -= qci.Quantity;
                        break;
                    }
                }
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
