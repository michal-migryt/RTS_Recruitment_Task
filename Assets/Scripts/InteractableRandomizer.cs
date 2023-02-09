using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRandomizer : MonoBehaviour
{
    enum Wall{UPPER, BOTTOM, LEFT, RIGHT}
    
    [SerializeField] private float xMin, xMax, zMin, zMax;
    [SerializeField] private Chest chest;
    [SerializeField] private Key key;
    [SerializeField] private Door[] doors;
    [SerializeField] private GameObject doorPrefab;
    

    public void RandomizeInteractables()
    {
        chest.transform.position = RandomPosition();
        chest.transform.Rotate(0, RandomAngle(), 0, Space.World);
        key.transform.Rotate(0, RandomAngle(), 0, Space.World);
    }
    private Vector3 RandomPosition()
    {
        float x = Random.Range(xMin, xMax);
        float z = Random.Range(zMin, zMax);
        return new Vector3(x, 0, z);
    }
    private float RandomAngle()
    {
        float angle = Random.Range(1, 361);
        return angle;
    }

}
