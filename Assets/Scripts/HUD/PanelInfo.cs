using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfo : MonoBehaviour
{

    enum PropertyNames { DPS, CritChance, CritDamage, WeaponStat1, WeaponStat2, WeaponStat3, WeaponStat4, WeaponStat5, WeaponStat6 }

    private class Property
    {
        public bool Enabled;
        public string Name;
        public float Value;
        public string Format;

        public Property(string n, float v, bool enabled = true)
        {
            Enabled = enabled;
            Name = n;
            Value = v;
            Format = "n0";
        }

        public Property(string n, float v, string f, bool enabled = true)
        {
            Enabled = enabled;
            Name = n;
            Value = v;
            Format = f;
        }
    }

    private class InfoBuilder
    {
        public string Name;
        public SlotType Slot;
        public Rarity Rarity;
        public string Class;
        public string DamageType;

        public Property[] Properties;

        public InfoBuilder(Item item, SlotType hoveredItemType = SlotType.None)
        {
            // Prep the property array.
            string[] names = Enum.GetNames(typeof(PropertyNames));
            Properties = new Property[names.Length];

            for (int i = 0; i < names.Length; i++)
            {
                if (names[i].Contains("WeaponStat"))
                {
                    Properties[i] = new Property(names[i], float.NaN, false);
                }
                else
                {
                    Properties[i] = new Property(names[i], float.NaN);
                }
            }

            if (item == null)
            {
                Name = "None";
                Slot = hoveredItemType;
                Rarity = Rarity.None;
                Class = "None";
                DamageType = "None";
            }
            else
            {
                Name = item.name;
                Slot = item.Slot();
                Rarity = item.Rarity;
            }

            if (item is WeaponBase)
            {
                WeaponBase wb = item as WeaponBase;
                DamageTypes[] types = wb.Stats.Sort();
                StringBuilder sb = new StringBuilder();

                foreach (DamageTypes type in types)
                {
                    float damage = wb.Stats.Damage(type);

                    if (damage > 0)
                    {
                        sb.Append(type.ToString() + ", ");
                    }
                }

                DamageType = sb.ToString().TrimEnd(' ', ',');

                Properties[(int)PropertyNames.DPS].Value = wb.DPS();
                Properties[(int)PropertyNames.CritChance].Value = wb.Stats.CritChance;
                Properties[(int)PropertyNames.CritChance].Format = "n1";
                Properties[(int)PropertyNames.CritDamage].Value = wb.Stats.CritDamage;
            }

            if (item is ProjectileWeapon)
            {
                ProjectileWeapon pw = item as ProjectileWeapon;
                Class = "Projectile";

                Properties[(int)PropertyNames.WeaponStat1] = new Property("RoF", pw.RateOfFire, "n1");
                Properties[(int)PropertyNames.WeaponStat2] = new Property("Ammo", pw.Ammunition);
                Properties[(int)PropertyNames.WeaponStat3] = new Property("Reload", pw.ReloadTime, "n3");

            }
            else if (item is BeamWeapon)
            {
                BeamWeapon bw = item as BeamWeapon;
                Class = "Beam";
            }
        }
    }


    public void UpdateStats(Item hoveredItem, Item slottedItem)
    {
        InfoBuilder hovered = new InfoBuilder(hoveredItem);
        InfoBuilder slotted = new InfoBuilder(slottedItem, hovered.Slot);

        Transform HoveredPanel = transform.Find("New");

        // panel

        HoveredPanel.Find("BorderImage").GetComponent<Image>().color = Constants.RarityColor[hovered.Rarity];

        // header

        Text headerName = HoveredPanel.Find("Header").GetChild(0).GetComponent<Text>();
        Text headerSlot = HoveredPanel.Find("Header").GetChild(1).GetComponent<Text>();

        headerName.text = hovered.Name;
        headerName.color = Constants.RarityColor[hovered.Rarity];
        headerSlot.text = hovered.Slot.ToString();

        // General

        Text weaponClass = HoveredPanel.Find("Class").GetChild(1).GetComponent<Text>();
        weaponClass.text = hovered.Class;

        Text damageTypes = HoveredPanel.Find("DamageTypes").GetChild(1).GetComponent<Text>();
        damageTypes.text = hovered.DamageType;



        for (int i = 0; i < hovered.Properties.Length; i++)
        {
            Property p = hovered.Properties[i];
            Property s = slotted.Properties[i];
            Color color = Color.white;

            if (p.Name == s.Name)
            {
                if (p.Name == "Reload")
                {
                    color = GetColor(s.Value, p.Value);
                }
                else
                {
                    color = GetColor(p.Value, s.Value);
                }
            }

            Display(HoveredPanel, p.Name, p.Value.ToString(p.Format), color);
        }



        // other

        //Display(HoveredPanel, "DPS", hovered.DPS.ToString("n2"), GetColor(hovered.DPS, slotted.DPS));
        //Display(HoveredPanel, "Crit", hovered.Stats.CritChance.ToString("p1"), GetColor(hovered.Stats.CritChance, slotted.Stats.CritChance));
        //Display(HoveredPanel, "CritDamage", hovered.Stats.CritDamage.ToString("p0"), GetColor(hovered.Stats.CritDamage, slotted.Stats.CritDamage));
        //Display(HoveredPanel, "Ammo", hovered.Ammunition.ToString(), GetColor(hovered.Ammunition, slotted.Ammunition));
        //Display(HoveredPanel, "Reload", hovered.ReloadTime.ToString("n3"), GetColor(slotted.ReloadTime, hovered.ReloadTime));
        //Display(HoveredPanel, "RateOfFire", hovered.RateOfFire.ToString(), GetColor(hovered.RateOfFire, slotted.RateOfFire));

        //DamageTypes[] types = hovered.Stats.Sort();

        //StringBuilder sb = new StringBuilder();

        //foreach (DamageTypes type in types)
        //{
        //    float damage = hovered.Stats.Damage(type);

        //    if (damage > 0)
        //    {
        //        sb.Append(type.ToString() + ", ");
        //    }
        //}

        //Display(HoveredPanel, "DamageTypes", sb.ToString().TrimEnd(' ', ','), Color.white);
    }

    private void Display(Transform panel, string name, string value, Color color, bool enabled = true)
    {

        Transform node = panel.Find(name);

        if (enabled)
        {
            node.gameObject.SetActive(true);
            node.GetChild(0).GetComponent<Text>().text = name;

            Text textbox = node.GetChild(1).GetComponent<Text>();
            textbox.text = value;
            textbox.color = color;
        }
        else
        {
            node.gameObject.SetActive(false);
        }

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
