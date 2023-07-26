using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ItemScript : MonoBehaviour
{
    public ItemType itemType;
    
    [Space(10f)]
    [Header("Animation")]
    public float popAnimTime = .4f;

    private event System.Action<ItemScript, Vector3> OnRemove;

    private void Start()
    {
        SetUp();
    }

    public void SetUp()
    {
       // OnRemove += GameManager.Instance.OnItemRemoved;
    }

    public void OnClick()
    {
        OnRemove?.Invoke(this, transform.position);
        transform.DOScale(Vector3.zero, popAnimTime).OnComplete(() => Destroy(gameObject)).SetEase(Ease.InOutBounce);
    }




}
