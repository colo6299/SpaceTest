﻿using System;
using System.Text;
using UnityEngine;

public class InfoPanelManager : MonoBehaviour {

    public InfoPanel hovered;
    public InfoPanel slotted;

    public void DisplayItemComparison(Item h, Item s)
    {
        Populate(hovered, h);
        Populate(slotted, s);

        if (h is WeaponBase && s is WeaponBase)
        {
            WeaponBase hw = h as WeaponBase;
            WeaponBase sw = s as WeaponBase;

            if (hw.DPS() > sw.DPS())
            {
                hovered.dps.color = Color.green;
                slotted.dps.color = Color.red;
            }
            else if (hw.DPS() < sw.DPS())
            {
                hovered.dps.color = Color.red;
                slotted.dps.color = Color.green;
            }

            if (hw.Stats.CritChance > sw.Stats.CritChance)
            {
                hovered.critChance.color = Color.green;
                slotted.critChance.color = Color.red;
            }
            else if (hw.Stats.CritChance < sw.Stats.CritChance)
            {
                hovered.critChance.color = Color.red;
                slotted.critChance.color = Color.green;
            }

            if (hw.Stats.CritDamage > sw.Stats.CritDamage)
            {
                hovered.critDamage.color = Color.green;
                slotted.critDamage.color = Color.red;
            }
            else if (hw.Stats.CritDamage < sw.Stats.CritDamage)
            {
                hovered.critDamage.color = Color.red;
                slotted.critDamage.color = Color.green;
            }

            if (h is ProjectileWeapon && s is ProjectileWeapon)
            {
                ProjectileWeapon hpw = h as ProjectileWeapon;
                ProjectileWeapon spw = s as ProjectileWeapon;

                int hAmmo = (hpw.Ammunition == 0) ? int.MaxValue : hpw.Ammunition;
                int sAmmo = (spw.Ammunition == 0) ? int.MaxValue : spw.Ammunition;

                if (hAmmo > sAmmo)
                {
                    hovered.stat1Value.color = Color.green;
                    slotted.stat1Value.color = Color.red;
                }
                else if (hAmmo < sAmmo)
                {
                    hovered.stat1Value.color = Color.red;
                    slotted.stat1Value.color = Color.green;
                }

                if (hpw.RateOfFire > spw.RateOfFire)
                {
                    hovered.stat2Value.color = Color.green;
                    slotted.stat2Value.color = Color.red;
                }
                else if (hpw.RateOfFire < spw.RateOfFire)
                {
                    hovered.stat2Value.color = Color.red;
                    slotted.stat2Value.color = Color.green;
                }

                if (hpw.ReloadTime < spw.ReloadTime)
                {
                    hovered.stat3Value.color = Color.green;
                    slotted.stat3Value.color = Color.red;
                }
                else if (hpw.ReloadTime > spw.ReloadTime)
                {
                    hovered.stat3Value.color = Color.red;
                    slotted.stat3Value.color = Color.green;
                }
            }
            else if (h is BeamWeapon && s is BeamWeapon)
            {
                BeamWeapon hbw = h as BeamWeapon;
                BeamWeapon sbw = s as BeamWeapon;

                if (hbw.PowerConsumption < sbw.PowerConsumption)
                {
                    hovered.stat1Value.color = Color.green;
                    slotted.stat1Value.color = Color.red;
                }
                else if (hbw.PowerConsumption > sbw.PowerConsumption)
                {
                    hovered.stat1Value.color = Color.red;
                    slotted.stat1Value.color = Color.green;
                }

                if (hbw.DamageFalloff > sbw.DamageFalloff)
                {
                    hovered.stat2Value.color = Color.green;
                    slotted.stat2Value.color = Color.red;
                }
                else if (hbw.DamageFalloff < sbw.DamageFalloff)
                {
                    hovered.stat2Value.color = Color.red;
                    slotted.stat2Value.color = Color.green;
                }
            }
        }
    }

    private void Populate(InfoPanel panel, Item item)
    {
        // do not show the panel if it doesn't contain an item
        panel.gameObject.SetActive(item != null);
        if (item == null) return;

        panel.ResetFields();

        panel.border.color = Constants.RarityColor[item.Rarity];

        panel.title.text = item.name;
        panel.title.color = Constants.RarityColor[item.Rarity];

        panel.slot.text = item.Slot().ToString();

        if (item is WeaponBase)
        {
            WeaponBase w = item as WeaponBase;

            panel.dps.text = w.DPS().ToString("n0");

            StringBuilder sb = new StringBuilder();
            foreach (DamageTypes dt in w.Stats.Sort())
            {
                if (w.Stats.Damage(dt) > 0)
                {
                    sb.Append(dt.ToString());
                    sb.Append(", ");
                }
            }
            panel.damageType.text = sb.ToString().TrimEnd(',', ' ');

            panel.critChance.text = w.Stats.CritChance.ToString("p1");
            panel.critDamage.text = w.Stats.CritDamage.ToString("p0");

            if (item is ProjectileWeapon)
            {
                ProjectileWeapon p = item as ProjectileWeapon;

                panel.itemClass.text = "Projectile Weapon";

                panel.stat1Title.text = "Ammo:";
                panel.stat1Value.text = p.Ammunition.ToString("n0");
                panel.stat1Title.transform.parent.gameObject.SetActive(true);

                panel.stat2Title.text = "RoF:";
                panel.stat2Value.text = p.RateOfFire.ToString("n2");
                panel.stat2Title.transform.parent.gameObject.SetActive(true);

                panel.stat3Title.text = "Reload:";
                panel.stat3Value.text = p.ReloadTime.ToString("n3");
                panel.stat3Title.transform.parent.gameObject.SetActive(true);
            }
            else if (item is BeamWeapon)
            {
                BeamWeapon b = item as BeamWeapon;

                panel.itemClass.text = "Beam Weapon";

                panel.stat1Title.text = "Power:";
                panel.stat1Value.text = b.PowerConsumption.ToString("n0") +"/s";
                panel.stat1Title.transform.parent.gameObject.SetActive(true);

                panel.stat2Title.text = "Falloff:";
                panel.stat2Value.text = b.DamageFalloff.ToString("n2") + "m";
                panel.stat2Title.transform.parent.gameObject.SetActive(true);

                panel.stat3Title.text = "Tick Rate:";
                panel.stat3Value.text = b.TickRate.ToString("n0");
                panel.stat3Title.transform.parent.gameObject.SetActive(true);

            }
        }
    }

}
