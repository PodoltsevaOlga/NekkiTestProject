using EntityComponents;
using EntityViews;

namespace Entities
{
    public class Creature : Entity
    {
        public Faction Faction { get; private set; }
        
        //вообще это все переезжает в отдельную систему инвентаря и слоты с экипировкой, как только все усложняется
        //а пока просто будет торчать в компоненте и экипироваться прям через кричу
        public WeaponHolderComponent WeaponHolderComponent { get; protected set; }
        
        public Creature(CreatureView view) : base(view)
        {
        }

        public void SetFaction(Faction faction)
        {
            Faction = faction;
        }

        protected override void OnEntityCreated()
        {
            base.OnEntityCreated();
            WeaponHolderComponent = new WeaponHolderComponent(this);
            AddComponent(WeaponHolderComponent);
        }
    }
}