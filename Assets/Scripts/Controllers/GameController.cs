using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool hasKey = false;
    public Interactable hoveredInteractable;
    private Interactable selectedInteractable;
    public InputActions _InputActions{get; private set;}
    private UIController uIController;
    private float timePassed=0f;
    private float record=0f;
    private void Awake() {
        if (instance != null)
            Destroy(this);
        else
            instance = this;

        _InputActions = new InputActions();
        _InputActions.Player.Enable();
        _InputActions.Player.Interact.performed += OnMouseClick;
        
        
    }
    // Start is called before the first frame update
    void Start()
    {
        uIController = UIController.instance;
        // uIController.InitializeUI();
        // Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        // uIController.UpdateTimer(timePassed);
    }
    private void OnMouseClick(InputAction.CallbackContext context)
    {
        if(hoveredInteractable != null)
        {
            selectedInteractable = hoveredInteractable;
            selectedInteractable.Interact();
        }
    }
    public void GameStart()
    {
        timePassed = 0f;
        Time.timeScale = 1;
    }
    public void GameOver()
    {
        uIController.OnGameOver();
        Time.timeScale = 0;
    }
    public void PositiveDecision()
    {
        selectedInteractable.OnPositiveDecision();
        selectedInteractable = null;
    }
    public void NegativeDecision()
    {
        selectedInteractable = null;
    }
}
