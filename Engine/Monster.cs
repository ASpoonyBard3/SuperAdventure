using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Monster : LivingCreature
    {
        public Monster(int currentHitPoints, int maximumHitPoints) : base(currentHitPoints, maximumHitPoints)
        {
        }

        public int ID { get; set; }
        public int Name { get; set; }
        public int MaximumDamage { get; set; }
        public int RewardExpPoints { get; set; }
        public int RewardGold { get; set; }
    }
}
