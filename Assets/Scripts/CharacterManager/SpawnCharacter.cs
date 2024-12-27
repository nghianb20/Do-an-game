using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCharacter : MonoBehaviour
{
    [SerializeField] ShopSkinItemsSO skinData;

    private void Awake()
    {
        GetIndexSkinChoosed();
    }


    void GetIndexSkinChoosed()
    {
        int idx = PlayerPrefs.GetInt("IdxSkinChoosed");
        Debug.Log("INDEX SKIN CHOOSED IS : " + idx);
        SpawnerCharacter(idx);
    }

    void SpawnerCharacter(int id)
    {
        Instantiate(skinData.character[id].characterPrefab, transform);
    }
}
