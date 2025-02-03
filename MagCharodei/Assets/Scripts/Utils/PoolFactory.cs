using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class PoolFactory
    {
        private readonly Dictionary<GameObject, GameObjectsPool> m_Pools = new();

        public GameObject GetOrCreateObject(GameObject prefab)
        {
            if (m_Pools.TryGetValue(prefab, out var pool))
            {
                return pool.GetOrCreateObject();
            }
            else
            {
                var newPool = new GameObjectsPool(prefab);
                m_Pools.Add(prefab, newPool);
                return newPool.GetOrCreateObject();
            }
        }
        

        public void ReleaseObject(GameObject go, GameObject prefab)
        {
            if (!m_Pools.TryGetValue(prefab, out var pool))
            {
                //непонятно, шо за го, не мы спавнили, не нам убирать, просто задестроим на всякий
                GameObject.Destroy(go);
            }
            else
            {
                pool.ReleaseObject(go);
            }
        }

        public void Clear()
        {
            m_Pools.Clear();
        }
    }
}