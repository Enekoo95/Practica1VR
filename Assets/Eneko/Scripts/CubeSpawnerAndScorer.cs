using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class CubeSpawnerAndScore : MonoBehaviour
{
    public float spawnInterval = 2f;
    public float spawnDistance = 10f;
    public float cubeSpeed = 5f;
    public int score = 0;
    public TMP_Text scoreText;
    public float destinationOffsetRange = 1f;
    public float bombProbability = 0.2f; // solo se usará en modo difícil
    public int penaltyPoints = 5;

    private float fixedHeadHeight;
    private bool modoDificil;

    void Start()
    {
        // Detectar escena actual
        string escenaActual = SceneManager.GetActiveScene().name;
        modoDificil = escenaActual == "ComplicatedLevel";

        fixedHeadHeight = Camera.main.transform.position.y;

        int cubeLayer = LayerMask.NameToLayer("Cube");
        int stickLayer = LayerMask.NameToLayer("Stick");

        if (cubeLayer < 0 || stickLayer < 0)
        {
            Debug.LogError("La capa 'Cube' o 'Stick' no está definida.");
            return;
        }

        for (int i = 0; i < 32; i++)
        {
            if (i != stickLayer)
            {
                Physics.IgnoreLayerCollision(cubeLayer, i, true);
            }
        }

        StartCoroutine(SpawnCubes());
    }

    IEnumerator SpawnCubes()
    {
        while (true)
        {
            SpawnCube();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnCube()
    {
        Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward * spawnDistance;
        spawnPos.y = fixedHeadHeight;

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = spawnPos;
        cube.transform.localScale *= 0.1f;

        bool isBomb = modoDificil && Random.value < bombProbability;

        cube.GetComponent<Renderer>().material.color = isBomb ? Color.red : Color.blue;

        if (isBomb)
            Debug.Log("Cubo bomba generado");
        else
            Debug.Log("Cubo normal generado");

        cube.layer = LayerMask.NameToLayer("Cube");

        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.useGravity = false;

        float offset = Random.Range(-destinationOffsetRange, destinationOffsetRange);
        Vector3 direction = (new Vector3(Camera.main.transform.position.x + offset, Camera.main.transform.position.y, Camera.main.transform.position.z) - spawnPos).normalized;
        rb.velocity = direction * cubeSpeed;

        CubeCollision collisionScript = cube.AddComponent<CubeCollision>();
        collisionScript.gameManager = this;
        collisionScript.isBomb = isBomb;
    }

    public void AddScore(int amount)
    {
        score += amount;
        score = Mathf.Max(0, score);
        scoreText.text = score >= 20 ? "FIN" : score.ToString();
    }
}

public class CubeCollision : MonoBehaviour
{
    public CubeSpawnerAndScore gameManager;
    public bool isBomb;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stick"))
        {
            int points = isBomb ? -gameManager.penaltyPoints : 1;
            gameManager.AddScore(points);
            Destroy(gameObject);
        }
    }
}
