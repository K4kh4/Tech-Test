using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : Singleton<InputManager>
{

    public event System.Action<Vector3> OnTouch;
        public event System.Action<Vector3> OnTouchEnd;


    public event System.Action<Vector3> OnDrag;

    public event System.Action<float> OnZoom;

    private float _timeToHold = .1f;
    private int UILayer;

    private float _timeHeld;
    private bool _holding
    {
        get
        {
            return _timeHeld > _timeToHold;
        }
    }

    private Vector3 _origin = Vector3.zero;
    private float _startZoom;
    Camera _mainCam;
    private Vector3 mousePosition => _mainCam.ScreenToWorldPoint(Input.mousePosition);
    private Vector3 touchPosition => _mainCam.ScreenToWorldPoint(Input.GetTouch(0).position);
    void Start()
    {
        UILayer = LayerMask.NameToLayer("UI");
        _mainCam = Camera.main;
        
    }

    void Update()
    {
        if (IsPointerOverUIElement())
            return;
        // #if UNITY_EDITOR

        //         if (Input.GetMouseButtonDown(0))
        //         {
        //             OnTouch?.Invoke(mousePosition);
        //             _origin = mousePosition;
        //         }
        //         if (Input.GetMouseButton(0))
        //         {
        //             _timeHeld += Time.deltaTime;
        //             if (_holding)
        //             {
        //                 OnDrag?.Invoke(_origin - mousePosition);

        //             }
        //         }
        //         if (Input.GetMouseButtonUp(0))
        //         {
        //             _timeHeld = 0;
        //         }

        //         if (Input.GetMouseButtonDown(1))
        //         {
        //             _origin = mousePosition;
        //         }
        //         if (Input.GetMouseButton(1))
        //         {

        //             OnZoom?.Invoke(_origin.y - mousePosition.y);
        //         }

        // #endif
        if (Input.touchCount<1)
        return;
        
        if (Input.GetTouch(0).phase == TouchPhase.Began)
        {
            OnTouch?.Invoke(touchPosition);
            _origin = touchPosition;
        }
        if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(0).phase == TouchPhase.Stationary)
        {
             _timeHeld += Time.deltaTime;
             OnDrag?.Invoke(_origin - touchPosition);   

        }
         if (Input.GetTouch(0).phase == TouchPhase.Ended && Input.touches.Length <2)
        {
           if (!_holding)
                OnTouchEnd?.Invoke(touchPosition);
           _timeHeld=0;
        }
        
        if (Input.touches.Length > 1)
        {
            if (Input.GetTouch(1).phase == TouchPhase.Began)
            {
                _startZoom = (touchPosition - _mainCam.ScreenToWorldPoint(Input.GetTouch(1).position)).magnitude;
                //_origin = touchPosition;
            }
            OnZoom?.Invoke(_startZoom - (touchPosition - _mainCam.ScreenToWorldPoint(Input.GetTouch(1).position)).magnitude);
        }

    }

    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    private bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults)
    {
        for (int index = 0; index < eventSystemRaysastResults.Count; index++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults[index];
            if (curRaysastResult.gameObject.layer == UILayer)
                return true;
        }
        return false;
    }

    static List<RaycastResult> GetEventSystemRaycastResults()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, raysastResults);
        return raysastResults;
    }
}
