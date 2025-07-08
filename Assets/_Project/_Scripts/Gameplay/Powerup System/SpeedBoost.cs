using UnityEngine;

public class SpeedBoost : MonoBehaviour, IPowerup
{
    [SerializeField] private float _boostForce;
    [SerializeField] private Vector3 _boostDirection;
    public void ApplyEffect(GameObject target)
    {
        Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();
        targetRb.AddForce(_boostDirection * _boostForce, ForceMode2D.Impulse);
    }
}
