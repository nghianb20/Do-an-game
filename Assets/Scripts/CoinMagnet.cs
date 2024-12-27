using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMagnet : MonoBehaviour
{
    public float magnetRadius = 5f; // Bán kính hút xu

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            // Thu thập đồng xu khi nó gặp phải item hút xu
            CollectCoin(other.gameObject);
        }
    }

    private void CollectCoin(GameObject coin)
    {
        // Xoá đồng xu 
       
        Destroy(coin);
    }

    private void Update()
    {
        // Hút đồng xu trong bán kính
        Collider2D[] coins = Physics2D.OverlapCircleAll(transform.position, magnetRadius, LayerMask.GetMask("Coin"));

        foreach (Collider2D coin in coins)
        {
            CollectCoin(coin.gameObject);
        }
    }
}
