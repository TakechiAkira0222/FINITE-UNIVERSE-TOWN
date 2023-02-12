using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataBase", menuName = "ItemData/CreateItemDataBase")]
public class ItemDataBase : ScriptableObject
{
    [SerializeField]
    private List<Item> m_itemLists = new List<Item>();
   
    /// <summary>
    /// アイテムリストを返す
    /// </summary>
    /// <returns></returns>
    public List<Item> GetItemLists()
    {
        return m_itemLists;
    }
}
