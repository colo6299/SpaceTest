using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    public Item Primary;
    public Item Secondary;
    public Item Armor;
    public Item Engine;

    private Dictionary<SlotType, Item> Slots = new Dictionary<SlotType, Item>
    {
        { SlotType.None, null },
        { SlotType.Primary, null },
        { SlotType.Secondary, null },
        { SlotType.Armor, null },
        { SlotType.Engine, null }
    };

    public override void Start()
    {
        base.Start();

        EquipItem(Primary, SlotType.Primary);
        EquipItem(Secondary, SlotType.Secondary);
        EquipItem(Armor, SlotType.Armor);
        EquipItem(Engine, SlotType.Engine);
    }

    /// <summary>
    /// Equips a new item
    /// 
    /// Returns the previously equipted item or the item provided if equiping fails
    /// </summary>
    /// <param name="item">the item being equipted</param>
    /// <param name="type">slot to equipt too</param>
    public Item EquipItem(Item item, SlotType type)
    {
        //if (!CanShipUseItem(item)) return item;

        // the None slot type is not valid
        if (type == SlotType.None)
        {
            return item;
        }

        Item oldItem = Slots[type];

        // deEquips an item
        if (item == null)
        {
            if (oldItem != null)
            {
                oldItem.gameObject.SetActive(false);
            }

            Slots[type] = null;

            return item;
        }

        // back out if the item slot type does not match the desired slot
        else if (item.Type != type)
        {
            return item;
        }

        // properly slot the item
        if (oldItem != null)
        {
            oldItem.gameObject.SetActive(false);
        }

        item.gameObject.SetActive(true);
        Slots[type] = item;
        return oldItem;
    }

    /// <summary>
    /// Destorys item
    /// </summary>
    /// <param name="item"></param>
    public void TrashItem(Item item)
    {
        //if (!CanShipUseItem(item)) return;

        if (item == null) return;

        if (item == Slots[item.Type])
        {
            EquipItem(null, item.Type);
        }

        Destroy(item.gameObject);
    }

    /// <summary>
    /// Returns true if the item is a child of this ship
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    //public bool CanShipUseItem(Item item)
    //{
    //    return gameObject.transform.Find(item.gameObject.transform.name) != null;
    //}

    /// <summary>
    /// Returns the item equipted to the slot of this items type
    ///         
    /// returns null if nothing equipt
    /// </summary>
    public Item GetEquippedItem(Item item)
    {
        return GetEquippedItem(item.Type);
    }

    /// <summary>
    /// Returns the item equipted in the given slot
    /// 
    /// returns null if nothing equipt
    /// </summary>
    public Item GetEquippedItem(SlotType type)
    {
        return Slots[type];
    }
}
