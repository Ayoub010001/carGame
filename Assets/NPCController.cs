using UnityEngine;

public class NPCController : MonoBehaviour
{
    public Transform target; 
    public float moveSpeed = 40f; 

    private void Update()
    {
        // D�placer le NPC vers le point de destination (le long de l'axe Z uniquement)
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

        // V�rifier si le NPC a atteint le point de destination
        if (transform.position.z >= target.position.z-5)
        {
            gameObject.SetActive(false);
            Debug.Log("Game Over");
            Time.timeScale = 0f;
        }
    }
}
