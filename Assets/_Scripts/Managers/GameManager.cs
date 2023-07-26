using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameManager : Singleton<GameManager>
{
    public int LevelIndex
    {
        get
        {

            return PlayerPrefs.GetInt("LevelIndex");
        }
        set
        {
            PlayerPrefs.SetInt("LevelIndex", value);

        }
    }
    public Dictionary<ItemType, GoalTracker> goalTrackers = new Dictionary<ItemType, GoalTracker>();
    public Level currentLevel;
    public ItemData itemData;
    [SerializeField] private LevelData levelData;
    public UiManager ui;

    private int _itemsFound;
    private int _itemsToFind;
    public event System.Action<SpriteRenderer> OnLevelLoaded;
    private void Start()
    {
        Application.targetFrameRate = 60;
        SetUpLevel();

    }
    public void LevelComplited()
    {
        StartCoroutine(LevelCleared());
    }

    IEnumerator LevelCleared()
    {
        yield return new WaitForSeconds(1);
        ui.OnLevelCleared();
        
    }

    public void SetUpLevel()
    {
        currentLevel = levelData.levels[LevelIndex % levelData.levels.Length];
        if (currentLevel.prefab != null)
        {
            GameObject t = Instantiate(currentLevel.prefab, transform).gameObject;
            OnLevelLoaded?.Invoke(t.transform.GetChild(0).GetComponent<SpriteRenderer>());

        }
        foreach (Goal g in currentLevel.goals)
        {
            _itemsToFind += g.count;
            goalTrackers.Add(g.type, new GoalTracker(g));
            ui.SetUpGoal(goalTrackers[g.type]);
        }
        ui.OnGameStart();
         ui.UpdateMainGoal(_itemsFound,_itemsToFind);
    }

    public void ItemRemoved(ItemScript item, Vector3 position)
    {
        _itemsFound++;
        goalTrackers[item.itemType].RemoveItem(1);
        if (CheckGoals())
        {
            LevelComplited();
        }
        ui.SpawnCorrectFx(goalTrackers[item.itemType],position);
        ui.UpdateMainGoal(_itemsFound,_itemsToFind);
    }

    private bool CheckGoals()
    {
        foreach (KeyValuePair<ItemType, GoalTracker> g in goalTrackers)
        {
            if (g.Value.count > 0)
            {
                return false;
            }
        }

        return true;
    }
}
