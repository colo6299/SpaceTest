using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelInfo : MonoBehaviour {

    private class InfoBuilder
    {
        public string Name;
        public SlotType Slot;
        public Rarity Rarity;

        public string WeaponClass;
        public string OldWeaponClass;

        public float RateOfFire = float.NaN;
        public float OldRateOfFire = float.NaN;

        public float Ammunition = float.NaN;
        public float OldAmmunition = float.NaN;

        public float ReloadTime = float.NaN;
        public float OldReloadTime = float.NaN;

        public ResistanceTypes DamageType;
        public ResistanceTypes OldDamageType;

        public float Damage = float.NaN;
        public float OldDamage = float.NaN;

        public float CritChance = float.NaN;
        public float OldCritChance = float.NaN;

        public float CritDamageMultiplier = float.NaN;
        public float OldCritDamageMultiplier = float.NaN;
    }


    public void UpdateStats(Item hoveredItem, Item slottedItem)
    {
        InfoBuilder builder = new InfoBuilder();

        builder.Name = hoveredItem.name;
        builder.Slot = hoveredItem.Slot();
        builder.Rarity = hoveredItem.Rarity;

        if (hoveredItem is ProjectileWeapon)
        {
            ProjectileWeapon w = hoveredItem as ProjectileWeapon;
            builder.WeaponClass = "Projectile";


            builder.RateOfFire = w.RateOfFire;
            builder.Ammunition = w.Ammunition;
            builder.ReloadTime = w.ReloadTime;

            builder.DamageType = w.Stats.DamageType;
            builder.Damage = w.Stats.Damage;
            builder.CritChance = w.Stats.CritChance;
            builder.CritDamageMultiplier = w.Stats.CritDamageMultiplier;


        }

        if (slottedItem is ProjectileWeapon)
        {
            ProjectileWeapon w = slottedItem as ProjectileWeapon;
            builder.OldWeaponClass = "Projectile";

            builder.OldRateOfFire = w.RateOfFire;
            builder.OldAmmunition = w.Ammunition;
            builder.OldReloadTime = w.ReloadTime;

            builder.OldDamageType = w.Stats.DamageType;
            builder.OldDamage = w.Stats.Damage;
            builder.OldCritChance = w.Stats.CritChance;
            builder.OldCritDamageMultiplier = w.Stats.CritDamageMultiplier;
        }

        // panel

        transform.Find("BorderImage").GetComponent<Image>().color = Constants.RarityColor[builder.Rarity];

        // header

        Text headerName = transform.Find("Header").transform.Find("Name").GetComponent<Text>();
        Text headerSlot = transform.Find("Header").transform.Find("Slot").GetComponent<Text>();

        headerName.text = builder.Name;
        headerName.color = Constants.RarityColor[builder.Rarity];

        headerSlot.text = builder.Slot.ToString();

        // class

        Text classHovered = transform.Find("Class").transform.Find("Hovered").GetComponent<Text>();
        Text classPointer = transform.Find("Class").transform.Find("->").GetComponent<Text>();
        Text classSlotted = transform.Find("Class").transform.Find("Slotted").GetComponent<Text>();

        classHovered.text = builder.WeaponClass;

        if (builder.OldWeaponClass != string.Empty)
        {
            classPointer.gameObject.SetActive(true);
            classSlotted.gameObject.SetActive(true);

            classSlotted.text = builder.OldWeaponClass;
        }
        else
        {
            classPointer.gameObject.SetActive(false);
            classSlotted.gameObject.SetActive(false);
        }

        // damage

        DisplayType("Damage", builder.Damage.ToString("n2"), builder.OldDamage.ToString("n2"), GetColor(builder.Damage, builder.OldDamage), GetColor(builder.OldDamage, builder.Damage));
        DisplayType("CritChance", builder.CritChance.ToString("p2"), builder.OldCritChance.ToString("p2"), GetColor(builder.CritChance, builder.OldCritChance), GetColor(builder.OldCritChance, builder.CritChance));
        DisplayType("CritDamage", builder.CritDamageMultiplier.ToString("p2"), builder.OldCritDamageMultiplier.ToString("p2"), GetColor(builder.CritDamageMultiplier, builder.OldCritDamageMultiplier), GetColor(builder.OldCritDamageMultiplier, builder.CritDamageMultiplier));
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

    private void DisplayType(string name, string newValue, string oldValue, Color newColor, Color oldColor)
    {
        Text hovered = transform.Find(name).transform.Find("Hovered").GetComponent<Text>();
        Text slotted = transform.Find(name).transform.Find("Slotted").GetComponent<Text>();

        hovered.text = newValue;
        hovered.color = newColor;

        slotted.text = oldValue;
        slotted.color = oldColor;
    }
}
