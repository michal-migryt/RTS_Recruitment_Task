using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    private Animator animator;
    override protected void Awake() {
        base.Awake();

        animator = GetComponent<Animator>();
        animator.Play("StartState");
    }
    public override void Interact()
    {
        Debug.Log("Interact with chest");
        UIController.instance.ShowDecision(question);
    }
    public override void OnPositiveDecision()
    {
        audioSource.Play();
        animator.SetTrigger("Open");
        boxCollider.enabled = false;
    }
    public override void NewGameState()
    {
        animator.Play("StartState");
        boxCollider.enabled = true;
    }
}
