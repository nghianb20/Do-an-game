using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth;
    [HideInInspector] public int currentHealth;

    public HealthBar healthBar;
    public GameObject goldPrefab;

    private float safeTime;
    public float safeTimeDuration = 0f;
    public bool isDead = false;

    public bool camShake = false;
    public int coinCount = 2; // Số lượng xu rơi ra
    public float dropForce = 3f; // Lực đẩy của xu khi rơi ra
    public float dropDistance = 0f;
    private void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.UpdateHealth(currentHealth, maxHealth);
    }

    public void TakeDam(int damage)
    {
        if (safeTime <= 0)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                if (this.gameObject.tag == "Enemy")
                {
                    DropGold();
                    FindObjectOfType<WeaponManager>().RemoveEnemyToFireRange(this.transform);
                    FindObjectOfType<Killed>().UpdateKilled();
                    FindObjectOfType<PlayerExp>().UpdateExperience(UnityEngine.Random.Range(1, 4));
                    Destroy(this.gameObject, 0.125f);
                }
                isDead = true;
            }

            // If player then update health bar
            if (healthBar != null)
                healthBar.UpdateHealth(currentHealth, maxHealth);

            safeTime = safeTimeDuration;
        }
    }
    private void DropGold()
    {
        if (goldPrefab != null)
        {
            for (int i = 0; i < coinCount; i++) // Vòng lặp để tạo ra nhiều xu hơn
            {
                // Tạo hiệu ứng vàng bắn ra xung quanh
                GameObject gold = Instantiate(goldPrefab, transform.position, Quaternion.identity);
                Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;

                // Tính toán vị trí đích cách vị trí hiện tại của kẻ thù 3f theo hướng ngẫu nhiên
                Vector2 targetPosition = (Vector2)transform.position + randomDirection * dropDistance;

                // Tính toán lực cần thiết để di chuyển vàng đến vị trí đích
                Vector2 force = randomDirection * dropForce;

                // Thêm lực vào xu
                Rigidbody2D rb = gold.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(force, ForceMode2D.Impulse);
                }
            }
        }
    }
    private void Update()
    {
        if (safeTime > 0)
        {
            safeTime -= Time.deltaTime;
        }
    }
}
