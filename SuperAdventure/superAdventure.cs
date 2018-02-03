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
            if(newLocation.ItemRequiredToEnter != null)
            {
                //see if the player has the require item in their inventory
                bool playerHasRequiredItem = false;

                foreach (InventoryItem ii in _player.Inventory)
                {
                    if(ii.Details.ID == newLocation.ItemRequiredToEnter.ID)
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


        }

    }
}
