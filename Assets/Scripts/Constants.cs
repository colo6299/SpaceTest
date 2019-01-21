using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Constants
{
    public const string Tag_UI_Item = "UI_Item";
    public const string Tag_UI_Slot = "UI_Slot";

    public static readonly Dictionary<Rarity, Color> RarityColor = new Dictionary<Rarity, Color>
    {
        { Rarity.Common, new Color(0.7f, 0.7f, 0.7f, 1f) },
        { Rarity.Uncommon, new Color(55f/255f, 1f, 0, 1) },
        { Rarity.Rare, new Color(0, 160f/225f, 1f, 1) },
        { Rarity.Legendary, new Color(160f/225f, 0, 1f, 1) },
        { Rarity.Ultimate, new Color(1f, 245f/255f, 0, 1) },
        { Rarity.None, Color.red },
        { Rarity.Unique, Color.red },
    };

}
