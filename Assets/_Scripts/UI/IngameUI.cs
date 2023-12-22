using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace SantaProject
{
    public class IngameUI : MonoBehaviour
    {
        [SerializeField] private Image dashBar;
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private TextMeshProUGUI waveNumberText;

        private bool waveBreak;
        private float timeRemaining = 5f;
        
        private void Awake()
        {
            dashBar.fillAmount = 0f;
        }

        private void Start()
        {
            EventManager.Instance.onDashPressed += OnDashPressed;
            EventManager.Instance.onWaveEnd += OnWaveEnd;
            waveNumberText.text = $"WAVE: 1";
        }

        private void Update()
        {
            if (waveBreak)
            {
                timeRemaining -= Time.deltaTime;
                waveText.text = $"New Wave in {timeRemaining:0}";
            }

            if (timeRemaining <= 0 && waveBreak)
            {
                waveBreak = false;
                waveText.gameObject.SetActive(false);
                timeRemaining = 5f;
                EventManager.Instance.onWaveBreakEnds.Invoke();
            }
        }

        private void OnDestroy()
        {
            EventManager.Instance.onDashPressed -= OnDashPressed;
            EventManager.Instance.onWaveEnd -= OnWaveEnd;
        }

        private void OnDashPressed(float dashCooldown)
        {
            dashBar.fillAmount = 1f;
            dashBar.DOFillAmount(0f, dashCooldown);
        }

        private void OnWaveEnd(int wave)
        {
            waveBreak = true;
            waveText.gameObject.SetActive(true);
            waveNumberText.text = $"WAVE: {wave}";
        }
    }

}
