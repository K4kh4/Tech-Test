using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ItemController : MonoBehaviour
{

    [SerializeField] LayerMask interactable;
    private Camera _mainCam;
    [SerializeField] private Transform _wrong;

    private bool _checkWrong;
    private void Start()
    {
        InputManager.Instance.OnTouch += OnClick;
         InputManager.Instance.OnTouchEnd += OnClilckEnd;
        _mainCam = Camera.main;

    }
    public void OnClick(Vector3 position)
    {
        ItemScript item = GetItemAt(position);
        if (item)
        {
            item.OnClick();
            _checkWrong = false;    
            AudioManager.Instance.OnSelect();
            return;
        }
        _checkWrong = true;
        

    }

    public void OnClilckEnd(Vector3 pos)
    {
        if (!GetItemAt(pos)&& _checkWrong)
        {
            WrongClick(pos);
        }
    }

    private void WrongClick(Vector3 pos)
    {
        _wrong.DOKill(false);
        AudioManager.Instance.OnWrong();
        pos.z = 0;
        _wrong.position = pos;
        _wrong.gameObject.SetActive(true);
        Sequence ms = DOTween.Sequence();
        ms.Append(_wrong.DOScale(Vector3.one, .2f));
        ms.Join(_wrong.DOShakePosition(.3f, .1f));
        ms.Append(_wrong.DOScale(Vector3.zero, .2f));
        ms.OnComplete(() =>
        {
            _wrong.gameObject.SetActive(false);
        });
    }


    private ItemScript GetItemAt(Vector3 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 100, interactable);
        ItemScript item;
        if (hit)
        {
            if (hit.collider.TryGetComponent(out item))
            {
                //                item.OnClick();
                return item;
            }
        }
        return null;
    }
}
