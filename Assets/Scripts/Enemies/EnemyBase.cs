using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    public string word = "";
    public TextMeshProUGUI txtWord = null;
    public float speed = 0f;
    public int difficulty = 0;

    private int currentCharIndex = 0;
    private int life = 0;

    public static event Action<Transform> OnEnemyDeath = delegate { };
    public bool isDead = false;

    public void SetEnemy(string setWord)
    {
        word = setWord;
        txtWord.text = setWord;
    }
    
    public void Hurt()
    {
        life++;
        char[] letters = word.ToCharArray();;
        if (life > letters.Length - 1)
        {
            isDead = true;
            OnEnemyDeath(transform);
            Destroy(gameObject);
        }
    }

    public void UpdateUI(char letter)
    {
        if (letter == currentChar())
        {
            currentCharIndex++;
            char[] letters = word.ToCharArray();
            string colorRed = "<color=\"red\">";
            string colorWhite = "<color=\"white\">";
            txtWord.text = "";
            for (int i = 0; i < letters.Length; i++)
            {
                if (i < currentCharIndex)
                {
                    txtWord.text += colorRed + letters[i];
                }
                else
                {
                    txtWord.text += colorWhite + letters[i];
                }
            }
            if (currentCharIndex > letters.Length - 1)
            {
                isDead = true;
                //OnEnemyDeath(transform);
            }
        }
    }

    public bool isComplete()
    {
        char[] letters = word.ToCharArray();
        return currentCharIndex > letters.Length - 1;
    }

    public char currentChar()
    {
        char[] letters = word.ToCharArray();
        return letters[currentCharIndex];
    }

    public void Move(Vector3 direction)
    {
        float deltaSpeed = speed * Time.deltaTime;
        transform.position += direction * deltaSpeed;
    }
}
