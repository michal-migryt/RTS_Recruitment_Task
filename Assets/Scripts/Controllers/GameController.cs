using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public bool hasKey = false;
    public Interactable hoveredInteractable;
    private Interactable selectedInteractable;
    public InputActions _InputActions{get; private set;}
    private UIController uIController;
    private float timePassed=0f;
    private void Awake() {
        if (instance != null)
            Destroy(this);
        else
            instance = this;

        _InputActions = new InputActions();
        _InputActions.Player.Enable();
        
    }
    // Start is called before the first frame update
    void Start()
    {
        uIController = UIController.instance;
    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        // uIController.UpdateTimer(timePassed);
    }
    private void OnMouseClick()
    {
        selectedInteractable = hoveredInteractable;
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
