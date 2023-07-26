using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
   public Level[] levels;
}
[System.Serializable]
public struct Level
{
    public string name;
    public Goal[] goals;

    public GameObject prefab;

}
[System.Serializable]
public struct Goal
{
    public ItemType type;
    public int count;
}

[System.Serializable]
public class GoalTracker
{
    public string name;
    public ItemType type;
    public int count;
    public GoalDisplay display;
    public event System.Action<int> OnItemRemoved;
    public  GoalTracker(Goal g)
    {
        name = g.type.ToString();
        type = g.type;
        count = g.count;
    }
    public void RemoveItem(int c)
    {
        count -= c;
        if (this.count < 0)
        {
            this.count = 0;
        }
        OnItemRemoved?.Invoke(this.count);
    }
}
