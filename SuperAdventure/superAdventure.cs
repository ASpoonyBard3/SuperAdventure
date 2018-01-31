using Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SuperAdventure
{
    public partial class superAdventure : Form
    {
        private Player _player;

        public superAdventure()
        {
            InitializeComponent();

            Location location = new Location(1, "Home", "This is your house.");

            _player = new Player(10, 10, 20, 0, 1);

            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblExp.Text = _player.ExpPoints.ToString();
            lblLevel.Text = _player.Level.ToString();
        }

        private void BtnNorth_Click(object sender, EventArgs e)
        {

        }

        private void BtnEast_Click(object sender, EventArgs e)
        {

        }

        private void BtnWest_Click(object sender, EventArgs e)
        {

        }

        private void BtnSouth_Click(object sender, EventArgs e)
        {

        }

        private void BtnUseWeapon_Click(object sender, EventArgs e)
        {

        }

        private void BtnUsePotion_Click(object sender, EventArgs e)
        {

        }
        //Location test1 = new Location(1, "Your house", "This is your house");
        //Location test2 = new Location(1, "Your house", "This is your house", null, null, null);

    }
}
