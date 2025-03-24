using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Mueve el cubo hacia adelante (eje Z negativo en este caso)
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // Destruye el cubo si pasa el límite
        if (transform.position.z < -5f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Saber"))
        {
            Destroy(gameObject);
        }
    }
}