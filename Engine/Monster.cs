using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    class Monster : LivingCreature
    {
        public int ID { get; set; }
        public int Name { get; set; }
        public int MaximumDamage { get; set; }
        public int RewardExpPoints { get; set; }
        public int RewardGold { get; set; }
    }
}
