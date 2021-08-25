using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace Unity.FPS.Game
{
    public class AutoRegenHealth : MonoBehaviour
    {
        [Tooltip("Time for the player to begin auto regen")]
        public float RegenTimout = 3f;

        public float RegenInterval = 1/30;

        public float HealthAmount = 2f;

        public Health Health { get; private set; }
        
        protected float TimeLastHit = Mathf.NegativeInfinity;

        protected float CurrentHealth = 0f;

        protected float TimeLastHeal = Mathf.NegativeInfinity;

        void Awake()
        {
            // find the health component either at the same level, or higher in the hierarchy
            Health = GetComponent<Health>();
            if (!Health)
            {
                Health = GetComponentInParent<Health>();
            }
            CurrentHealth = Health.CurrentHealth;
        }
        
        void Update()
        {
            if(CurrentHealth > Health.CurrentHealth)
            {
                TimeLastHit = Time.time;
            }
            if(Health.CanHeal() && (Time.time - RegenTimout) >= TimeLastHit) 
            {
                /* if((Time.time - RegenInterval) > TimeLastHeal) // Revisar lo de time delta
                { 
                    Health.Heal(HealthAmount);
                    TimeLastHeal = Time.time;
                } */
                Health.Heal(HealthAmount * Time.deltaTime);
            }
            CurrentHealth = Health.CurrentHealth;
        }
    }
}