using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordManager : MonoBehaviour {

	public List<Word> words;
    public Text countText;
	public WordSpawner wordSpawner;
    public GameObject youWinUI;


    private int wordsTyped;
	private bool hasActiveWord;
	private Word activeWord;

    void Start()
    {
        wordsTyped = 0;
        SetCountText();
    }
    public void AddWord ()
	{
		Word word = new Word(WordGenerator.GetRandomWord(), wordSpawner.SpawnWord());
		Debug.Log(word.word);

		words.Add(word);
	}

	public void TypeLetter (char letter)
	{
		if (hasActiveWord)
		{
			if (activeWord.GetNextLetter() == letter)
			{
				activeWord.TypeLetter();
			}
		} else
		{
			foreach(Word word in words)
			{
				if (word.GetNextLetter() == letter)
				{
					activeWord = word;
					hasActiveWord = true;
					word.TypeLetter();
					break;
				}
			}
		}

		if (hasActiveWord && activeWord.WordTyped())
		{
			hasActiveWord = false;
            wordsTyped = wordsTyped + 1;
            SetCountText();


            words.Remove(activeWord);
		}
	}

    void SetCountText()
    {
        countText.text = "Words Typed: " + wordsTyped.ToString();
        {
            if (wordsTyped >= 10)
            {
                FindObjectOfType<GameManager>().YouWin();
            }
        }
        
    }
}
