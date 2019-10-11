using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WordDictionaryModule : MonoBehaviour
{
    public static WordDictionaryModule Instance
    {
        get; private set;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public List<WordLevel> dictionary = new List<WordLevel>();

    public string GetRandomWordByLevel(int level)
    {
        List<string> words = dictionary.Where(w => w.difficulty == level).FirstOrDefault().words;
        return words[Random.Range(0, words.Count - 1)];
    }

}

[System.Serializable]
public class WordLevel
{
    public int difficulty = 0;
    public List<string> words = new List<string>();
}
