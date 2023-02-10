using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    protected override void Awake()
    {
        base.Awake();
        boxCollider = GetComponent<BoxCollider>();
    }
    public override void Interact()
    {
        Debug.Log("Interact with key");
        UIController.instance.ShowDecision(question);
    }
    public override void OnPositiveDecision()
    {
        GameController.instance.hasKey = true;
        audioSource.Play();
        meshRenderers[0].enabled = false;
        boxCollider.enabled = false;

    }
    public override void NewGameState()
    {

        meshRenderers[0].enabled = true;
        boxCollider.enabled = true;
    }
}
