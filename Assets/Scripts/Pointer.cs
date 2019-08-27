using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Pointer : MonoBehaviour
{

    public float m_DefaultLenght = 5.0f;
    public GameObject m_Dot;
    public InputModule m_InputModule;

    private LineRenderer m_LineRenderer = null;

    private void Awake()
    {
        m_LineRenderer = GetComponent<LineRenderer>();  
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateLine();
    }

    private void UpdateLine()
    {
        //Use default or distance
        float targetLenght = m_DefaultLenght;

        //RayCast
        RaycastHit hit = CreateRayCast(targetLenght);

        //Default
        Vector3 endPosition = transform.position + (transform.forward * targetLenght);

        //or based on hit
        if (hit.collider != null)
        {
            endPosition = hit.point;
        }

        //set position of the dot
        m_Dot.transform.position = endPosition;

        //set line renderer
        m_LineRenderer.SetPosition(0, transform.position);
        m_LineRenderer.SetPosition(1, endPosition);
    }

    private RaycastHit CreateRayCast(float lenght)
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position, transform.forward);
        Physics.Raycast(ray, out hit, m_DefaultLenght);

        return hit;
    }


}
