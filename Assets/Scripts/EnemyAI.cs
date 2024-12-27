using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding; //thu vien de tim duong di 
using UnityEngine.TextCore.Text;

public class EnemyAI : MonoBehaviour
{
    public Transform target;

    public float moveSpeed = 2f;
    public float nextWayPointDistance = 2f;
    public float repeatTimeUpdatePath = 0.5f;
    public SpriteRenderer characterSR;
    public Animator animator;
    public int minDamage;
    public int maxDamage;

    Path path; // Đường đi của kẻ địch
    Seeker seeker; // Thành phần tính toán đường đi
    Rigidbody2D rb; // Rigidbody2D của kẻ địch để di chuyển vật lý
    Health PlayerHealth; //  máu của người chơi

    Coroutine moveCoroutine;

  
    public float freezeDurationTime;
    float freezeDuration;

    private void Start()
    {
        seeker = GetComponent<Seeker>();        // Lấy component Seeker để tìm đường
        rb = GetComponent<Rigidbody2D>();       // Lấy component Rigidbody2D
        freezeDuration = 0;                     // Thời gian đóng băng ban đầu = 0
        target = FindObjectOfType<Player>().transform;  // Tìm và gán target là người chơi
        InvokeRepeating("CalculatePath", 0f, repeatTimeUpdatePath); // Lặp lại việc tính toán đường đi
    }

    void CalculatePath()
    {
        if (seeker.IsDone())    // Tính toán đường đi từ vị trí hiện tại đến target
            seeker.StartPath(rb.position, target.position, OnPathCompleted);
    }

    void OnPathCompleted(Path p)
    {
        if (!p.error)   // Nếu tìm được đường đi, bắt đầu di chuyển
        {
            path = p;
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine);
        moveCoroutine = StartCoroutine(MoveToTargetCoroutine());
    }

    public void FreezeEnemy()
    {
        freezeDuration = freezeDurationTime;    // Set thời gian đóng băng
    }

    IEnumerator MoveToTargetCoroutine()
    {
        int currentWP = 0;

        while (currentWP < path.vectorPath.Count)
        {
            while (freezeDuration > 0)        // Kiểm tra trạng thái đóng băng
            {
                freezeDuration -= Time.deltaTime;
                yield return null;
            }
            // Tính toán hướng và lực di chuyển
            Vector2 direction = ((Vector2)path.vectorPath[currentWP] - rb.position).normalized;
            Vector2 force = direction * moveSpeed * Time.deltaTime;
            transform.position += (Vector3)force;

            // Kiểm tra khoảng cách để chuyển điểm tiếp theo
            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWP]);
            if (distance < nextWayPointDistance)
                currentWP++;

            // Xử lý flip sprite theo hướng di chuyển
            if (force.x != 0)
                if (force.x < 0)
                    characterSR.transform.localScale = new Vector3(-1, 1, 0);
                else
                    characterSR.transform.localScale = new Vector3(1, 1, 0);

            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Khi va chạm với người chơi
        if (collision.CompareTag("Player"))
        {
            PlayerHealth = collision.GetComponent<Health>();
            InvokeRepeating("DamagePlayer", 0, 1f);  // Gây sát thương mỗi giây
        }
        // Khi vào tầm bắn
        if (collision.CompareTag("FireRange"))
        {
            FindObjectOfType<WeaponManager>().AddEnemyToFireRange(this.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CancelInvoke("DamagePlayer");
            PlayerHealth = null;
        }
        if (collision.CompareTag("FireRange"))
        {
            FindObjectOfType<WeaponManager>().RemoveEnemyToFireRange(this.transform);
        }
    }

    void DamagePlayer()
    {
        // Gây sát thương ngẫu nhiên trong khoảng min-max
        int damage = Random.Range(minDamage, maxDamage);
        PlayerHealth.TakeDam(damage);
        PlayerHealth.GetComponent<Player>().TakeDamageEffect(damage);
    }
}
