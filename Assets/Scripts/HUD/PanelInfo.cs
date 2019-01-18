using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfo : MonoBehaviour {

    private class InfoBuilder
    {
        public string Name;
        public SlotType Slot;
        public Rarity Rarity;

        public string Class;

        public float DPS = float.NaN;
        public float RateOfFire = float.NaN;
        public float Ammunition = float.NaN;
        public float ReloadTime = float.NaN;

        public WeaponInfo Stats = new WeaponInfo()
        {
            CritChance = float.NaN,
            CritDamage = float.NaN
        };

        public static InfoBuilder GetInfo(Item item)
        {
            InfoBuilder info = new InfoBuilder();

            if (item == null)
            {
                return info;
            }

            info.Name = item.name;
            info.Slot = item.Slot();
            info.Rarity = item.Rarity;

            if (item is ProjectileWeapon)
            {
                ProjectileWeapon w = item as ProjectileWeapon;
                info.Class = "Projectile";

                info.DPS = w.DPS();
                info.RateOfFire = w.RateOfFire;
                info.Ammunition = w.Ammunition;
                info.ReloadTime = w.ReloadTime;

                info.Stats = w.Stats;
            }

            return info;
        }
    }


    public void UpdateStats(Item hoveredItem, Item slottedItem)
    {
        InfoBuilder hovered = InfoBuilder.GetInfo(hoveredItem);
        InfoBuilder slotted = InfoBuilder.GetInfo(slottedItem);

        Transform HoveredPanel = transform.Find("New");

        // panel

        HoveredPanel.Find("BorderImage").GetComponent<Image>().color = Constants.RarityColor[hovered.Rarity];

        // header

        Text headerName = HoveredPanel.Find("Header").transform.Find("Name").GetComponent<Text>();
        Text headerSlot = HoveredPanel.Find("Header").transform.Find("Slot").GetComponent<Text>();

        headerName.text = hovered.Name;
        headerName.color = Constants.RarityColor[hovered.Rarity];
        headerSlot.text = hovered.Slot.ToString();

        // class

        Text weaponClass = HoveredPanel.Find("Class").transform.Find("Value").GetComponent<Text>();

        weaponClass.text = hovered.Class;

        // damage

        Display(HoveredPanel, "DPS", hovered.DPS.ToString("n2"), GetColor(hovered.DPS, slotted.DPS));
        Display(HoveredPanel, "Crit", hovered.Stats.CritChance.ToString("p1"), GetColor(hovered.Stats.CritChance, slotted.Stats.CritChance));
        Display(HoveredPanel, "CritDamage", hovered.Stats.CritDamage.ToString("p0"), GetColor(hovered.Stats.CritDamage, slotted.Stats.CritDamage));
        Display(HoveredPanel, "Ammo", hovered.Ammunition.ToString(), GetColor(hovered.Ammunition, slotted.Ammunition));
        Display(HoveredPanel, "Reload", hovered.ReloadTime.ToString("n3"), GetColor(slotted.ReloadTime, hovered.ReloadTime));
        Display(HoveredPanel, "RateOfFire", hovered.RateOfFire.ToString(), GetColor(hovered.RateOfFire, slotted.RateOfFire));

        DamageTypes[] types = hovered.Stats.Sort();

        StringBuilder sb = new StringBuilder();

        foreach (DamageTypes type in types)
        {
            float damage = hovered.Stats.Damage(type);

            if (damage > 0)
            {
                sb.Append(type.ToString() + ", ");
            }
        }

        Display(HoveredPanel, "DamageTypes", sb.ToString().TrimEnd(' ', ','), Color.white);
    }

    private void Display(Transform panel, string name, string value, Color color)
    {
        Text textbox = panel.Find(name).transform.Find("Value").GetComponent<Text>();

        textbox.text = value;
        textbox.color = color;
    }

    private Color GetColor(float v1, float v2)
    {
        if (float.IsNaN(v1))
        {
            return Color.red;
        }

        if (float.IsNaN(v2))
        {
            return Color.green;
        }

        if (v1 == v2)
        {
            return Color.white;
        }

        return (v1 > v2) ? Color.green : Color.red;
    }
}
