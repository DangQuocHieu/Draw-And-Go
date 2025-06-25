
using UnityEngine;

public class GravityFlip : MonoBehaviour, IPowerup
{
    private Vector2 initialGravity;

    void Start()
    {
        initialGravity = Physics2D.gravity;
    }


    public void ApplyEffect(GameObject target)
    {
        Physics2D.gravity = new Vector2(Physics2D.gravity.x, Physics2D.gravity.y * -1);

        target.GetComponent<CarMovement>().OnGravityFlip();
    }

    void OnDestroy()
    {
        Physics2D.gravity = initialGravity;
    }
}
