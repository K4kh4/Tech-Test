using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;
using System;

public class UiManager : MonoBehaviour
{



    [SerializeField] private TextMeshProUGUI _levelName;
    [SerializeField] private RectTransform _bottomBar;
    [SerializeField] private GameObject _hideButton;
    [SerializeField] private GameObject _showButton;

    [Header("Goals")]
    [SerializeField] private GoalDisplay _goalDisplayPrefab;
    [SerializeField] private Transform _goalHolder;
    [SerializeField] private Image _mainGoalBar;
    [SerializeField] private TextMeshProUGUI _mainGoaltext;


    public void OnGameStart()
    {

        _levelName.text = GameManager.Instance.currentLevel.name;
    }



    public void SetUpGoal(GoalTracker tracker)
    {
        GoalDisplay g = Instantiate(_goalDisplayPrefab, _goalHolder);
        g.SetUp(tracker, GameManager.Instance.itemData.items[(int)tracker.type].icon);
        tracker.OnItemRemoved += g.UpdateDisplay;
    }

    public void HideBottomBar()
    {
        _bottomBar.transform.DOMoveY(-_bottomBar.rect.height, .2f).OnComplete(() =>
        {
            _bottomBar.gameObject.SetActive(false);
            _showButton.gameObject.SetActive(true);
        });

    }
    public void ShowBottomBar()
    {
        _bottomBar.gameObject.SetActive(true);
        _showButton.gameObject.SetActive(false);
        _bottomBar.transform.DOMoveY(0, .2f).SetEase(Ease.InOutBounce);
    }


    public void UpdateMainGoal(int current, int total)
    {
        _mainGoaltext.text = current + "/" + total;
        _mainGoalBar.DOFillAmount ((float)current/total,.1f) ;
    }

    public void SpawnCorrectFx()
    {
        // Vector3 goalPosition = _coinDisplay.transform.position;
        // // goalPosition = Camera.main.ScreenToWorldPoint(goalPosition);
        // //goalPosition = WorldPositionToScreenSpaceCameraPosition(canvas, goalPosition);

        // GameObject temp = Instantiate(coinFx, Vector3.zero, Quaternion.identity);
        // temp.transform.SetParent(this.transform);

        // Vector3 screenPoint = _currentCoinDisplay.transform.position;
        // screenPoint.x += UnityEngine.Random.Range(-150f, 150f);
        // screenPoint.y += UnityEngine.Random.Range(-150f, 150f);
        // temp.transform.position = screenPoint;
        // Sequence ms = DOTween.Sequence();
        // Vector3 scale = temp.transform.localScale;
        // temp.transform.localScale = Vector3.zero;
        // ms.Append(temp.transform.DOScale(scale, .2f));
        // ms.AppendInterval((UnityEngine.Random.Range(.5f, 1f)));
        // ms.Append(temp.transform.DOMove(goalPosition, .5f).SetDelay(UnityEngine.Random.Range(0, .1f)).OnComplete(() =>
        // {
        //     Destroy(temp.gameObject);
        //     PowerUpManager.Instance.Coin++;
        //     AudioManager.Instance.CoinFX();
        // }));
    }
}


