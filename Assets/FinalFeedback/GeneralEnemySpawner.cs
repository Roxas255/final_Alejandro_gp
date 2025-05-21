using UnityEngine;

public class GeneralEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Enemy you want to spawn
    public Vector3 location; // Locaiton it will spawn at
    public float spawnRate; // The rate it will spawn at
    public float timer; // The timer keeping track of the spawn rate

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime; // Counting down
        if(timer <= 0) { // Checking timer
            Instantiate(enemyPrefab, location, Quaternion.identity); // Spawnning prefab
            timer = spawnRate; // Resetting timer
        }
    }
}
