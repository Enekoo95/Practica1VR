using UnityEngine;

public class Saber : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cube"))
        {
            GameManager.Instance.AddPoint();
            Destroy(other.gameObject);
            Debug.Log("¡Cubo destruido!");
        }
    }
}
