using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordCollision : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Collider")
        {
            FindObjectOfType<GameManager>().EndGame();
            
        }
    }
}
