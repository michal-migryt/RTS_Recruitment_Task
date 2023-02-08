using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected string question;
    [SerializeField] protected AudioSource audioSource;
    // List in case object has multiple mesh renderers like chest game object
    private List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    private Color normalColor, highlightColor = Color.white;

    protected virtual void Awake() {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderers.Add(meshRenderer);
        MeshRenderer[] tempMeshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer mR in tempMeshRenderers)
        {
            meshRenderers.Add(mR);
        }
        normalColor = meshRenderer.material.color;
    }

    private void OnMouseEnter() {
        foreach(MeshRenderer meshRenderer in meshRenderers)
            meshRenderer.material.color = highlightColor;

        GameController.instance.hoveredInteractable = this;
    }

    private void OnMouseExit() {
        foreach(MeshRenderer meshRenderer in meshRenderers)
            meshRenderer.material.color = normalColor;
        
        if(GameController.instance.hoveredInteractable == this)
            GameController.instance.hoveredInteractable = null;
    }
   
    public virtual void Interact(){}
    public virtual void OnPositiveDecision(){}
}
