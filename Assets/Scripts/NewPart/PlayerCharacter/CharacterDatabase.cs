using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class CharacterData
{
    public string characterName; // Tên nhân vật
    public Sprite characterImage; // Ảnh nhân vật

    // Constructor
    public CharacterData(string name, Sprite image)
    {
        characterName = name;
        characterImage = image;
    }
}

public class CharacterList : MonoBehaviour
{
    // Danh sách nhân vật
    public List<CharacterData> characters = new List<CharacterData>();

    // Hàm khởi tạo danh sách nhân vật
    void Awake()
    {
        // Thêm các nhân vật vào danh sách
        //characters.Add(new CharacterData("Nhân vật 1", characterSprite1));
        //characters.Add(new CharacterData("Nhân vật 2", characterSprite2));
        // Thêm những nhân vật khác tương tự ở đây
    }
}
