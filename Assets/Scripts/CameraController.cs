using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform[] focusObjects;                                //this is a transform array which contain the player and enemy in the game;
                                                                    //for this we can use prefab to instantiate enemy in our scene we have to create fix number of enemy in game
    public Vector3 offset = new Vector3(-4.4f, 6.23f, -15.6f);      //this offset is used for camera adjustment
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        if (focusObjects.Length == 0)                       //if there is no object in transform then do nothing mean there is no need to focus on any object
        {
            return;
        }
        Vector3 newPos = GetMidPoint() + offset;           //now we store that mid point of the bound and add offset in it to set our camera position in new vector 3   
        transform.position = newPos;                       //then we assign the position to our camera in Last Update;


    }
    Vector3 GetMidPoint()
    {
        if (focusObjects.Length == 1)                      //if the length of tranform array is one then it will focus on there object;
        {
            return focusObjects[0].position;               //here it will return that object tranform component values
        }
        var bound = new Bounds(focusObjects[0].position, Vector3.zero);     //here this thing create a box around first tranform object; 
        for (int i = 0; i < focusObjects.Length; i++)       //this looop will encapsulate all object
        {
            bound.Encapsulate(focusObjects[i].position);    //here it will check position of all transform array object an Encapsulate or create a box around then 
                                                            //simply it create a box according to transform array object in which all that element are place
        }
        return bound.center;                                //after the box is create we simply return the center point of that box where our camera will focus or look at;

    }
}
