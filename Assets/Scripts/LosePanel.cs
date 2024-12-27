using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LosePanel : MonoBehaviour
{
    public TextMeshProUGUI score; 
    private void Start()
    {
        // Ẩn bảng thua ngay khi bắt đầu trò chơi
        Hide();
    }

    public void Show()
    {
        // Hiển thị bảng thua
        gameObject.SetActive(true);

        // Tính toán điểm số dựa trên số lượng kẻ địch đã tiêu diệt
        int scoreI = FindObjectOfType<Killed>().currentKilled * 10;

        // Cập nhật văn bản hiển thị điểm số
        score.text = "You get: " + scoreI.ToString() + " Score";

        // Dừng thời gian (đóng băng trò chơi)
        Time.timeScale = 0;
    }

    public void Hide()
    {
        // Khôi phục thời gian (trò chơi tiếp tục chạy)
        Time.timeScale = 1;

        // Ẩn bảng thua
        gameObject.SetActive(false);
    }

    private void Update()
    {
        // Kiểm tra nếu người chơi nhấn phím "R"
        if (Input.GetKeyDown(KeyCode.R))
        {
            // Khôi phục thời gian (nếu đang bị dừng)
            Time.timeScale = 1;

            // Tải lại màn chơi hiện tại
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
