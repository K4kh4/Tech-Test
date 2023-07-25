using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ItemData", menuName = "Data/ItemData", order = 0)]
public class ItemData : ScriptableObject
{ 
    public ItemSettings[] items;
}
[System.Serializable]
public struct ItemSettings
{
    public string name;
    public ItemType type;
    public Sprite icon;
    
}
public enum ItemType {Alien,Monster,UFO  };