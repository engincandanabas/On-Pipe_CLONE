using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    #region SerializeField
    [SerializeField] private GameObject _panel;
    [SerializeField] private TextMeshProUGUI _distance;
    [SerializeField] private RectTransform health_Bar;
    [SerializeField] private TextMeshProUGUI highScoreText;
    #endregion
    #region Unity
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        highScoreText.text = PlayerPrefs.GetFloat("HighScore").ToString("F1");
    }

    #endregion
    public void OpenGameOverPanel()
    {
        if(_panel !=null)_panel.SetActive(true);
        _distance.gameObject.SetActive(false);
    }
    public void SetDistanceValue(float value)
    {
        _distance.text=value.ToString("f1");
    }
    public void SetPlayerHealth(float health)
    {
        health_Bar.localScale = new Vector3(health/10f, 1.0f, 1.0f);
    }
}
