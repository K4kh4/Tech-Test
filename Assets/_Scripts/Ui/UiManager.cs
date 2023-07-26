using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameplayPanel;
    [SerializeField] private GameObject LevelClearedPanel;
    [Header("Gameplay")]
    [SerializeField] private TextMeshProUGUI _levelName;
    [SerializeField] private RectTransform _bottomBar;
    [SerializeField] private GameObject _hideButton;
    [SerializeField] private GameObject _showButton;
     [Header("LevelCleared")]
    [SerializeField] private TextMeshProUGUI _levelNameEnd;
    [SerializeField] private RectTransform _box;

    [Header("Goals")]
    [SerializeField] private GoalDisplay _goalDisplayPrefab;
    [SerializeField] private RectTransform _goalHolder;
    [SerializeField] private Image _mainGoalBar;
    [SerializeField] private TextMeshProUGUI _mainGoaltext;
    [Header("fx")]
    [SerializeField] private Image itemfx;
    private Camera _mainCam;
    private GameObject _activePanel;
    public void OnGameStart()
    {
        ChangePanel(menuPanel);
        _mainCam = Camera.main;
        _levelName.text = GameManager.Instance.currentLevel.name;
        _levelNameEnd.text = GameManager.Instance.currentLevel.name;
        Rect temp = _goalHolder.rect;
        temp.width = _goalHolder.childCount * (_goalDisplayPrefab.GetComponent<RectTransform>().rect.width + 50);
        _goalHolder.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _goalHolder.childCount * (_goalDisplayPrefab.GetComponent<RectTransform>().rect.width + 50));
    }


    private void ChangePanel(GameObject panel)
    {
        _activePanel?.SetActive(false);
        _activePanel = panel;
        _activePanel?.SetActive(true);
    }

    public void SetUpGoal(GoalTracker tracker)
    {
        GoalDisplay g = Instantiate(_goalDisplayPrefab, _goalHolder);
        g.SetUp(tracker, GameManager.Instance.itemData.items[(int)tracker.type].icon);
        tracker.OnItemRemoved += g.UpdateDisplay;
        tracker.display = g;
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
        _mainGoalBar.DOFillAmount((float)current / total, .1f);
    }

    public void SpawnCorrectFx(GoalTracker goal, Vector3 pos)
    {
        Vector3 goalPosition = goal.display.transform.position;

        Vector3 screenPoint = _mainCam.WorldToScreenPoint(pos);
        Image temp = Instantiate(itemfx, screenPoint, Quaternion.identity);
        temp.sprite = GameManager.Instance.itemData.items[(int)goal.type].icon;
        temp.transform.SetParent(this.transform);

        Vector3 scale = temp.transform.localScale;

        // Sequence ms = DOTween.Sequence();
        //     ms.Append(temp.transform.DOPunchScale(scale , .2f));
        //     ms.Append(temp.transform.DOJump(goalPosition, 300 ,1, .4f)).OnComplete(() =>
        //     {
        //         Destroy(temp.gameObject);
        //     });
        //     ms.Join(temp.transform.DOScale(scale,.4f));
        // temp.transform.DOScale(scale*.8f,.5f);
        temp.transform.DOJump(goalPosition, 300, 1, .5f).OnComplete(() =>
        {
            Destroy(temp.gameObject);
        });
    }

    public void OnLevelCleared()
    {
        ChangePanel(LevelClearedPanel);
        Vector2 temp = _box.sizeDelta;
        _box.sizeDelta = new Vector2(temp.x,0);
        _box.DOSizeDelta(temp,.5f).SetEase(Ease.OutElastic);

    }

    public void PlayButton()
    {
        ChangePanel(gameplayPanel);
    }
    public void NextWord()
    {
        SceneManager.LoadScene(0);
    }


}


