using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private string information;
    public override void Interact()
    {
        Debug.Log("Interact with door");
        if(GameController.instance.hasKey)
            UIController.instance.ShowDecision(question);
        else
            UIController.instance.ShowInformation(information);
    }
    public override void OnPositiveDecision()
    {
        audioSource.Play();
        GameController.instance.GameOver();
    }
}
