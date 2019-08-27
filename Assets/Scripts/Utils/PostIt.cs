using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostIt : MonoBehaviour
{

    public Material prefabMaterial;

    private bool inHand;

    private void Awake()
    {
        inHand = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeMaterial(Material newMaterial)
    {
        this.GetComponentsInChildren<Renderer>()[0].material = newMaterial;
    }

    public void resetMaterial()
    {
        this.GetComponentsInChildren<Renderer>()[0].material = prefabMaterial;
    }

    public void setInHand(bool inHand)
    {
        this.inHand = inHand;
    }

    public bool isInHand()
    {
        return inHand;
    }
}
