using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Help : MonoBehaviour
{

    public GameObject camara;
    public GameObject helpView;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (camara != null && helpView != null)
        {
            Vector3 camaraVector = new Vector3(camara.transform.position.x, 0f, camara.transform.position.z);
            Vector3 helpVector = new Vector3(transform.position.x, 0f, transform.position.z);

            if (Vector3.Distance(camaraVector, helpVector) < 0.5f)
            {
                Debug.Log("In");
            }
        }
    }
}
