using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTexture : MonoBehaviour
{
    private MeshRenderer meshRenderer;                                  //create a variable for a mesh renderer component
    public Material[] materialTexture;                                  //create a material array which contain materials
    public GameObject changeObject;                                     //giving reference of the player whom color will change
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = changeObject.GetComponent<MeshRenderer>();       //getting mesh rendere component
    }

    //this function will change the material of our player
    public void ChangeMaterial()
    {
        int materialIndex = Random.Range(0, materialTexture.Length);   // pick random material to be on our player object;
        meshRenderer.material = materialTexture[materialIndex];        //here we apply material to the mesh of player 
    }
}