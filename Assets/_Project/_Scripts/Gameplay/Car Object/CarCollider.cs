using UnityEngine;
using UnityEngine.SceneManagement;

public class CarCollider : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IPowerup>() != null)
        {
            Debug.Log("POWERUP COLLECTED");
            collision.GetComponent<IPowerup>().ApplyEffect(gameObject);
        }
        else if (collision.gameObject.CompareTag(GameConstant.END_POINT_TAG))
        {
            if (GameManager.Instance.PreviousSceneName == GameConstant.LEVEL_EDITOR_SCENE)
            {
                SceneManager.LoadSceneAsync(GameConstant.LEVEL_EDITOR_SCENE);
            }
            else
            {
                MessageManager.SendMessage(new Message(GameMessageType.OnLevelCompleted));
            }
        }
    }
}
