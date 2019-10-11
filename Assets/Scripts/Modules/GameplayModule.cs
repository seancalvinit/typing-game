using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

public class GameplayModule : MonoBehaviour
{

    
    public Text wavesLefttxt;


    private int currentNumberOfWaves = 0;

    public List<Wave> waves = new List<Wave>();

    public List<Transform> lanes = new List<Transform>();

    public GameObject youWinUI;

    public Vector2 direction = Vector2.zero;

    private List<EnemyBase> spawnedEnemies = new List<EnemyBase>();

    private bool isStarting = false;

    private bool lockedOn = false;

    private EnemyBase currentLockedOnEnemy = null;

    public static event Action<bool, Transform> OnType = delegate { };
    public static event Action OnWavesComplete = delegate { };
    public static event Action OnWaveComplete = delegate { };
    public static event Action OnLevelComplete = delegate { };

    public GameObject explosionFX = null;

    private void Start()
    {
        InputModule.Instance.OnType += HandleOnYype;
        GameOverModule.OnGameOver += HandleOnGameOver;
        StartCoroutine(StartGame());

        EnemyBase.OnEnemyDeath += HandleOnDeath;

        Time.timeScale = 1f;
    }

    private void HandleOnDeath(Transform target)
    {
        GameObject explosion = Instantiate(explosionFX, target.transform.position, target.transform.rotation);

        Destroy(explosion, 0.5f);
    }

    private void Update()
    {
        for (int i = 0; i < spawnedEnemies.Count; i++)
        {
            EnemyBase enemy = spawnedEnemies[i];
            if (isStarting)
            {
                if (enemy == null)
                {
                    spawnedEnemies.Remove(enemy);
                }
                else
                {
                    enemy.Move(direction);
                }
            }
        }
    }

    private void HandleOnGameOver()
    {
        isStarting = false;
    }

    private void HandleOnYype(string inputString)
    {
        if (lockedOn && isStarting)
        {
            foreach (char letter in inputString)
            {
                bool isLetterCorrect = letter == currentLockedOnEnemy.currentChar();
                ResultsModule.Instance.RecordAccuracy(isLetterCorrect);
                OnType(isLetterCorrect, currentLockedOnEnemy.transform);
                if (isLetterCorrect)
                {
                    currentLockedOnEnemy.UpdateUI(letter);
                    if(currentLockedOnEnemy.isComplete())
                    {
                        currentLockedOnEnemy = null;
                        lockedOn = false;
                    }
                }
            }
        }
        else
        {
            foreach (char letter in inputString)
            {
                EnemyBase enemy = spawnedEnemies.Where(e => !e.isComplete() && e.currentChar() == letter).FirstOrDefault();
                if (enemy != null)
                {
                    currentLockedOnEnemy = enemy;
                    enemy.UpdateUI(letter);
                    ResultsModule.Instance.RecordAccuracy(true);
                    OnType(true, currentLockedOnEnemy.transform);
                    lockedOn = true;
                    break;
                }
                else
                {
                    ResultsModule.Instance.RecordAccuracy(false);
                    OnType(false, null);
                }
            }
        }
    }

    IEnumerator StartGame()
    {
        isStarting = true;
        ResultsModule.Instance.StartTimer();
        int waveCounter = currentNumberOfWaves;
        while (currentNumberOfWaves < waves.Count)
        {
            StartCoroutine(SpawnWave(waves[waveCounter]));
            wavesLefttxt.text = "Waves Left: " + (waveCounter + 1) + "/" + waves.Count;
            yield return new WaitUntil(() => waveCounter < currentNumberOfWaves);
            waveCounter = currentNumberOfWaves;
        }
        OnWavesComplete();
        yield return new WaitUntil(() => spawnedEnemies.Count == 0);
        OnLevelComplete();
        isStarting = false;
    }

    IEnumerator SpawnWave(Wave wave)
    {
        int currentNumberOfEnemies = 0;
        int maxNumberOfEnemies = UnityEngine.Random.Range(wave.minEnemies, wave.maxEnemies);
        float remainingTime = wave.EndTime;
        while (currentNumberOfEnemies < maxNumberOfEnemies)
        {
            float spawnTime = UnityEngine.Random.Range(wave.minSpawnTime, remainingTime > wave.maxSpawnTime ? wave.maxSpawnTime : remainingTime);
            yield return new WaitForSeconds(spawnTime);

            GameObject enemy = Instantiate(wave.enemies[UnityEngine.Random.Range(0, wave.enemies.Count - 1)].gameObject, lanes[UnityEngine.Random.Range(0, lanes.Count - 1)]);
            EnemyBase enemyScript = enemy.GetComponent<EnemyBase>();
            enemyScript.SetEnemy(WordDictionaryModule.Instance.GetRandomWordByLevel(enemyScript.difficulty));
            spawnedEnemies.Add(enemyScript);

            remainingTime -= spawnTime;
            currentNumberOfEnemies++;
        }
        yield return new WaitForSeconds(remainingTime);
        currentNumberOfWaves++;
        OnWaveComplete();
    }

}
