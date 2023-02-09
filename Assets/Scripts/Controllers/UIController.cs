using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject startPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI currentTimeText;
    [SerializeField] private TextMeshProUGUI recordTimeText;
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
        timerText.text = ConvertToTimeString(seconds);
    }
    private string ConvertToTimeString(float seconds)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
        string result = timeSpan.ToString("mm':'ss':'ff");
        return result;
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
    public void OnGameOver(float currentSeconds, float recordSeconds)
    {
        gameOverPanel.SetActive(true);
        currentTimeText.text = ConvertToTimeString(currentSeconds);
        if(recordSeconds != 0f)
            recordTimeText.text = ConvertToTimeString(recordSeconds);
        else
        {
            recordTimeText.text = "No record";
            recordTimeText.fontStyle = FontStyles.Strikethrough;
        }
        if(currentSeconds < recordSeconds)
            recordTimeText.fontStyle = FontStyles.Strikethrough;
            
    }
    public void OnGameStart()
    {
        GameController.instance.GameStart();
        gameOverPanel.SetActive(false);
        startPanel.SetActive(false);
        CloseDecision();
        CloseInformation();
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
