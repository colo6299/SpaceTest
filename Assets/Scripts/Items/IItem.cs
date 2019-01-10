using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum SlotType { Primary, Secondary, Armor }

interface IItem
{
    /// <summary>
    /// Gets the slot this item fits into
    /// </summary>
    /// <returns>SlotType enum item</returns>
    SlotType Slot();

    /// <summary>
    /// Rolls random stats for this item
    /// </summary>
    /// <param name="info"></param>
    void RollStats(RollInfo info);

}
