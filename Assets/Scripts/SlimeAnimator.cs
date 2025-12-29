using UnityEngine;

public class SlimeAnimator : MonoBehaviour 
{ 
    [Header("Ajustes de Gelatina")] 
    public float pulseSpeed = 4f;          // Velocidad del latido
    public float squashIntensity = 0.15f;  // Qué tanto se deforma

    private Vector3 originalScale;

    void Start()
    {
        // Guardamos la escala inicial para que el rebote sea relativo a ella
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Calculamos la oscilación usando una onda Senoidal
        float bounce = Mathf.Sin(Time.time * pulseSpeed) * squashIntensity;

        // Aplicamos la deformación: cuando se estira hacia arriba (Y), se encoge a los lados (X, Z)
        transform.localScale = new Vector3(
            originalScale.x - (bounce * 0.5f), 
            originalScale.y + bounce, 
            originalScale.z - (bounce * 0.5f)
        );
    }
}