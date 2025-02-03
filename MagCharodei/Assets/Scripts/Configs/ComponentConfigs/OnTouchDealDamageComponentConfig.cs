using Entities;
using EntityComponents;
using UnityEngine;

namespace Configs.ComponentConfigs
{
    [CreateAssetMenu(fileName = "DealDamageOnTouch", menuName = "Configs/Components/Deal Damage On Touch")]
    public class OnTouchDealDamageComponentConfig : ComponentConfig
    {
        [SerializeField] private int m_InitialDamage;
        public int InitialDamage => m_InitialDamage;
        
        public override EntityComponent CreateComponent(Entity owner)
        {
            return new OnTouchDealDamageComponent(owner, m_InitialDamage);
        }
    }
}