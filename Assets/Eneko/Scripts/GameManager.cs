using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;
    private int maxScore = 20;

    public static GameManager Instance;

    void Start()
    {
        scoreText.text = "Puntos: 0";
        scoreText.ForceMeshUpdate(); // Esto fuerza la actualización visual en TextMeshPro
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddPoint()
    {
        score++;
        scoreText.text = "Puntos: " + score;

        if (score >= maxScore)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        Debug.Log("¡Has ganado!");
        // Aquí puedes cargar una nueva escena o reiniciar el juego
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}