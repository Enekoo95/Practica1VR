using System.Collections;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float spawnDistance = 5f;
    public float spawnIntervalMin = 2f;
    public float spawnIntervalMax = 3f;
    public float destinationOffsetRange = 1f;
    public int maxPoints = 20;

    private int score = 0;

    void Start()
    {
        StartCoroutine(SpawnProjectiles());
    }

    IEnumerator SpawnProjectiles()
    {
        while (score < maxPoints)
        {
            SpawnProjectile();
            float waitTime = Random.Range(spawnIntervalMin, spawnIntervalMax);
            yield return new WaitForSeconds(waitTime);
        }
        Debug.Log("Game Over: Score = " + score);
    }

    void SpawnProjectile()
    {
        Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
        float offset = Random.Range(-destinationOffsetRange, destinationOffsetRange);
        Vector3 direction = (new Vector3(Camera.main.transform.position.x + offset, Camera.main.transform.position.y, Camera.main.transform.position.z) - spawnPos).normalized;
        GameObject projectile = Instantiate(projectilePrefab, spawnPos, Quaternion.LookRotation(direction));
        projectile.GetComponent<Rigidbody>().velocity = direction * 5f; // Adjust speed as needed
    }

    public void AddPoint()
    {
        score++;
        Debug.Log("Score: " + score);
    }
}