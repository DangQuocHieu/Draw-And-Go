using System;
using System.Collections;
using UnityEngine;

public class SpringBoost : MonoBehaviour, IPowerup
{
    [SerializeField] private float _boostForce;
    [SerializeField] private Vector2 _direction = Vector2.up;
    [SerializeField] private Animator _springAnim;
    [SerializeField] private float _releaseTime = 2f;

    private bool _canReleased = true;

    public void ApplyEffect(GameObject target)
    {
        if (!_canReleased) return;
        _canReleased = false;
        Rigidbody2D targetRb = target.GetComponent<Rigidbody2D>();
        targetRb.AddForce(_boostForce * _direction, ForceMode2D.Impulse);
        if (_springAnim != null)
        {
            _springAnim.Play(GameConstant.RELEASED);
            StartCoroutine(ReleaseSpringCoroutine());
        }
    }

    private IEnumerator ReleaseSpringCoroutine()
    {

        yield return new WaitForSeconds(_releaseTime);
        _springAnim.Play(GameConstant.COMPRESSED);
        _canReleased = true;
    }


}
