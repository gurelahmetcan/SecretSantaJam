using System;
using System.Collections;
using System.Collections.Generic;
using SantaProject;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI totalKillText;

    public void OnEnable()
    {
        totalKillText.text = $"Total Kill: {GameManager.Instance.killedEnemyCount}";
    }

    public void OnRestartPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMenuPressed()
    {
        SceneManager.LoadScene(0);
    }
}
