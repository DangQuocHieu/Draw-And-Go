using System;
using System.Collections;
using UnityEngine;

public class SpringBoost : MonoBehaviour, IPowerup
{
    [SerializeField] private float _boostForce;
    [SerializeField] private Vector2 _direction = Vector2.up;
    [SerializeField] private Animator _springAnim;

    public void ApplyEffect(GameObject target)
    {
        Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();
        targetRb.linearVelocityY = 0f;
        targetRb.AddForce(_boostForce * _direction, ForceMode2D.Impulse);
        if (_springAnim != null)
        {
            _springAnim.Play(GameConstant.RELEASED);
        }
    }
}
