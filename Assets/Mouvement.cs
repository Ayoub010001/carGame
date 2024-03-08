
using UnityEngine;

public class Mouvement : MonoBehaviour
{
    public float baseMoveSpeed = 15f; // Vitesse de d�placement de base
    public float nitroMoveSpeed = 48f; // Vitesse de d�placement avec le nitro
    private float currentMoveSpeed; // Vitesse de d�placement actuelle
    private float lastCollisionTime; // Temps �coul� depuis la derni�re collision
    private int collectedCoins = 0; // Nombre de pi�ces collect�es
    private bool isNitroActive = false; // Indique si le nitro est activ�

    private void Start()
    {
        currentMoveSpeed = baseMoveSpeed; // Initialiser la vitesse de d�placement actuelle � la vitesse de d�placement de base
        lastCollisionTime = Time.time; // Initialiser le temps de la derni�re collision au temps actuel
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

        // Activer le nitro lorsque la touche espace est enfonc�e
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isNitroActive = true;
        }
        // D�sactiver le nitro lorsque la touche espace est rel�ch�e
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isNitroActive = false;
        }

        // Augmenter progressivement la vitesse apr�s la collision avec une bo�te
        if (Time.time - lastCollisionTime < 4f) // Augmenter la vitesse pendant les 4 premi�res secondes apr�s la collision
        {
            currentMoveSpeed += 8f * Time.deltaTime; // Augmenter la vitesse de 0.8 unit� par seconde
        }

        // Appliquer la vitesse de d�placement normale ou avec le nitro
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
            lastCollisionTime = Time.time; // Mettre � jour le temps de la derni�re collision
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            Destroy(collision.gameObject); // D�truire le coin
            // Incr�menter le nombre de pi�ces collect�es
            collectedCoins++;
            // Afficher le nombre de pi�ces collect�es dans la console
            Debug.Log("Nombre de pi�ces collect�es : " + collectedCoins);
        }
    }

    private void FixedUpdate()
    {
        // Limiter le d�placement de la voiture � l'int�rieur de la route
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, -2f, 2f); // Limiter la position X de la voiture
        transform.position = position;
    }
}
