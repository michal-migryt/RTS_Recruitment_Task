using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GameController : MonoBehaviour
{
    public static GameController instance;
    [HideInInspector] public bool hasKey = false;
    [HideInInspector] public Interactable hoveredInteractable;
    private Interactable selectedInteractable;
    public InputActions _InputActions{get; private set;}
    private UIController uIController;
    private float timePassed=0f, record=0f;
    private InteractableRandomizer interactableRandomizer;
    public delegate void StartGameDelegate();
    public StartGameDelegate startGameDelegate;
    [SerializeField] private AudioSource backgroundMusicSource, victorySoundSource;
    private void Awake() {
        if (instance != null)
            Destroy(this);
        else
            instance = this;

        _InputActions = new InputActions();
        _InputActions.Player.Enable();
        _InputActions.Player.Interact.performed += OnMouseClick;
        startGameDelegate += GameStart;
    }
    // Start is called before the first frame update
    void Start()
    {
        uIController = UIController.instance;
        uIController.InitializeUI();
        startGameDelegate += uIController.OnGameStart;
        interactableRandomizer = FindObjectOfType<InteractableRandomizer>();
        interactableRandomizer.SpawnDoors();
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ManageTime();
    }
    private void OnMouseClick(InputAction.CallbackContext context)
    {
        if(hoveredInteractable != null)
        {
            selectedInteractable = hoveredInteractable;
            selectedInteractable.Interact();
        }
    }
    private void ManageTime()
    {
        timePassed += Time.deltaTime;
        uIController.UpdateTimer(timePassed);
    }
    public void GameStart()
    {
        interactableRandomizer.RandomizeInteractables();
        timePassed = 0f;
        Time.timeScale = 1f;
        victorySoundSource.Stop();
        backgroundMusicSource.Play();
    }
    public void GameOver()
    {
        uIController.OnGameOver(timePassed, record);
        if(timePassed < record || record == 0f)
            record = timePassed;
        hasKey = false;
        Time.timeScale = 0;
        backgroundMusicSource.Stop();
        victorySoundSource.Play();
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
