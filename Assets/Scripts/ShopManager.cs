using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    
        public GameObject shopUI; // UI của shop
        public Button[] skinButtons; // Các nút mua skin
        public TextMeshProUGUI goldText; // Hiển thị số lượng vàng hiện tại
        private int currentGold; // Số vàng hiện tại của người chơi
        public TextMeshProUGUI[] costText; // Hiển thị giá của các skin
        public int[] skinCosts; // Chi phí cho mỗi skin

    [SerializeField] ShopSkinItemsSO skinData;

    void Start()
    {
        currentGold = GoldManage.instance.GetCurrentGold();
        //currentGold = 999999;
        UpdateGoldUI(); // Cập nhật số vàng khi khởi tạo scene
        UpdateCostTexts(); // Cập nhật giá của các skin
        UpdateButtonInteractivity(); // Cập nhật trạng thái các nút mua

    }

    void UpdateGoldUI()
    {
        goldText.text = "Gold: " + currentGold.ToString();
    }

    private void OnEnable()
    {
        // Đảm bảo cập nhật số vàng khi scene được hiển thị
        UpdateGoldUI();
        UpdateButtonInteractivity(); // Cập nhật trạng thái các nút mua khi scene được hiển thị
    }

    // Cập nhật trạng thái các nút mua dựa trên số vàng hiện tại và các skin đã sở hữu
    void UpdateButtonInteractivity()
    {
        //for (int i = 0; i < skinButtons.Length; i++)
        //{
        //    if (currentGold >= skinCosts[i])
        //    {
        //        skinButtons[i].interactable = true;
        //    }
        //    else
        //    {
        //        skinButtons[i].interactable = false;
        //    }
        //}
        for (int i = 0; i < skinData.character.Count; i++)
        {
            if (skinData.character[i].owned == true)
            {
                costText[i].text = "Owned";
            }

        }
    }

    // Cập nhật giá của các skin trên các nút
    void UpdateCostTexts()
    {
        for (int i = 0; i < costText.Length; i++)
        {
            costText[i].text = skinCosts[i].ToString() + " Gold";
        }
    }

    // Hàm để mua skin
    public void BuySkin(int index)
    {
        if (currentGold >= skinCosts[index] && skinData.character[index].owned == false)
        {
            currentGold -= skinCosts[index];
            GoldManage.instance.SetCurrentGold(currentGold); // Cập nhật số vàng trong hệ thống quản lý vàng
            UpdateGoldUI();
            UpdateButtonInteractivity(); // Cập nhật lại trạng thái các nút mua sau khi mua

            costText[index].text = "Owned";
            skinData.character[index].owned = true;
            skinButtons[index].interactable = false;
            // Logic để thêm skin vào kho đồ của người chơi (tùy thuộc vào hệ thống của bạn)
            Debug.Log("Skin " + index + " purchased.");
        }
        else
        {
            Debug.Log("Not enough gold to purchase skin " + index);
        }
    }

    // Hàm để quay lại menu
    public void ReturnMenu()
    {
        SceneManager.LoadScene(0);
    }

}

