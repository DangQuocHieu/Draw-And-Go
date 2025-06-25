using System;
using System.Collections;
using UnityEngine;

public class SpringBoost : MonoBehaviour, IPowerup
{
    [SerializeField] private float _boostForce;
    [SerializeField] private Vector2 _direction = Vector2.up;
    private Animator _springAnim;
    [SerializeField] private float _releaseTime = 2f;

    [SerializeField] private float _yOffset;
    private bool _canReleased = true;
    void Awake()
    {
        _springAnim = transform.parent.GetComponent<Animator>();

    }

    public void ApplyEffect(GameObject target)
    {
        if (!_canReleased) return;
        _canReleased = false;
        Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();
        targetRb.AddForce(_boostForce * _direction, ForceMode2D.Impulse);
        _springAnim.Play(GameConstant.RELEASED);
        StartCoroutine(ReleaseSpringCoroutine());
    }

    private IEnumerator ReleaseSpringCoroutine()
    {
        yield return new WaitForSeconds(_releaseTime);
        _springAnim.Play(GameConstant.COMPRESSED);
        _canReleased = true;
    }

    
}
