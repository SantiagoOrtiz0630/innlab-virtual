using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{

    public Camera m_PointerCamera;

    private SteamVR_Input_Sources m_TargetSource;
    private SteamVR_Action_Boolean m_ClickAction;

    private GameObject m_CurrentObject = null;
    private PointerEventData m_Data = null;

    protected override void Awake()
    {
        base.Awake();

        m_TargetSource = SteamVR_Input_Sources.RightHand;
        m_ClickAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("InteractUI");

        m_Data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        //Reset Data, set camera

        m_Data.Reset();
        m_Data.position = new Vector2(m_PointerCamera.pixelWidth/2, m_PointerCamera.pixelHeight/2);

        //RayCast
        eventSystem.RaycastAll(m_Data, m_RaycastResultCache);
        m_Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        m_CurrentObject = m_Data.pointerCurrentRaycast.gameObject;

        //Clear
        m_RaycastResultCache.Clear();

        //Hover
        HandlePointerExitAndEnter(m_Data, m_CurrentObject);

        //Press
        if (m_ClickAction.GetStateDown(m_TargetSource))
        {
            ProcessPress(m_Data);
        }  

        //Release
        if (m_ClickAction.GetStateUp(m_TargetSource))
        {
            ProcessReleased(m_Data);
        }
    }

    public PointerEventData GetData()
    {
        return m_Data;
    }

    private void ProcessPress(PointerEventData data)
    {
        //set raycast
        data.pointerPressRaycast = data.pointerCurrentRaycast;

        //Check for object hit, the down handler, call
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(m_CurrentObject, data, ExecuteEvents.pointerDownHandler);

        //if whe dont have down handler, try and get click handler
        if (newPointerPress == null)
        {
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject); 
        }

        //set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = m_CurrentObject;

    }

    private void ProcessReleased(PointerEventData data)
    {
        //Execute pointer up
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

        //Check for click handler
        GameObject poiterUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(m_CurrentObject);

        //check if actual
        if (data.pointerPress == poiterUpHandler)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }

        //clear selected gameobject
        eventSystem.SetSelectedGameObject(null);

        //Reset data
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
    }
}
