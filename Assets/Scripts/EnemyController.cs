using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject damPopUp; 

    Health health;             
    ColoredFlash flash;        
    EnemyAI enemyAI;           

    private void Start()
    {
        health = GetComponent<Health>();          
        flash = GetComponent<ColoredFlash>();     
        enemyAI = GetComponent<EnemyAI>();        
    }

    public void TakeDamEffect(int damage)
    {
        // Hiển thị popup sát thương nếu damPopUp được gán
        if (damPopUp != null)
        {
            // Tạo instance của popup tại vị trí enemy với chút dịch chuyển ngẫu nhiên
            GameObject instance = Instantiate(damPopUp, transform.position
                    + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0), Quaternion.identity);

            // Hiển thị số sát thương trong TextMeshPro
            instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();

            // Tìm animator của popup để hiển thị hiệu ứng (critical nếu damage > 10)
            Animator animator = instance.GetComponentInChildren<Animator>();
            if (damage <= 10) animator.Play("normal"); // Hiệu ứng sát thương thường
            else animator.Play("critical");           // Hiệu ứng sát thương chí mạng
        }

        // Tạo hiệu ứng flash nếu component ColoredFlash có tồn tại
        if (flash != null)
        {
            flash.Flash(Color.white); // Làm nhấp nháy màu trắng
        }

        // Đóng băng enemy nếu component EnemyAI có tồn tại
        if (enemyAI != null)
        {
            enemyAI.FreezeEnemy(); // Gọi hàm FreezeEnemy từ EnemyAI để tạm dừng di chuyển
        }
    }
}
