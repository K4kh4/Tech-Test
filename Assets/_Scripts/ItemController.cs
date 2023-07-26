using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    [SerializeField] LayerMask interactable;
    private Camera _mainCam;

    private void Start()
    {
        InputManager.Instance.OnTouch += OnClick;
        _mainCam = Camera.main;

    }
    public void OnClick(Vector3 position)
    {
        RaycastHit2D hit = Physics2D.Raycast(position,Vector2.zero, 100, interactable);
        ItemScript item;
        if (hit)
        {
            if (hit.collider.TryGetComponent(out item))
            {
                item.OnClick();
            }
        }
    }

    
}
