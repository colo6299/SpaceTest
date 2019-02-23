using System;
using UnityEngine;

namespace Assets.Scripts.Entities
{
    public class Regenerator
    {

        public Action OnEmpty;
        public Action OnFull;

        /// <summary>
        /// The max value that can be reached
        /// 
        /// Can not be less than 0
        /// </summary>
        private float max;
        public float Max
        {
            get { return max; }
            set
            {
                if (value < 0)
                {
                    max = 0;
                }
                else
                {
                    max = value;
                }
            }
        }

        /// <summary>
        /// The current value
        /// 
        /// Can not be less than 0
        /// Can not be greater than Max
        /// </summary>
        private float value;
        public float Value
        {
            get { return value; }
            set
            {
                if (value > Max)
                {
                    this.value = Max;

                    if (OnFull != null)
                    {
                        OnFull.Invoke();
                    }
                }
                else if (value < 0)
                {
                    this.value = 0;

                    if (OnEmpty != null)
                    {
                        OnEmpty.Invoke();
                    }
                }
                else
                {
                    this.value = value;
                }
            }
        }

        /// <summary>
        /// The regeneration amount per second
        /// 
        /// This can be both a nagative or positive number
        /// </summary>
        public float Regen { get; set; }

        /// <summary>
        /// A cooldown, in seconds, that stops regeneration
        ///
        /// Can not be less than 0
        /// </summary>
        private float cooldown;
        public float Cooldown
        {
            get { return cooldown; }
            set
            {
                if (value < 0)
                {
                    cooldown = 0;
                }
                else
                {
                    cooldown = value;
                }
            }

        }

        /// <summary>
        /// the current cooldown timer
        /// 
        /// if the timer is 0 the cooldown is no longer in effect
        /// </summary>
        public float CurrentCooldown { get; private set; }
        
        public bool IsOnCooldown { get { return CurrentCooldown > 0; } }

        private float timeSinceLastRegenerate;

        /// <summary>
        /// Creates an instance of the regeneration class
        /// </summary>
        public Regenerator(float value, float max, float regen, float cooldown = 0)
        {
            Max = max;
            Value = value;
            Regen = regen;
            Cooldown = cooldown;

            timeSinceLastRegenerate = Time.time;
        }

        /// <summary>
        ///  regenerates resources. Does not regenerate if the cooldown is active
        /// </summary>
        public void Regenerate()
        {
            float timedelta = (Time.time - timeSinceLastRegenerate);

            if (IsOnCooldown)
            {
                CurrentCooldown = (CurrentCooldown-timedelta < 0) ? 0 : CurrentCooldown-timedelta;
            }
            else
            {
                Value += Regen * timedelta;
            }

            timeSinceLastRegenerate = Time.time;
        }

        /// <summary>
        /// Begins a new cooldown cycle
        /// </summary>
        public void BeginOrResetCooldown()
        {
            CurrentCooldown = Cooldown;
        }
    }
}
