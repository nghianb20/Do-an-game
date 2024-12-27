using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Items", menuName = "item/itemsData")]
public class ShopSkinItemsSO : ScriptableObject
{
    public List<Character> character;
}
