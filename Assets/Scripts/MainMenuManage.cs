using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuManage : MonoBehaviour
{
    public GameObject settingMenu;
    public GameObject extrasMenu;
    public GameObject selectCharPanel;

    // Danh sách các nút trong menu chính
    public Button[] menuButtons;

    private int currentIndex = 0; // Vị trí nút hiện tại

    void Start()
    {
        // Đặt focus vào nút đầu tiên
        EventSystem.current.SetSelectedGameObject(menuButtons[currentIndex].gameObject);
    }

    void Update()
    {
        // Điều hướng bằng phím mũi tên
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            currentIndex = (currentIndex - 1 + menuButtons.Length) % menuButtons.Length;
            EventSystem.current.SetSelectedGameObject(menuButtons[currentIndex].gameObject);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            currentIndex = (currentIndex + 1) % menuButtons.Length;
            EventSystem.current.SetSelectedGameObject(menuButtons[currentIndex].gameObject);
        }

        // Kích hoạt nút khi bấm Enter
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            menuButtons[currentIndex].onClick.Invoke();
        }

        // Quay lại menu chính khi nhấn ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscape();
        }
    }

    // Xử lý khi nhấn ESC
    void HandleEscape()
    {
        if (selectCharPanel.activeSelf)
        {
            selectCharPanel.SetActive(false);
        }
        else if (settingMenu.activeSelf)
        {
            settingMenu.SetActive(false);
        }
        else if (extrasMenu.activeSelf)
        {
            extrasMenu.SetActive(false);
        }
        else
        {
            Debug.Log("Thoát chương trình nếu đang ở menu chính");
            // Thoát game nếu cần (hoặc giữ nguyên)
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }

    public void playButton()
    {
        selectCharPanel.SetActive(true);
    }

    public void settingButton()
    {
        settingMenu.SetActive(true);
    }

    public void settingButtonoff()
    {
        settingMenu.SetActive(false);
    }

    public void extrasButton()
    {
        extrasMenu.SetActive(true);
    }

    public void extrasButtonoff()
    {
        extrasMenu.SetActive(false);
    }

    public void exitbutton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void Shopbutton()
    {
        SceneManager.LoadScene(2);
    }
}
