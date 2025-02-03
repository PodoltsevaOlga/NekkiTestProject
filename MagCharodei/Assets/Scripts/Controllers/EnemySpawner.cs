using System.Collections.Generic;
using Entities;
using EntityComponents;
using EntityViews;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controllers
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private int m_MaxEnemyCount;
        [SerializeField] private List<GameObject> m_EnemyPrefabs = new();
        [SerializeField] private Collider2D m_SceneBorders;
        [SerializeField] private float m_MaxDistanceToSpawn;

        private List<Creature> m_Enemies = new();

        private void Update()
        {
            List<Creature> enemiesTemp = new();
            foreach (var enemy in m_Enemies)
            {
                if (enemy.TryGetComponent<HealthComponent>()?.IsDead ?? true)
                {
                    GameObject.Destroy(enemy.View.gameObject);
                }
                else
                {
                    enemiesTemp.Add(enemy);
                }
            }

            m_Enemies = enemiesTemp;

            if (m_Enemies.Count < m_MaxEnemyCount)
            {
                for (int i = 0; i < m_MaxEnemyCount - m_Enemies.Count; ++i)
                {
                    var prefabId = Random.Range(0, m_EnemyPrefabs.Count);
                    var position = RandomCoordinates();
                    var newEnemy = GameObject.Instantiate(m_EnemyPrefabs[prefabId], position, Quaternion.identity);
                    
                    var newEnemyView = newEnemy.GetComponent<CreatureView>();
                    if (newEnemyView?.Data != null)
                        m_Enemies.Add(newEnemyView.Data);
                    
                    newEnemy.GetComponent<FollowAgent>()?.SetTarget(GameController.Instance.Player.View);
                }
            }
        }

        private Vector3 RandomCoordinates()
        {
            //не особо равномерный рандом, надо пересчитать
            var max = m_SceneBorders.bounds.max;
            var min = m_SceneBorders.bounds.min;
            float x = Random.Range(min.x - m_MaxDistanceToSpawn, max.x + m_MaxDistanceToSpawn);
            float y = 0.0f;
            if (x > min.x && x < max.x)
            {
                float yShift = Random.Range(0.0f, m_MaxDistanceToSpawn);
                if (Random.Range(-1, 1) < 0)
                {
                    y = min.y - yShift;
                }
                else
                {
                    y = max.y + yShift;
                }
            }
            else
            {
                y = Random.Range(min.y - m_MaxDistanceToSpawn, max.y + m_MaxDistanceToSpawn);
            }

            return new Vector3(x, y, 0.0f);
        }
    }
}