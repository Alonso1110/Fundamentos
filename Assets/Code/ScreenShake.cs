using System.Collections; // Necesario para las Coroutines (IEnumerator)
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    // Instancia est�tica para que otras clases puedan acceder f�cilmente a este script
    public static ScreenShake Instance { get; private set; }

    // Posici�n original de la c�mara (sin temblor)
    private Vector3 originalPosition;

    void Awake()
    {
        // Implementaci�n del patr�n Singleton para que solo haya una instancia
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destruye esta instancia si ya existe otra
        }
        else
        {
            Instance = this;
        }

        // Guarda la posici�n original de la c�mara al inicio
        originalPosition = transform.localPosition;
    }

    /// <summary>
    /// Inicia el temblor de la pantalla.
    /// </summary>
    /// <param name="duration">Duraci�n total del temblor en segundos.</param>
    /// <param name="magnitude">Magnitud (intensidad) del temblor.</param>
    public void Shake(float duration, float magnitude)
    {
        // Detiene cualquier temblor actual para evitar conflictos
        StopAllCoroutines();
        // Inicia la corutina del temblor
        StartCoroutine(ShakeCoroutine(duration, magnitude));
    }

    private IEnumerator ShakeCoroutine(float duration, float magnitude)
    {
        float elapsed = 0f; // Tiempo transcurrido

        // Loop mientras el tiempo transcurrido sea menor que la duraci�n
        while (elapsed < duration)
        {
            // Calcula un desplazamiento aleatorio para el temblor.
            // Multiplica por 'magnitude' para controlar la intensidad.
            // 'Random.insideUnitSphere' devuelve un Vector3 aleatorio con una magnitud de 1.
            // Para un juego 2D, a menudo solo querr�s temblar en X e Y, as� que podr�amos ajustar Z a 0.
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;
            // Si tu juego es puramente 2D y no quieres movimiento en Z, usa:
            // Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * magnitude;
            // Para 3D o 2D con cierta perspectiva:
            Vector3 randomOffset = Random.insideUnitSphere * magnitude;

            // Aplica el desplazamiento a la posici�n local de la c�mara.
            // Usamos 'localPosition' para no interferir con el movimiento del padre (si lo hay).
            transform.localPosition = originalPosition + randomOffset;

            // Incrementa el tiempo transcurrido
            elapsed += Time.deltaTime;

            // Espera al siguiente frame
            yield return null;
        }

        // Una vez que el temblor termina, regresa la c�mara a su posici�n original
        transform.localPosition = originalPosition;
    }
}
