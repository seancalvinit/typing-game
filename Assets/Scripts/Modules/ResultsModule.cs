using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class ResultsModule : MonoBehaviour
{

    public Text scoreCounttxt;
    public Text scoreMultipliertxt;
    


    public static ResultsModule Instance
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

    List<bool> accuracyRecord = new List<bool>();

    float startTime = 0f;
    Dictionary<int, int> wordPerMinuteRecord = new Dictionary<int, int>();

    public float currentScore = 0f;

    public float scoreAdd = 0f;

    public float currentMultiplierMeter = 0;

    public float maxMultiplierMeter = 1000;

    public float multiplierAdd = 0;

    private void Start()
    {
        EnemyBase.OnEnemyDeath += HandleOnEnemyDeath;
        GameplayModule.OnType += GameplayModule_OnType;
    }

    private void OnDestroy()
    {
        EnemyBase.OnEnemyDeath -= HandleOnEnemyDeath;
        GameplayModule.OnType -= GameplayModule_OnType;
    }

    public void GameplayModule_OnType(bool isCorrect, Transform target)
    {
        if (isCorrect)
        {
            if(maxMultiplierMeter >= currentMultiplierMeter + multiplierAdd)
            {
                currentMultiplierMeter += multiplierAdd;
            }
            else
            {
                currentMultiplierMeter = maxMultiplierMeter;
            }


            float multiplier = currentMultiplierMeter;
            int multiplyBy = 0;
            for (int i = 0; multiplier > 100; i++)
            {
                multiplyBy++;
                multiplier -= 100;
            }
            currentScore += scoreAdd * multiplyBy;
        }
        else
        {
            currentMultiplierMeter = 100;
        }
        scoreCounttxt.text = "Score: " + Mathf.RoundToInt(currentScore);

        scoreMultipliertxt.text = "Multiplier: " + currentMultiplierMeter.ToString();
    }

    public void StartTimer()
    {
        startTime = Time.time;
    }

    private void HandleOnEnemyDeath(Transform target)
    {
        int TimeInMinutes = Mathf.RoundToInt((Time.time - startTime) / 60);
        if (wordPerMinuteRecord.ContainsKey(TimeInMinutes))
        {
            wordPerMinuteRecord[TimeInMinutes] += 1;
        }
        else
        {
            wordPerMinuteRecord.Add(TimeInMinutes, 1);
        }
    }


    public void RecordAccuracy(bool isCorrect)
    {
        accuracyRecord.Add(isCorrect);
    }

    public double Accuracy()
    {
        List<bool> corrects = accuracyRecord.Where(a => a == true).ToList();
        return (double)corrects.Count / accuracyRecord.Count;
    }

    public double WordPerMinute()
    {
        return wordPerMinuteRecord.Values.Average();
    }

}
