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
        new Wave(.25f, 1f/2f,       new int[]{ 60/8,  6/2 }),
        new Wave(.5f, 1.5f/2f,    new int[]{ 85/8,  11/2 }),
        new Wave(1.0f, 2.5f/2f,    new int[]{ 187/8, 23/2 }),
        new Wave(1.25f, 5f/2f,      new int[]{ 262/8, 38/2 }),
        new Wave(1.5f, 6.5f/2f,    new int[]{ 325/8, 65/2 }),
        new Wave(1.75f, 8f/2f,    new int[]{ 700/8, 65/2 }),
        new Wave(1.75f, 9.5f/2f,    new int[]{ 700/8, 65/2 }),
        new Wave(1.75f, 11f/2f,    new int[]{ 700/8, 65/2 }),
        new Wave(1.75f, 12.5f/2f,    new int[]{ 700/8, 65/2 }),
        new Wave(1.75f, 14f/2f,    new int[]{ 700/8, 65/2 }),
        new Wave(1.75f, 15.5f/2f,    new int[]{ 700/8, 65/2 }),
        new Wave(1.75f, 17f/2f,    new int[]{ 700/8, 65/2 }),
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
                        Enemy enemy = Instantiate(enemyPrefabs[enemyType], new Vector3(Random.value, 0, Random.value*3), default);
                        enemy.transform.localScale = enemyPrefabs[enemyType].transform.localScale;
                    }
                }
            }
        }
    }
}
