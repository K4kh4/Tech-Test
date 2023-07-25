using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{

    public event System.Action<Vector3> OnTouch;

    public event System.Action<Vector3> OnDrag;

    public event System.Action<float> OnZoom;

    private float _timeToHold = .15f;

    private float _timeHeld;
    private bool _holding
    {
        get
        {
            return _timeHeld > _timeToHold;
        }
    }

    private Vector3 _origin = Vector3.zero;
    Camera _mainCam;
    private Vector3 mousePosition => _mainCam.ScreenToWorldPoint(Input.mousePosition);
    void Start()
    {
        _mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        if (Input.GetMouseButtonDown(0))
        {
            OnTouch?.Invoke(mousePosition);
            _origin = mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            _timeHeld += Time.deltaTime;
            if (_holding)
            {
                OnDrag?.Invoke(_origin - mousePosition);

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _timeHeld = 0;
        }

        if (Input.GetMouseButtonDown(1))
        {
            _origin = mousePosition;
        }
        if (Input.GetMouseButton(1))
        {

            OnZoom?.Invoke(_origin.y - mousePosition.y);
        }

#endif
    }
}
