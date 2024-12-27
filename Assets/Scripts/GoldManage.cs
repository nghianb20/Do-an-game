using UnityEngine;
using TMPro;

public class GoldManage : MonoBehaviour
{
    public static GoldManage instance; // Singleton instance

    public TextMeshProUGUI goldText; // Tham chiếu đến TextMeshPro UI
    public TextMeshProUGUI goldText2; // Tham chiếu đến TextMeshPro UI
    private int currentGold = 0;

    private void Awake()
    {
        // Thiết lập Singleton instance
        if (instance == null)   // Nếu chưa có instance, thiết lập instance là đối tượng hiện tại(this)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);    // Nếu đã có instance, phá hủy đối tượng hiện tại để chỉ giữ lại một instance duy nhất
        }
        // Load the saved gold amount
        currentGold = PlayerPrefs.GetInt("CurrentGold", 0);
    }

    private void Start()
    {
        UpdateGoldUI();
    }
    private void SaveGold()
    {
        PlayerPrefs.SetInt("CurrentGold", currentGold);
        PlayerPrefs.Save();
    }
    // Phương thức để thêm vàng
    public void AddGold(int amount)
    {
        currentGold += amount;
        UpdateGoldUI();
        SaveGold();
    }
    public void SpendGold(int amount)
    {
        currentGold -= amount;
        UpdateGoldUI();
        SaveGold();
    }
    public void SetCurrentGold(int gold)
    {
        currentGold = gold;
        // Lưu trạng thái số vàng vào lưu trữ (PlayerPrefs...)
    }
    public int GetCurrentGold()
    {
        return currentGold;
    }
    // Cập nhật UI để hiển thị số lượng vàng hiện tại
    private void UpdateGoldUI()
    {
        if (goldText != null)
        {
            goldText.text = "Gold: " + currentGold.ToString();
        }
        if (goldText2 != null)
        {
            goldText2.text = currentGold.ToString();
        }
    }
}
