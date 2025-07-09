using Unity.VisualScripting;
using UnityEngine;

public class CarCustomization : MonoBehaviour, IMessageHandle
{
    [SerializeField] private SpriteRenderer _bodyRenderer;
    [SerializeField] private PolygonCollider2D _bodyCollider;
    [SerializeField] private SpriteRenderer _backWheelRenderer;
    [SerializeField] private SpriteRenderer _frontWheelRenderer;

    void OnEnable()
    {
        MessageManager.AddSubscriber(GameMessageType.OnCarCustomizationLoaded, this);
    }

    void OnDisable()
    {
        MessageManager.RemoveSubscriber(GameMessageType.OnCarCustomizationLoaded, this);
    }
    void Start()
    {
        HandleCustomization();
    }

    private void HandleCustomization()
    {
        if (CarCustomizationManager.Instance != null)
        {
            CarCustomizationData data = CarCustomizationManager.Instance.CustomizationData;
            _backWheelRenderer.sprite = data.WheelSO.PartSprite;
            _frontWheelRenderer.sprite = data.WheelSO.PartSprite;
            _bodyRenderer.sprite = data.BodySO.PartSprite;
            PolygonCollider2D sourceCollider = data.BodySO.CarBodyPrefab.GetComponent<PolygonCollider2D>();
            for (int i = 0; i < sourceCollider.pathCount; i++)
            {
                _bodyCollider.SetPath(i, sourceCollider.GetPath(i));
            }
        }
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnCarCustomizationLoaded:
                HandleCustomization();
                break;
        }
    }
}
