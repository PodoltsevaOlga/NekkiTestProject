using Configs;
using Entities;
using EntityComponents;
using UnityEngine;

namespace EntityViews
{
    public class EntityView : MonoBehaviour
    {
        protected Entity m_Data;
        public Entity Data => m_Data;

        [SerializeReference] private EntityConfig m_EntityConfig; 

        protected virtual void CreateEntity()
        {
            m_Data = new Entity(this);
        }

        protected virtual void OnEntityCreated()
        {
            m_EntityConfig.SetupConfig(m_Data);
        }

        private void Awake()
        {
            CreateEntity();
            OnEntityCreated();
        }

        private void Start()
        {
            Data.OnStart();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            var entityView = collision.collider.gameObject.GetComponent<EntityView>();
            if (entityView?.Data != null)
            {
                var components = Data.GetComponents<IOnTouch>();
                foreach (var comp in components)
                {
                    comp.OnTouch(entityView.Data);
                }
            }
        }

        private void Update()
        {
            Data.OnUpdate();
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            Rotation = rotation;
        }

        
        public Vector3 Position => transform.position;
        
        //раз мы в 2д, то надо спрайт заменять при повороте. Но при этом нам важно помнить, в какую сторону мы стреляем. так что запоминаем
        public Quaternion Rotation { get; private set; }

        public void OnDestroy()
        {
            Debug.Log($"Object {this} destroyed");
        }
    }
}