using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    public override void Interact()
    {
        Debug.Log("Interact with key");
        UIController.instance.ShowDecision(question);
    }
    public override void OnPositiveDecision()
    {
        GameController.instance.hasKey = true;
        gameObject.SetActive(false);
    }
    public override void NewGameState()
    {
        gameObject.SetActive(true);
    }
}
