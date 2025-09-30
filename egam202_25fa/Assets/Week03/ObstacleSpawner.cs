using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public Vector2 size;
    public GameObject toSpawn;

    public Vector3 boxOverlapSize;

    public float spawnDuration = 1;
    float spawnTimer;

    void Update()
    {
        // Add to the timer until we need to spawn
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDuration)
        {
            spawnTimer = 0;
            Spawn();
        }
    }

    void Spawn()
    {
        // We want to find a non-taken area to spawn our object
        int maxAttempts = 100;

        bool hasFoundPosition = false;
        while (hasFoundPosition == false)
        {
            // Pick a random offset
            Vector3 newOffset = Vector3.zero;
            newOffset.x = Random.Range(-size.x, size.x);
            newOffset.z = Random.Range(-size.y, size.y);

            // Determine the final position
            Vector3 spawnPosition = transform.position + newOffset;

            // Is this overlapping with anything?
            Collider[] colliders = Physics.OverlapBox(spawnPosition, boxOverlapSize * 0.5f);

            // No colliders = no overlap
            if (colliders.Length == 0)
            {
                hasFoundPosition = true;

                // Create the object
                GameObject newObject = Instantiate(toSpawn);
                newObject.transform.position = spawnPosition;
            }
            // Colliders = overlap
            else
            {
                // Debug print
                Debug.Log("Something was in the position " + spawnPosition);
                Debug.DrawRay(spawnPosition, boxOverlapSize * 0.5f, Color.red, 1f);

                // Use one attempt
                // This prevents the list from endlessly looping
                maxAttempts--;
                if (maxAttempts <= 0)
                {
                    Debug.Log("Tried 100 times to find a position");
                    break;
                }
            }
        }
    }
}
