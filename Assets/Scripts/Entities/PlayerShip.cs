namespace Assets.Scripts.Entities
{
    public class PlayerShip : Ship
    {
        public Item Primary { get; private set; }
        public Item Secondary { get; private set; }
        public Item Armor { get; private set; }
        public Item Engine { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Item EquipItem(Item item, SlotType type)
        {
            // Equiping the item failed
            if (item.Type != type) return item;

            Item output;
            switch (type) 
            {
                case SlotType.Primary:
                    output = Primary;
                    Primary = item;
                    return output;

                case SlotType.Secondary:
                    output = Secondary;
                    Secondary = item;
                    return output;

                case SlotType.Armor:
                    output = Armor;
                    Armor = item;
                    return output;

                case SlotType.Engine:
                    output = Engine;
                    Engine = item;
                    return output;
            }

            return item;
        }
    }
}
