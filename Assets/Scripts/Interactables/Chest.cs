using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    private BoxCollider boxCollider;
    private Animator animator;
    override protected void Awake() {
        base.Awake();
        boxCollider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
        animator.Play("StartState");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update() {
        Debug.Log(transform.position);
    }
    public override void Interact()
    {
        Debug.Log("interacted");
        // OnPositiveDecision();
    }
    public override void OnPositiveDecision()
    {
        animator.SetTrigger("Open");
        boxCollider.enabled = false;
    }
    public override void NewGameState()
    {
        // Randomize position
        boxCollider.enabled = true;
    }
}
