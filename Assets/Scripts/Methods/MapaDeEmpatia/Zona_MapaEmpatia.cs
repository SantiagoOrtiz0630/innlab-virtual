using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zona_MapaEmpatia : MonoBehaviour
{

    public Material postItMaterial;
    private LinkedList<Collider> assignedPostIts;

    void Awake()
    {
        assignedPostIts = new LinkedList<Collider>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach (Collider p in assignedPostIts)
        {
            PostIt postIt = p.GetComponent<PostIt>();

            if (!postIt.isInHand())
            {
                postIt.gameObject.transform.rotation = gameObject.transform.rotation;
                postIt.gameObject.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            }
            else
            {
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PostIt>())
        {
            other.GetComponent<PostIt>().changeMaterial(postItMaterial);
            assignedPostIts.AddLast(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PostIt>())
        {
            other.GetComponent<PostIt>().resetMaterial();
            other.gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
            assignedPostIts.Remove(other);
        }
    }
}
