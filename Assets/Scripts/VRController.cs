using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{

    //Actions, values of SteamVR

    private SteamVR_Action_Boolean grabPinchAction = null, grabGripAction = null;
    private SteamVR_Behaviour_Pose m_Pose = null;

    public SteamVR_Input_Sources m_HandType;
    public GameObject pointerObject;

    void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        grabPinchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabPinch");
        grabGripAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("GrabGrip");
    }

    // Use this for initialization
    void Start()
    {

    }

    void Update()
    {
        //for both hands
        if (grabPinchAction.GetStateDown(m_Pose.inputSource))
        {
            GrabPostIt();
        }

        if (grabPinchAction.GetStateUp(m_Pose.inputSource))
        {
            ReleasePostIt();
        }

        //If hand is the right hand, see the SteamVR input UI
        if (grabGripAction.GetStateDown(m_Pose.inputSource))
        {
            SetPointerState(true);
        }

        if (grabGripAction.GetStateUp(m_Pose.inputSource))
        {
            SetPointerState(false);
        }

    }

    //Post It Interactions

    private GameObject collidingObject;//To keep track of what objects have rigidbodies
    private GameObject objectInHand;//To track the object you're holding

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

    private void GrabPostIt() // Picking up object and assigning objectInHand variable
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
    private void ReleasePostIt()
    {
        if (objectInHand != null && objectInHand.GetComponent<PostIt>())
        {
            objectInHand.GetComponent<Rigidbody>().isKinematic = false;
            objectInHand.transform.SetParent(null);
            objectInHand.GetComponent<PostIt>().setInHand(false);
            objectInHand = null;
        }
    }

    private void SetPointerState(bool b)
    {
        if (pointerObject != null)
        {
            pointerObject.SetActive(b);
        }
    }

}