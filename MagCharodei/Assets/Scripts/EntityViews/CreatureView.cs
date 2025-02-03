using Entities;

namespace EntityViews
{
    public class CreatureView : EntityView
    {
        public new Creature Data => (Creature)base.Data;

        protected override void CreateEntity()
        {
            m_Data = new Creature(this);
        }
        
        

    }
}