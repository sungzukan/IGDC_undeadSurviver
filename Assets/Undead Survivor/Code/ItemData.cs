using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable object/ItemData")]
// 커스텀 메뉴를 만드는 속성 .
public class ItemData : ScriptableObject
{
    public enum ItemType { Melee, Range, Glove, Shoe, Heal }

    [Header("# Main Info")]
    public ItemType itemType;
    public int itemId;
    public string itemName;
    public string itemDesc;
    public Sprite itemIcon;


    [Header("# Level Data")]
    public float baseDamage; // 0 level
    public int baseCount; // 0 level
    public float[] damages; // per level
    public int[] counts; // per level


    [Header("# Weapon")]
    public GameObject projectile;
    public Sprite hand;
}
