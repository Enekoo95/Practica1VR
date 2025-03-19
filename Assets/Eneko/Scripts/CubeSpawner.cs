using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public GameObject cubePrefab;
    public float spawnRate = 1f;

    void Start()
    {
        InvokeRepeating("SpawnCube", 1f, spawnRate);
    }

    void SpawnCube()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-1.5f, 1.5f), 1f, 10f);
        Instantiate(cubePrefab, spawnPosition, Quaternion.identity);
    }
}
