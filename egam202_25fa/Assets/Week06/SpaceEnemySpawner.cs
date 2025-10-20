using System.Collections.Generic;
using UnityEngine;

public class SpaceEnemySpawner : MonoBehaviour
{
    // Places to spawn enemies
    public List<Transform> spawnHandles;

    // Types of enemies to spawn
    public SpaceEnemyController enemyPrefab;
    public SpaceEnemyController enemyPrefabSmall;
    public SpaceEnemyController enemyPrefabBig;

    // Represents the data for each wave
    [System.Serializable]
    public class WaveData
    {
        public int spawnCount;
        public bool isSmall;
        public bool isBig;
    }

    public List<WaveData> waveDatas;
    public int waveIndex = 0;

    // List of enemies spawned (Used to determine when there's no enemies left)
    List<SpaceEnemyController> enemiesSpawned = new();

    void Update()
    {
        // Only check when on a valid wave
        if (waveIndex < waveDatas.Count)
        {
            // Look through all of the enemies to see if any exist
            bool isAnyEnemyAlive = false;
            foreach (SpaceEnemyController enemy in enemiesSpawned)
            {
                if (enemy != null)
                {
                    // This enemy exists - we can stop looking
                    isAnyEnemyAlive = true;
                    break;
                }
            }

            // No enemies left? Move to the next wave
            if (isAnyEnemyAlive == false)
            {
                Spawn();
                waveIndex++;
            }
        }
    }

    void Spawn()
    {
        // Start by creating a new list of handles
        List<Transform> temporarySpawnHandles = new();

        // Add all of the possible handles
        temporarySpawnHandles.AddRange(spawnHandles);

        // Find teh current wave data in the list
        if (waveIndex < waveDatas.Count)
        {
            WaveData thisWave = waveDatas[waveIndex];

            // Create this number of enemies
            for (int i = 0; i < thisWave.spawnCount; i++)
            {
                // Which type of enemy to create?
                SpaceEnemyController prefab = enemyPrefab;
                if (thisWave.isSmall)
                {
                    if (Random.value < 0.33f)
                    {
                        prefab = enemyPrefabSmall;
                    }
                }
                if (thisWave.isBig)
                {
                    if (Random.value < 0.33f)
                    {
                        prefab = enemyPrefabBig;
                    }
                }

                // Pick a random index
                int spawnIndex = Random.Range(0, temporarySpawnHandles.Count - 1);

                // Create the enemym place at this point
                SpaceEnemyController enemy = Instantiate(prefab);
                enemy.transform.position = temporarySpawnHandles[spawnIndex].position;
                enemiesSpawned.Add(enemy);

                // Remove this transform from the list of options
                temporarySpawnHandles.RemoveAt(spawnIndex);
            }
        }
    }
}
