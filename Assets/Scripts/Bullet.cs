using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int minDamage = 6;
    public int maxDamage = 16;

    //  viên đạn va chạm với một Collider khác
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu đối tượng va chạm có tag là "Enemy"
        if (collision.CompareTag("Enemy"))
        {
            // Tạo giá trị sát thương ngẫu nhiên trong khoảng từ minDamage đến maxDamage
            int damage = Random.Range(minDamage, maxDamage);

            // Gây sát thương cho đối tượng kẻ địch (giảm máu)
            collision.GetComponent<Health>().TakeDam(damage);

            // Gọi hiệu ứng trúng đạn từ EnemyController 
            collision.GetComponent<EnemyController>().TakeDamEffect(damage);

            // Hủy viên đạn sau khi gây sát thương, để ngăn nó va chạm thêm lần nữa
            Destroy(gameObject);
        }
    }
}
