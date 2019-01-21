using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : EntityInfo {

    private bool tookDamage = false;

    private float wait = 0;

    public override void Update()
    {
        base.Update();

        if (tookDamage)
        {
            wait += Time.deltaTime;

            if (wait >= 5)
            {
                tookDamage = false;
                wait = 0;
            }
        }

        if (!tookDamage && Health < MaxHealth)
        {
            Health += 500 * Time.deltaTime;

            if (Health > MaxHealth)
            {
                Health = MaxHealth;
            }

            EntityHpBar bar = GetComponentInChildren<EntityHpBar>();

            if (bar != null) bar.UpdateBar(Health, MaxHealth);
        }
    }

    public override void TakeDamage(WeaponInfo info)
    {
        base.TakeDamage(info);

        tookDamage = true;
    }

}
