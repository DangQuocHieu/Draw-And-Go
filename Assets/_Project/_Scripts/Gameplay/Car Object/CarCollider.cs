using UnityEngine;

public class CarCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IPowerup>() != null)
        {
            collision.GetComponent<IPowerup>().ApplyEffect(gameObject);

        }
        else if (collision.gameObject.CompareTag(GameConstant.END_POINT_TAG))
        {
            MessageManager.SendMessage(new Message(GameMessageType.OnLevelCompleted));
            Debug.Log("LEVEL COMPLETED");
        }
    }
}
