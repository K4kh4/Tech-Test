using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public class GoalDisplay : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _check;
    [SerializeField] private TextMeshProUGUI _counter;
    private int totalCount;
    public void SetUp(GoalTracker goal, Sprite icon)
    {
        _icon.sprite = icon;
        totalCount = goal.count;
        _counter.text = 0 + "/" + totalCount;
    }

    public void UpdateDisplay(int c)
    {
        _counter.text = totalCount-c + "/" + totalCount;
        if (c > 0)
        {
            _icon.transform.DOScale(Vector3.one * 1.2f, .2f).OnComplete(() => _icon.transform.localScale = Vector3.one);
            return;
        }
        CompleteAnim();


    }
    private void CompleteAnim()
    {
        _counter.gameObject.SetActive(false);
        _check.gameObject.SetActive(true);
        _check.transform.DOPunchScale(Vector3.one * 1.1f, .3f);
        transform.DOPunchScale(Vector3.one * 1.1f, .3f).SetEase(Ease.InOutBounce);//.OnComplete(() => transform.SetSiblingIndex(transform.parent.childCount - 1)).SetDelay(.4f);
    }
}


