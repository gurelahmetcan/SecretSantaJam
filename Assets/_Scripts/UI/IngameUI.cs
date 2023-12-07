using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace SantaProject
{
    public class IngameUI : MonoBehaviour
    {
        [SerializeField] private Image dashBar;

        private void Awake()
        {
            dashBar.fillAmount = 0f;
        }

        private void Start()
        {
            EventManager.Instance.onDashPressed += OnDashPressed;
        }

        private void OnDestroy()
        {
            EventManager.Instance.onDashPressed -= OnDashPressed;
        }

        private void OnDashPressed(float dashCooldown)
        {
            dashBar.fillAmount = 1f;
            dashBar.DOFillAmount(0f, dashCooldown);
        }
    }

}
