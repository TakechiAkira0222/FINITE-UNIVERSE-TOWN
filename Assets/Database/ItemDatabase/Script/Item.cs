using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "ItemData/CreateItem")]
public class Item : ScriptableObject
{
    public enum TypeOfItem
    {
        Weapon,
        UseItem
    }

    //　アイテムの種類
    [SerializeField]
    private TypeOfItem m_typeOfItem;
    //　アイテムのアイコン
    [SerializeField]
    private Sprite icon;
    //　アイテムの名前
    [SerializeField]
    private string itemName;
    //　アイテムの情報
    [SerializeField]
    private string information;

    /// <summary>
    /// itemの種類
    /// </summary>
    /// <returns></returns>
    public TypeOfItem GetTypeOfItem()
    {
        return m_typeOfItem;
    }

    /// <summary>
    /// itemのアイコン
    /// </summary>
    /// <returns></returns>
    public Sprite GetIcon()
    {
        return icon;
    }

    /// <summary>
    /// itemの名前
    /// </summary>
    /// <returns></returns>
    public string GetItemName()
    {
        return itemName;
    }

    /// <summary>
    /// itemの情報
    /// </summary>
    /// <returns></returns>
    public string GetInformation()
    {
        return information;
    }
}
