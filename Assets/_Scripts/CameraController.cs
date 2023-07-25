using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public SpriteRenderer temp;
    [SerializeField] Vector2 minMaxZoom = new Vector2(3, 6);

    Camera _cam;
    private float _speed = 5f;

    private float _minX = -10, _maxX= 10,
                    _minY =-10, _maxY = 10;

    private void Start()
    {
        _cam = GetComponent<Camera>();
        targetPos = transform.position;
        InputManager.Instance.OnDrag += Move;
        InputManager.Instance.OnZoom += Zoom;
        UpdateBounds();
    }

    public void UpdateBounds()
    {
        _minX = temp.bounds.min.x+_cam.orthographicSize*_cam.aspect;
        _maxX = temp.bounds.max.x-_cam.orthographicSize*_cam.aspect;
        _minY = temp.bounds.min.y+_cam.orthographicSize;
        _maxY = temp.bounds.max.y-_cam.orthographicSize;
    }


    Vector3 _origin;
    private Vector3 targetPos;
    public void Move(Vector3 posiition)
    {
        Vector3 delta = posiition;
        delta.z = 0;
       
        targetPos = transform.position + delta;
    }
    private void LateUpdate()
    {
        targetPos.x = Mathf.Clamp(targetPos.x,_minX,_maxX);
        targetPos.y = Mathf.Clamp(targetPos.y,_minY,_maxY);
        transform.position = Vector3.Lerp(transform.position, targetPos, _speed * Time.deltaTime);

    }

    private void Zoom(float amount)
    {
        _cam.orthographicSize = Mathf.Clamp(_cam.orthographicSize + amount / 10, minMaxZoom.x, minMaxZoom.y);
        UpdateBounds();
    }
}
