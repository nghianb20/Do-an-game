using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Đối tượng đạn (projectile) mà vũ khí sẽ bắn
    public GameObject projectile;

    // Muzzle (đầu súng) nơi đạn sẽ xuất phát
    public GameObject muzzle;

    // Các vị trí spawn đạn, cho phép spawn nhiều đạn từ các vị trí khác nhau
    public Transform[] spawnPos;

    // Thời gian giữa các lần bắn
    private float timeBtwShots;
    // Thời gian giữa các lần bắn ban đầu
    public float startTimeBtwShots;

    // Lực bắn đạn
    public float bulletForce;

    // Độ sát thương tối thiểu và tối đa của đạn
    public int minDamage = 6;
    public int maxDamage = 16;

    // Hiệu ứng lửa khi bắn
    public GameObject fireEffect;

    // Quản lý vũ khí
    public WeaponManager weaponManager;

    // Điểm tính toán để tìm kẻ địch gần nhất
    public Transform calculatePoint;

    private void Start()
    {
        // Lấy đối tượng WeaponManager từ scene
        weaponManager = FindObjectOfType<WeaponManager>();
    }

    private void Update()
    {
        // Giảm thời gian giữa các lần bắn
        timeBtwShots -= Time.deltaTime;

        // Nếu thời gian giữa các lần bắn đã đến 0 hoặc nhỏ hơn
        if (timeBtwShots <= 0)
        {
            // Tìm kẻ địch gần nhất trong pham vi 
            Transform enemy = weaponManager.FindNearestEnemy(calculatePoint.position);

            // Nếu có kẻ địch gần đó, quay súng về phía kẻ địch và bắn
            if (enemy != null)
            {
                RotateGun(enemy.position);
                Fire();
            }
        }
    }

    // Hàm quay súng về phía kẻ địch
    void RotateGun(Vector3 pos)
    {
        // Tính toán hướng quay từ vũ khí đến kẻ địch
        Vector2 lookDir = pos - transform.position;
        // Tính toán góc quay theo hướng X-Y
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // Cập nhật góc quay của vũ khí
        transform.rotation = rotation;

        // Nếu góc quay vượt qua 90 độ và dưới 270 độ, lật đối tượng vũ khí theo chiều dọc
        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
            transform.localScale = new Vector3(1, -1, 0);
        else
            transform.localScale = new Vector3(1, 1, 0);
    }

    // Hàm bắn đạn
    void Fire()
    {
        foreach (Transform spawn in spawnPos)
        {
            // Tạo hiệu ứng muzzle (nơi bắn)
            Instantiate(muzzle, spawn.position, transform.rotation, transform);

            // Tạo đạn từ vị trí spawn và khởi tạo các thuộc tính cho đạn
            var bullet = Instantiate(projectile, spawn.position, Quaternion.identity);
            Bullet bulletC = bullet.GetComponent<Bullet>();
            bulletC.minDamage = minDamage;
            bulletC.maxDamage = maxDamage;

            // Cập nhật lại thời gian giữa các lần bắn
            timeBtwShots = startTimeBtwShots;

            // Thêm lực cho đạn để nó di chuyển theo hướng của súng
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.right * bulletForce, ForceMode2D.Impulse);

            // Tạo hiệu ứng lửa khi bắn
            var fireE = Instantiate(fireEffect, spawn.position, transform.rotation, transform);
        }
    }
}
