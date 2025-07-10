using UnityEngine;

public class UIHomeScreenManager : MonoBehaviour, IMessageHandle
{
    [SerializeField] private RewardScreenHUD _rewardScreen;
    void OnEnable()
    {
        MessageManager.AddSubscriber(GameMessageType.OnItemUnlocked, this);
    }

    void OnDisable()
    {
        MessageManager.RemoveSubscriber(GameMessageType.OnItemUnlocked, this);
    }

    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnItemUnlocked:
                CarPartSO data = (CarPartSO)message.data[0];
                _rewardScreen.OnItemUnlocked(data);
                _rewardScreen.gameObject.SetActive(true);
                break;
        }
    }
}
