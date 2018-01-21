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

            _player = new Player
            {
                CurrentHitPoints = 10,
                MaximumHitPoints = 10,
                Gold = 20,
                ExpPoints = 0,
                Level = 1
            };

            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblExp.Text = _player.ExpPoints.ToString();
            lblLevel.Text = _player.Level.ToString();
        }
        
    }
}
