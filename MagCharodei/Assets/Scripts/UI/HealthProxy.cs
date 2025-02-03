using System;
using Controllers;
using EntityViews;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthProxy : MonoBehaviour
    {
        //Text UI object
        [SerializeField] private GameObject HealthBarPrefab;
        [SerializeField] private Vector3 m_Offset;

        private EntityView m_Owner;
        private TMP_Text m_HealthText;
        private RectTransform m_HealthTransform;

        public void SpawnHealthBar()
        {
            m_Owner = GetComponent<EntityView>();
            var canvas = GameController.Instance.ScreenSpaceCameraCanvas;
            var go = GameObject.Instantiate(HealthBarPrefab, canvas.transform);
            m_HealthText = go.GetComponent<TMP_Text>();
            m_HealthTransform = go.GetComponent<RectTransform>();
            SetHealthPosition();
        }

        private void Update()
        {
            SetHealthPosition();
        }

        private void SetHealthPosition()
        {
            var position = m_Owner.Position + m_Offset;
            m_HealthTransform.transform.position = position;
        }

        public void SetHealth(int maxHealth, int currentHealth)
        {
            m_HealthText.text = $"{currentHealth} / {maxHealth}";
        }

        private void OnDisable()
        {
            if (!m_HealthText.IsDestroyed())
            {
                m_HealthText.gameObject.SetActive(false);
            }
        }
    }
}