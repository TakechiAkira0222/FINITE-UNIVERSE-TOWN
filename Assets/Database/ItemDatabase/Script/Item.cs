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

    //�@�A�C�e���̎��
    [SerializeField]
    private TypeOfItem m_typeOfItem;
    //�@�A�C�e���̃A�C�R��
    [SerializeField]
    private Sprite icon;
    //�@�A�C�e���̖��O
    [SerializeField]
    private string itemName;
    //�@�A�C�e���̏��
    [SerializeField]
    private string information;

    /// <summary>
    /// item�̎��
    /// </summary>
    /// <returns></returns>
    public TypeOfItem GetTypeOfItem()
    {
        return m_typeOfItem;
    }

    /// <summary>
    /// item�̃A�C�R��
    /// </summary>
    /// <returns></returns>
    public Sprite GetIcon()
    {
        return icon;
    }

    /// <summary>
    /// item�̖��O
    /// </summary>
    /// <returns></returns>
    public string GetItemName()
    {
        return itemName;
    }

    /// <summary>
    /// item�̏��
    /// </summary>
    /// <returns></returns>
    public string GetInformation()
    {
        return information;
    }
}
