using UnityEngine;

public class CarCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IPowerup>() != null)
        {
            collision.GetComponent<IPowerup>().ApplyEffect(gameObject);
            
        }   
    }
}
