using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector2 minMaxZoom = new Vector2(3, 6);

    Camera _cam;
    private float _speed = 10f;

    private float _minX = -10, _maxX= 10,
                    _minY =-10, _maxY = 10;

    private SpriteRenderer _BackGround;
    private Vector3 _targetPos;
    private float _targetZoom;
    private void Awake()
    {
        _cam = GetComponent<Camera>();
        GameManager.Instance.OnLevelLoaded +=OnLevelLoaded;
        _targetPos = transform.position;
        _targetZoom = _cam.orthographicSize;
        InputManager.Instance.OnDrag += Move;
        InputManager.Instance.OnZoom += Zoom;
      
    }

    public void OnLevelLoaded(SpriteRenderer bg)
    {
        _BackGround = bg;
        UpdateBounds();
    }
    public void UpdateBounds()
    {
        _minX = _BackGround.bounds.min.x+_targetZoom*_cam.aspect;
        _maxX = _BackGround.bounds.max.x-_targetZoom*_cam.aspect;
        _minY = _BackGround.bounds.min.y+_targetZoom;
        _maxY = _BackGround.bounds.max.y-_targetZoom;
    }


    
    public void Move(Vector3 posiition)
    {
        Vector3 delta = posiition;
        delta.z = 0;
       
        _targetPos = transform.position + delta;
    }
    private void LateUpdate()
    {
        _targetPos.x = Mathf.Clamp(_targetPos.x,_minX,_maxX);
        _targetPos.y = Mathf.Clamp(_targetPos.y,_minY,_maxY);
        transform.position = Vector3.Lerp(transform.position, _targetPos, _speed * Time.deltaTime);
        _cam.orthographicSize = Mathf.Lerp(_cam.orthographicSize,_targetZoom,_speed*Time.deltaTime);
    }

    private void Zoom(float amount)
    {
        _targetZoom = Mathf.Clamp(_cam.orthographicSize + amount / 10, minMaxZoom.x, minMaxZoom.y);
        UpdateBounds();
    }
}
