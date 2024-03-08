
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    public float baseMoveSpeed = 15f; // Vitesse de déplacement de base
    public float nitroMoveSpeed = 48f; // Vitesse de déplacement avec le nitro
    private float currentMoveSpeed; // Vitesse de déplacement actuelle
    private float lastCollisionTime; // Temps écoulé depuis la dernière collision
    private int collectedCoins = 0; // Nombre de pièces collectées
    private bool isNitroActive = false; // Indique si le nitro est activé

    private void Start()
    {
        currentMoveSpeed = baseMoveSpeed; // Initialiser la vitesse de déplacement actuelle à la vitesse de déplacement de base
        lastCollisionTime = Time.time; // Initialiser le temps de la dernière collision au temps actuel
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (verticalInput < 0f)
        {
            verticalInput = 0f;
        }

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * currentMoveSpeed * Time.deltaTime;

        transform.Translate(movement);

        transform.Rotate(Vector3.up, horizontalInput * 10f * Time.deltaTime);

        // Activer le nitro lorsque la touche espace est enfoncée
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isNitroActive = true;
        }
        // Désactiver le nitro lorsque la touche espace est relâchée
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isNitroActive = false;
        }

        // Augmenter progressivement la vitesse après la collision avec une boîte
        if (Time.time - lastCollisionTime < 4f) // Augmenter la vitesse pendant les 4 premières secondes après la collision
        {
            currentMoveSpeed += 8f * Time.deltaTime; // Augmenter la vitesse de 0.8 unité par seconde
        }

        // Appliquer la vitesse de déplacement normale ou avec le nitro
        if (isNitroActive)
        {  
            if(collectedCoins > 6) { currentMoveSpeed = nitroMoveSpeed; }
            
        }
        else
        {
            currentMoveSpeed = baseMoveSpeed;
        }

    }
    

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Box"))
        {
            Destroy(collision.gameObject, 0.3f);
            currentMoveSpeed *= 0.5f; // Diminuer la vitesse de la voiture de 50%
            lastCollisionTime = Time.time; // Mettre à jour le temps de la dernière collision
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject); // Détruire le coin
            // Incrémenter le nombre de pièces collectées
            collectedCoins++;
            // Afficher le nombre de pièces collectées dans la console
            Debug.Log("Nombre de pièces collectées : " + collectedCoins);
        }
    }

    private void FixedUpdate()
    {
        // Limiter le déplacement de la voiture à l'intérieur de la route
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -2f, 2f); // Limiter la position X de la voiture
        transform.position = position;
    }
}
