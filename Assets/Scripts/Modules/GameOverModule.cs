using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverModule : MonoBehaviour
{
    public static event Action OnGameOver = delegate { };

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            OnGameOver();
        }
    }
}
