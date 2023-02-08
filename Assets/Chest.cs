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
        animator.Play("");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void Interact()
    {
        
    }
}
