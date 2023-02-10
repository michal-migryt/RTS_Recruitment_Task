using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Interactable : MonoBehaviour
{
    [SerializeField] protected string question;
    [SerializeField] protected AudioSource audioSource;
    // List in case object has multiple mesh renderers like chest game object
    protected List<MeshRenderer> meshRenderers = new List<MeshRenderer>();
    protected BoxCollider boxCollider;
    private Color normalColor, highlightColor = Color.white;

    protected virtual void Awake() {
        audioSource = GetComponent<AudioSource>();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderers.Add(meshRenderer);
        MeshRenderer[] tempMeshRenderers = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer mR in tempMeshRenderers)
        {
            meshRenderers.Add(mR);
        }
        normalColor = meshRenderer.material.color;
        boxCollider = GetComponent<BoxCollider>();
    }
    private void Start() {
        GameController.instance.startGameDelegate += NewGameState;
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
    public Vector3 GetMeasurements()
    {
        return boxCollider.size * transform.localScale.x;
    }
   
    public virtual void Interact(){}
    public virtual void OnPositiveDecision(){}
    public virtual void NewGameState(){}
}
