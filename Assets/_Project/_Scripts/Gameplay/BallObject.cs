using UnityEngine;

public class BallObject : MonoBehaviour, IStartable, IMessageHandle
{
    private Collider2D _ballCollider;
    private Rigidbody2D _ballRb;

    void OnEnable()
    {
        MessageManager.AddSubscriber(GameMessageType.OnCarStarted, this);
    }

    void OnDisable()
    {
        MessageManager.RemoveSubscriber(GameMessageType.OnCarStarted, this);
    }
    void Awake()
    {
        _ballCollider = GetComponent<Collider2D>();
        _ballRb = GetComponent<Rigidbody2D>();
        OnIdle();
    }


    public void OnIdle()
    {
        _ballCollider.enabled = false;
        _ballRb.bodyType = RigidbodyType2D.Static;
    }

    public void OnStart()
    {
        _ballCollider.enabled = true;
        _ballRb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnCarStarted:
                OnStart();
                break;
        }
    }
}
