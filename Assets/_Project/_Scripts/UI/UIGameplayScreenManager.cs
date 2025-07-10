using UnityEngine;

public class UIGameplayScreenManager : MonoBehaviour, IMessageHandle
{
    [SerializeField] private LevelCompleteScreenHUD _levelCompleteScreen;
    [SerializeField] private GamePlayScreenHUD _gameplayScreen;
    void OnEnable()
    {
        MessageManager.AddSubscriber(GameMessageType.OnLevelCompleted, this);
    }

    void OnDisable()
    {
        MessageManager.RemoveSubscriber(GameMessageType.OnLevelCompleted, this);
    }
    public void Handle(Message message)
    {
        switch (message.type)
        {
            case GameMessageType.OnLevelCompleted:
                _gameplayScreen.gameObject.SetActive(false);
                _levelCompleteScreen.gameObject.SetActive(true);
                break;
        }
    }
}
