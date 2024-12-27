using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour
{
    public int goldAmount = 1; // Số lượng vàng mà đối tượng này cung cấp

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Gọi phương thức để tăng vàng và cập nhật UI
            GoldManage.instance.AddGold(goldAmount);
            // Hủy đối tượng vàng sau khi được nhặt
            Destroy(gameObject);
        }
    }
}
