using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectCharacter : MonoBehaviour
{

    private const string crrIndexSkinKey = "IdxSkinChoosed";
    public int crrIndexSkin;

    public List<Image> BgSkinBtn;
    public List<Button> skinBtn;

    [SerializeField] ShopSkinItemsSO skinData;
    private void Awake()
    {
        GetIndexChoosedSkin();
    }

    private void Start()
    {
        DisplayBgSkin();
    }

    void DisplayBgSkin()
    {
        for (int i = 0; i < BgSkinBtn.Count; i++)
        {
            if (skinData.character[i].owned == false)
            {
                BgSkinBtn[i].color = Color.black;
                skinBtn[i].interactable = false;
            }
            else
            {
                DisPlayCrrBgSkin(i);
                skinBtn[i].interactable = true;
            }
        }
    }
    void DisPlayCrrBgSkin(int _idx)
    {
        if (_idx == crrIndexSkin)
        {
            BgSkinBtn[_idx].color = Color.yellow;
        }
        else
        {
            BgSkinBtn[_idx].color = Color.white;
        }
    }

    public void ChooseSkin(int idx)
    {
        crrIndexSkin = idx;
        DisplayBgSkin();
    }


    public void SaveIndexChoosedSkin()
    {
        PlayerPrefs.SetInt(crrIndexSkinKey, crrIndexSkin);
        PlayerPrefs.Save();

        SceneManager.LoadScene(1);
    }

    void GetIndexChoosedSkin()
    {
        crrIndexSkin = PlayerPrefs.GetInt(crrIndexSkinKey);
    }

}
