using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardScreenHUD : MonoBehaviour
{
    [SerializeField] private Image _rewardBoxImage;
    [SerializeField] private RectTransform _radialCircle;
    [SerializeField] private Button _collectButton;
    [SerializeField] private TextMeshProUGUI _itemText;
    [SerializeField] private Image _itemImage;

    void OnEnable()
    {
        _collectButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });

        _collectButton.interactable = false;
        _rewardBoxImage.transform.localScale = Vector3.one;
        _radialCircle.transform.localScale = Vector3.zero;
        _collectButton.transform.localScale = Vector3.zero;
        _itemText.transform.localScale = Vector3.zero;
        _itemImage.transform.localScale = Vector3.zero;

        StartCoroutine(CollectCoroutine());
    }

    private IEnumerator CollectCoroutine()
    {
        yield return _rewardBoxImage.transform
            .DOScale(1.2f, 0.3f)
            .SetLoops(6, LoopType.Yoyo)
            .WaitForCompletion();

        yield return _rewardBoxImage.transform
            .DOScale(0f, 0.3f)
            .WaitForCompletion();

        _radialCircle.transform.DOScale(1f, 0.5f);
        _itemText.transform.DOScale(1f, 0.5f);
        _itemImage.transform.DOScale(1f, 0.5f);
        yield return _collectButton.transform.DOScale(1f, 0.5f).WaitForCompletion();
        _collectButton.interactable = true;
    }

    void OnDisable()
    {
        _collectButton.onClick.RemoveAllListeners();
    }

    public void OnItemUnlocked(CarPartSO data)
    {
        _itemText.text = data.PartID;
        _itemImage.sprite = data.PartSprite;
    }
}
