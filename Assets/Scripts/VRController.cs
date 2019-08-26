using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class VRController : MonoBehaviour
{

    private SteamVR_Action_Boolean grabPinchAction = null;

    private SteamVR_Behaviour_Pose m_Pose = null;

    void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        grabPinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
    }

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        if (grabPinchAction.GetStateDown(m_Pose.inputSource))
        {
            GrabObject();
        }

        if (grabPinchAction.GetStateUp(m_Pose.inputSource))
        {
            ReleaseObject();
        }
    }

    public GameObject collidingObject;//To keep track of what objects have rigidbodies
    public GameObject objectInHand;//To track the object you're holding

    void OnTriggerEnter(Collider other)//Activate function in trigger zone, checking rigidbodies and ignoring if no rigidbodies 
    {
        if (!other.GetComponent<Rigidbody>())
        {
            return;
        }
        collidingObject = other.gameObject;//If rigidbody, then assign object to collidingObject variable
    }

    void OnTriggerExit(Collider other)
    {
        collidingObject = null;
    }

    private void GrabObject() // Picking up object and assigning objectInHand variable
    {
        if (collidingObject != null && collidingObject.GetComponent<PostIt>())
        {
            objectInHand = collidingObject;
            objectInHand.transform.SetParent(this.transform);
            objectInHand.GetComponent<Rigidbody>().isKinematic = true;
            objectInHand.GetComponent<PostIt>().setInHand(true);
        }
    }
       
    // Releasing object and disabling kinematic functionality so other forces can affect object
    private void ReleaseObject()
    {
        if (objectInHand != null && objectInHand.GetComponent<PostIt>())
        {
            objectInHand.GetComponent<Rigidbody>().isKinematic = false;
            objectInHand.transform.SetParent(null);
            objectInHand.GetComponent<PostIt>().setInHand(false);
            objectInHand = null;
        }
    }

}