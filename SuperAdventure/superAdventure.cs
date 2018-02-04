using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Engine;

namespace SuperAdventure
{
    public partial class superAdventure : Form
    {
        private Player _player;
        private Monster _currentMonster;

        public superAdventure()
        {
            InitializeComponent();

            _player = new Player(10, 10, 20, 0, 1);
            MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            _player.Inventory.Add(new InventoryItem(
                World.ItemByID(World.ITEM_ID_RUSTY_SWORD), 1));

            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblExp.Text = _player.ExpPoints.ToString();
            lblLevel.Text = _player.Level.ToString();
        }

        private void btnNorth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToNorth);
        }

        private void btnEast_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToEast);
        }

        private void btnSouth_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToSouth);
        }

        private void btnWest_Click(object sender, EventArgs e)
        {
            MoveTo(_player.CurrentLocation.LocationToWest);
        }

        private void MoveTo(Location newLocation)
        {
            //does the location have any required items
            if (newLocation.ItemRequiredToEnter != null)
            {
                //see if the player has the require item in their inventory
                bool playerHasRequiredItem = false;

                foreach (InventoryItem ii in _player.Inventory)
                {
                    if (ii.Details.ID == newLocation.ItemRequiredToEnter.ID)
                    {
                        //we found the required item
                        playerHasRequiredItem = true;
                        break; //exit out of the foreach loop
                    }
                }

                if (!playerHasRequiredItem)
                {
                    //we didn't find the required item in their inventory,
                    //so display a message and stop trying to move
                    rtbMessages.Text += "You must have a " +
                    newLocation.ItemRequiredToEnter.Name +
                    " to enter this location." + Environment.NewLine;
                    return;
                }
            }

            //update the player's current location
            _player.CurrentLocation = newLocation;

            // show/hide available movement buttons
            btnNorth.Visible = (newLocation.LocationToNorth != null);
            btnEast.Visible = (newLocation.LocationToEast != null);
            btnSouth.Visible = (newLocation.LocationToSouth != null);
            btnWest.Visible = (newLocation.LocationToWest != null);

            //display current location name and description
            rtbLocation.Text = newLocation.Name + Environment.NewLine;
            rtbLocation.Text += newLocation.Description + Environment.NewLine;

            // completely heal the player
            _player.CurrentHitPoints = _player.MaximumHitPoints;

            // update hit points in UI
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();

            //does the location have a quest?
            if (newLocation.QuestAvailableHere != null)
            {
                //see if the player already has the quest, and if they've completed it.
                bool playerAlreadyHasQuest = false;
                bool playerAlreadyCompletedQuest = false;

                foreach (PlayerQuest playerQuest in _player.Quests)
                {
                    if (playerQuest.Details.ID == newLocation.QuestAvailableHere.ID)
                    {
                        playerAlreadyHasQuest = true;

                        if (playerQuest.IsCompleted)
                        {
                            playerAlreadyCompletedQuest = true;
                        }
                    }
                }

                // see if the player already has the quest
                if (playerAlreadyHasQuest)
                {
                    //if the player has not completed the quest yet
                    if (!playerAlreadyCompletedQuest)
                    {
                        //see if the player has all the items needed to complete the quest
                        bool playerHasAllItemsToCompleteQuest = true;

                        foreach (QuestCompletionItems qci in newLocation.QuestAvailableHere.QuestCompletionItems)
                        {
                            bool foundItemInPlayerInventory = false;

                            //check each item in the player's inventory, to see
                            //if they have it, and enough of it.
                            foreach (InventoryItem ii in _player.Inventory)
                            {
                                // the player has this item in their inventory
                                if (ii.Details.ID == qci.Details.ID)
                                {
                                    foundItemInPlayerInventory = true;
                                    if (ii.Quantity < qci.Quantity)
                                    {
                                        //the player does not have enough of this item to complete the quest
                                        playerHasAllItemsToCompleteQuest = false;
                                        // there is not reason to continue checking for the other quest completion items
                                        break;
                                    }

                                    //we found the item, so don't check the rest of the player's inventory
                                    break;
                                }
                            }

                            //if we didn't find the required item, set our variable and 
                            // stop looking for other items
                            if (!foundItemInPlayerInventory)
                            {
                                // the player does not have this item in their inventory
                                playerHasAllItemsToCompleteQuest = false;

                                //there is not reason to keep checking for compltion items
                                break;
                            }
                        }

                        // the player has all items required to complete the quest
                        if (playerHasAllItemsToCompleteQuest)
                        {
                            // display message
                            rtbMessages.Text += Environment.NewLine;
                            rtbMessages.Text += "You complete the " +
                                newLocation.QuestAvailableHere.Name +
                                " quest." + Environment.NewLine;

                            //Remove quest items from inventory
                            foreach (QuestCompletionItems qci in
                                newLocation.QuestAvailableHere.QuestCompletionItems)
                            {
                                foreach (InventoryItem ii in _player.Inventory)
                                {
                                    //subtract the quantity from the player's 
                                    // inventory that was needed to complete the quest
                                    ii.Quantity -= qci.Quantity;
                                    break;
                                }
                            }
                        }
                        // give quest rewards
                        rtbMessages.Text += "You receive: " + Environment.NewLine;
                        rtbMessages.Text +=
                            newLocation.QuestAvailableHere.RewardExperiencePoints.ToString() +
                            " experience points" + Environment.NewLine;
                        rtbMessages.Text +=
                            newLocation.QuestAvailableHere.RewardGold.ToString() +
                            " gold" + Environment.NewLine;
                        rtbMessages.Text +=
                            newLocation.QuestAvailableHere.RewardItem.Name +
                                Environment.NewLine;
                        rtbMessages.Text += Environment.NewLine;

                        _player.ExpPoints +=
                            newLocation.QuestAvailableHere.RewardExperiencePoints;
                        _player.Gold += newLocation.QuestAvailableHere.RewardGold;

                        // add the reward item to the player's inventory
                        bool addedItemToPlayerInventory = false;

                        foreach (InventoryItem ii in _player.Inventory)
                        {
                            if (ii.Details.ID ==
                                newLocation.QuestAvailableHere.RewardItem.ID)
                            {
                                // they have the item in their inventory,
                                // so increase the quantity by one
                                ii.Quantity++;

                                addedItemToPlayerInventory = true;

                                break;
                            }
                        }

                        //they didn't have the item, so add it to their inventory,
                        //with a quantity of 1
                        if (!addedItemToPlayerInventory)
                        {
                            _player.Inventory.Add(new InventoryItem(
                                newLocation.QuestAvailableHere.RewardItem, 1));
                        }

                        // mark the quest as completed
                        // find the quest in the player's quest list
                        foreach (PlayerQuest pq in _player.Quests)
                        {
                            if (pq.Details.ID == newLocation.QuestAvailableHere.ID)
                            {
                                // mark it as completed
                                pq.IsCompleted = true;

                                break;
                            }
                        }
                    }
                }
            }
            else
            {
                // the player does not already have the quest

                //display the messages
                rtbMessages.Text += "You receive the " +
                    newLocation.QuestAvailableHere.Name +
                    " quest." + Environment.NewLine;
                rtbMessages.Text += newLocation.QuestAvailableHere.Description +
                    Environment.NewLine;
                rtbMessages.Text += "To complete it, return with:" +
                    Environment.NewLine;
                foreach (QuestCompletionItems qci
                    in newLocation.QuestAvailableHere.QuestCompletionItems)
                {
                    if (qci.Quantity == 1)
                    {
                        rtbMessages.Text += qci.Quantity.ToString() + " " +
                            qci.Details.Name + Environment.NewLine;
                    }
                    else
                    {
                        rtbMessages.Text += qci.Quantity.ToString() + " " +
                            qci.Details.NamePlural + Environment.NewLine;
                    }
                }
                rtbMessages.Text += Environment.NewLine;

                // add the quest to the player's quest list
                _player.Quests.Add(new PlayerQuest(newLocation.QuestAvailableHere));
            }
        }
        
        //does the location have a monster?
        
        







        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
        
        }
        
        private void btnUsePotion_Click(object sender, EventArgs e)
        {
        
        }



    }
}
