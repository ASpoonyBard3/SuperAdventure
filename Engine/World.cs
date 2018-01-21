using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public static class World
    {
        public static readonly List<Item> Items = new List<Item>(); 
        public static readonly List<Monster> monsters = new List<Monster>();
        public static readonly List<Quest> quests = new List<Quest>();
        public static readonly List<Location> Location = new List<Location>(); 
        
        public const int ITEM_ID_RUSTY_SWORD = 1; 
        public const int ITEM_ID_RAT_TAIL = 2; 
        public const int ITEM_ID_PIECE_OF_FUR = 3; 
        public const int ITEM_ID_SNAKE_FANG = 4; 
        public const int ITEM_ID_SNAKESKIN = 5;
        public const int ITEM_ID_CLUB = 6;
        public const int ITEM_ID_HEALING_POTION = 7;
        public const int ITEM_ID_SPIDER_FANG = 8;
        public const int ITEM_ID_SPIDER_SILK = 9;
        public const int ITEM_ID_ADVENTURE_PASS = 10;

        public const int MONSTER_ID_RAT = 1;
        public const int MONSTER_ID_SNAKE = 2;
        public const int MONSTER_ID_GIANT_SPIDER = 3;

        public const int QUEST_ID_CLEAR_ALCHEMIST_GARDEN = 1;
        public const int QUEST_ID_CLEAR_FARMERS_FIELD = 2;

        public const int LOCATION_ID_HOME = 1;
        public const int LOCATION_ID_TOWN_SQUARE = 2;
        public const int LOCATION_ID_GUARD_POST = 3;
        public const int LOCATION_ID_ALCHEMIST_HUT = 4;
        public const int LOCATION_ID_ALCHEMISTS_GARDEN = 5;
        public const int LOCATION_ID_FARMHOUSE = 6;
        public const int LOCATION_ID_FARM_FIELD = 7;
        public const int LOCATION_ID_BRIDGE = 8;
        public const int LOCATION_ID_SPIDER_FIELD = 9;
        public const int LOCATION_ID_MAKO_REACTOR = 10;

        static World()
        {
            PopulateItems();
            PopulateMonsters();
            PopulateQuests();
            PopulateLocations();
        }

        private static void PopulateItems()
        {
            Items.Add(new Weapon(ITEM_ID_RUSTY_SWORD, "Rusty sword", "Rusty swords", 0, 5));
            Items.Add(new Item(ITEM_ID_RAT_TAIL, "Rat tail","Rat tails"));
            Items.Add(new Item(ITEM_ID_PIECE_OF_FUR, "Piece of fur","Pieces of fur"));
            Items.Add(new Item(ITEM_ID_SNAKE_FANG, "Snake fang", "Snake fangs"));
            Items.Add(new Item(ITEM_ID_SNAKESKIN, "Snakeskin", "Snakeskins"));
            Items.Add(new Weapon(ITEM_ID_CLUB, "Club", "Clubs", 3, 10));
            Items.Add(new HealingPotion(ITEM_ID_HEALING_POTION, "Healing potion", "Healing Potions", 5));
            Items.Add(new Item(ITEM_ID_SPIDER_FANG, "Spider fang", "Spider Fangs"));
            Items.Add(new Item(ITEM_ID_SPIDER_SILK, "Spider silk", "Spider silks"));
            Items.Add(new Item(ITEM_ID_ADVENTURE_PASS, "Adventurer pass", "Adventurer passes"));
            //throw new NotImplementedException();
        }

        private static void PopulateMonsters()
        {
            Monster rat = new Monster(MONSTER_ID_RAT, "Rat", 5, 3, 10, 3, 3);
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_RAT_TAIL), 75, false));
            rat.LootTable.Add(new LootItem(ItemByID(ITEM_ID_PIECE_OF_FUR), 75, true));
            
            Monster snake = new Monster(MONSTER_ID_SNAKE, "Snake", 5, 3, 10, 3, 3);
            snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKE_FANG), 75, false));
            snake.LootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKESKIN), 75, true));
            
            Monster giantSpider = new Monster(MONSTER_ID_GIANT_SPIDER,"Giant spider", 20, 5, 40, 10, 10);
            giantSpider.LootTable.Add(new LootItem(
            ItemByID(ITEM_ID_SPIDER_FANG), 75, true));
            giantSpider.LootTable.Add(new LootItem(
            ItemByID(ITEM_ID_SPIDER_SILK), 25, false));
            
            Monsters.Add(rat);
            Monsters.Add(snake);
            Monsters.Add(giantSpider);
            throw new NotImplementedException();
        }

        private static void PopulateLocations()
        {







            throw new NotImplementedException();
        }

        private static void PopulateQuests()
        {
            Quest clearAlchemistGarden = new Quest(QUEST_ID_CLEAR_ALCHEMIST_GARDEN,
            "Clear the alchemist's garden",
            "Kill rats in the alchemist's garden and bring back 3 rat tails. You will receive a healing potion and 10 gold pieces.", 20, 10);

            clearAlchemistGarden.QuestCompletionItems.Add(new QuestCompletionItem(
            ItemByID(ITEM_ID_RAT_TAIL), 3));
            
            clearAlchemistGarden.RewardItem = ItemByID(ITEM_ID_HEALING_POTION);
                     
            Quest clearFarmersField = new Quest(QUEST_ID_CLEAR_FARMERS_FIELD,
            "Clear the farmer's feld",
            "Kill snakes in the farmer's feld and bring back 3 snake fangs.You will receive an adventurer's pass and 20 gold pieces.", 20, 20);

            clearFarmersField.QuestCompletionItems.Add(new QuestCompletionItem(
            ItemByID(ITEM_ID_SNAKE_FANG), 3));
            
            clearFarmersField.RewardItem = ItemByID(ITEM_ID_ADVENTURER_PASS);
                    
            Quests.Add(clearAlchemistGarden);
            Quests.Add(clearFarmersField);

            throw new NotImplementedException();
        }


    }
