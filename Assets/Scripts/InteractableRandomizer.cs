using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableRandomizer : MonoBehaviour
{
    //clockwise order of walls
    enum Wall{UPPER, RIGHT, BOTTOM, LEFT}
    
    [SerializeField] private float xMin, xMax, zMin, zMax;
    [SerializeField] private Chest chest;
    [SerializeField] private Key key;
    [SerializeField] private GameObject doorPrefab;
    [SerializeField] private int doorAmount;
    private List<Door> doorList = new List<Door>();
    private List<Door> placedDoors = new List<Door>();

    public void SpawnDoors()
    {
        for(int i=0; i< doorAmount; i++)
        {
            GameObject newDoor = Instantiate(doorPrefab, Vector3.zero, doorPrefab.transform.rotation);
            Debug.Log(newDoor.transform.rotation);
            doorList.Add(newDoor.GetComponent<Door>());
        }
    }
    public void RandomizeInteractables()
    {
        chest.transform.position = RandomPosition(chest.GetMeasurements());
        chest.transform.Rotate(0, RandomAngle(), 0, Space.World);
        key.transform.Rotate(0, RandomAngle(), 0, Space.World);
        placedDoors.Clear();
        foreach(Door door in doorList)
        {
            RandomizeDoor(door);
        }
    }
    private Vector3 RandomPosition(Vector3 objectMeasures)
    {
        float x = Random.Range(xMin + objectMeasures.x, xMax - objectMeasures.x);
        float z = Random.Range(zMin + objectMeasures.z, zMax - objectMeasures.z);
        return new Vector3(x, 0, z);
    }
    private void RandomizeDoor(Door door)
    {
        bool foundValidPosition = false;
        
        // depth is y component because of twisted door's prefab rotation or something
        float doorWidth = door.GetMeasurements().x, doorDepth = door.GetMeasurements().y/1.5f, doorLength = door.GetMeasurements().z;
        Debug.Log(doorDepth);
        while(!foundValidPosition)
        {
            int wallNumber = Random.Range(0, 4);
            float x, z;
            Wall chosenWall = (Wall) wallNumber;
            Vector3 potentialPosition;
            switch (chosenWall)
            {
                case Wall.UPPER:
                z = zMax - doorDepth;
                x = Random.Range(xMin + doorWidth, xMax - doorWidth);
                break;
                case Wall.RIGHT:
                x = xMax - doorDepth;
                z = Random.Range(zMin + doorWidth, zMax - doorWidth);
                break;
                case Wall.BOTTOM:
                z = zMin + doorDepth;
                x = Random.Range(xMin + doorWidth, xMax - doorWidth);
                break;
                case Wall.LEFT:
                x = xMin + doorDepth;
                z = Random.Range(zMin + doorWidth, zMax - doorWidth);
                break;
                default:
                z = zMax - doorDepth;
                x = Random.Range(xMin + doorWidth, xMax - doorWidth);
                break;
            }

            potentialPosition = new Vector3(x,doorLength, z);
            foundValidPosition = true;
            foreach(Door placedDoor in placedDoors)
            {
                if(Vector3.Distance(placedDoor.transform.position, potentialPosition) <= doorWidth)
                    foundValidPosition = false;
            }
            
            if(foundValidPosition)
            {
                door.transform.position = potentialPosition;
                // reset rotation
                door.transform.rotation = doorPrefab.transform.rotation;
                // wallnumber*90 because door on upper wall need to have rotation of 0 and after going clockwise from upper wall door need to be rotated by 90 degrees on Z axis of rotation
                door.transform.Rotate(0, 0, wallNumber*90, Space.Self);
                placedDoors.Add(door);
            }
        }
        

    }
    private float RandomAngle()
    {
        float angle = Random.Range(1, 361);
        return angle;
    }

}
