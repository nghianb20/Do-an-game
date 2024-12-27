using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<Transform> weaponSlots = new List<Transform>(); // Danh sách các vị trí (slots) để gắn vũ khí
    int currentWeaponSlot = 0; // Chỉ số vị trí vũ khí hiện tại đang sử dụng

    public void AddWeapon(GameObject weaponPrefab)
    {
        // Kiểm tra nếu vẫn còn vị trí trống trong danh sách weaponSlots
        if (currentWeaponSlot < weaponSlots.Count)
        {
            // Tạo (Instantiate) vũ khí từ prefab tại vị trí hiện tại trong danh sách weaponSlots
            Instantiate(weaponPrefab, weaponSlots[currentWeaponSlot]);
            // Tăng chỉ số vị trí vũ khí lên một
            currentWeaponSlot++;
        }
    }

    // Danh sách quản lý các kẻ địch trong tầm bắn
    public List<Transform> Enemies = new List<Transform>();

    // Thêm một kẻ địch vào danh sách khi nó vào tầm bắn
    public void AddEnemyToFireRange(Transform transform)
    {
        // Lấy thành phần Health của kẻ địch
        Health enemyHealth = transform.GetComponent<Health>();
        // Chỉ thêm kẻ địch vào danh sách nếu nó chưa chết
        if (!enemyHealth.isDead)
            Enemies.Add(transform);
    }

    // Xóa một kẻ địch ra khỏi danh sách khi nó ra khỏi tầm bắn
    public void RemoveEnemyToFireRange(Transform transform)
    {
        // Loại bỏ Transform của kẻ địch khỏi danh sách
        Enemies.Remove(transform);
    }

    // Tìm kẻ địch gần nhất so với vị trí của vũ khí
    public Transform FindNearestEnemy(Vector2 weaponPos)
    {
        if (Enemies != null && Enemies.Count <= 0) return null;

        // gán kẻ địch gần nhất là phần tử đầu tiên 
        Transform nearestEnemy = Enemies[0];

        // Duyệt qua tất cả kẻ địch trong danh sách
        foreach (Transform enemy in Enemies)
        {
            // So sánh khoảng cách giữa kẻ địch và vũ khí
            if (Vector2.Distance(enemy.position, weaponPos) < Vector2.Distance(nearestEnemy.position, weaponPos))
                nearestEnemy = enemy; // Cập nhật kẻ địch gần nhất nếu tìm được kẻ địch gần hơn
        }

        
        return nearestEnemy;
    }
}
