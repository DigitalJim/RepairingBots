using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoreGame : MonoBehaviour
{
    private struct Wave
    {
        public float duration;

        public float botsPerSecond;

        public int[] enemies;

        public Wave(
            float durationInSeconds,
            float botsPerSecond,
            int[] enemies
            )
        {
            this.duration = durationInSeconds;
            this.botsPerSecond = botsPerSecond;
            this.enemies = enemies;
        }
    }

    private class CurrentWave
    {
        public int waveIndex;
        public Wave wave;
        public Queue<int> enemiesRemaining;
        public CurrentWave(int waveIndex, Wave wave)
        {
            this.waveIndex = waveIndex;
            this.wave = wave;
            enemiesRemaining = GenerateWave(wave.enemies);
        }

        private Queue<int> GenerateWave(int[] waveComposition)
        {
            int[] enemies = new int[waveComposition.Sum()];
            int i = 0;
            for (int enemyType = waveComposition.Length-1; enemyType >= 0; enemyType--)
            {
                for (; i < waveComposition[enemyType]; i++)
                {
                    enemies[i] = enemyType;
                }
            }
            
            System.Random r = new System.Random();
            return new Queue<int>(enemies.OrderBy(x => r.Next()));
        }
    }

    public Enemy[] enemyPrefabs;
    private CurrentWave currentWave;
    private float lastSpawnTime;
    
    Wave[] waves =
    {
        //new Wave(.25f, 1,       new int[]{ 60/4,  6 }),
        //new Wave(.5f, 1.5f,    new int[]{ 85 / 4,  11 }),
        //new Wave(1.0f, 2.5f,    new int[]{ 187 / 4, 23 }),
        new Wave(1.25f, 5f,      new int[]{ 262 / 4, 38 }),
        new Wave(1.5f, 6.5f,    new int[]{ 325 / 4, 65 }),
        new Wave(1.75f, 14f,    new int[]{ 1000 / 4, 65 }),
    };

    float Rate(int wave)
    {
        return 0;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int waveIndex;
        if (currentWave == null || !currentWave.enemiesRemaining.Any())
        {
            if (currentWave == null)
            {
                waveIndex = 0;
            }
            else
            {
                waveIndex = currentWave.waveIndex + 1;
            }
            if (waveIndex < waves.Length)
            {
                Debug.Log(waveIndex);
                currentWave = new CurrentWave(waveIndex, waves[waveIndex]);
            }
        }
        else
        {
            waveIndex = currentWave.waveIndex;
        }

        if (currentWave != null)
        {
            if (lastSpawnTime + 1f/currentWave.wave.botsPerSecond < Time.time || !Enemy.enemies.Any())
            {
                if (currentWave.enemiesRemaining.Any())
                {
                    int enemyType = currentWave.enemiesRemaining.Dequeue();
                    lastSpawnTime = Time.time;
                    if (enemyPrefabs[enemyType] != null)
                    {
                        Enemy enemy = Instantiate(enemyPrefabs[enemyType], new Vector3(Random.value, 0, Random.value), default);
                        enemy.transform.localScale = enemyPrefabs[enemyType].transform.localScale;
                    }
                }
            }
        }
    }
}
