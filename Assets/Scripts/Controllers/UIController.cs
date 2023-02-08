using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject decisionPanel;
    [SerializeField] private TextMeshProUGUI decisionText;
    [SerializeField] private GameObject informationPanel;
    [SerializeField] private TextMeshProUGUI informationText;
    [SerializeField] private TextMeshProUGUI timerText;
    public static UIController instance;
    private void Awake() {
        if(instance != null)
            Destroy(this);
        else
            instance = this;
    }
    public void InitializeUI()
    {
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        CloseDecision();
        CloseInformation();
    }
    public void UpdateTimer(float seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        timerText.text = timeSpan.ToString("mm':'ss':'ff");
    }
    public void ShowDecision(string question)
    {
        decisionText.text = question;
        decisionPanel.SetActive(true);
    }
    public void CloseDecision()
    {
        decisionPanel.SetActive(false);
    }
    public void ShowInformation(string information)
    {
        informationText.text = information;
        informationPanel.SetActive(true);
    }
    public void CloseInformation()
    {
        informationPanel.SetActive(false);
    }
    public void OnGameOver()
    {
        gameOverPanel.SetActive(true);
    }
    public void OnGameStart()
    {
        gameOverPanel.SetActive(false);
        startPanel.SetActive(false);
    }
    public void OnYesButton()
    {
        GameController.instance.PositiveDecision();
        CloseDecision();
    }
    public void OnNoButton()
    {
        GameController.instance.NegativeDecision();
        CloseDecision();
    }
    public void OnOkButton()
    {
        GameController.instance.NegativeDecision();
        CloseInformation();
    }
}
