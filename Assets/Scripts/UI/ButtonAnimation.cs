using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Vector2 _houverSize;
    [SerializeField] private float _duration;
    private RectTransform _rect;
    private Vector2 _defaultSize;

    void Awake()
    {
        _rect = GetComponent<RectTransform>();
        _defaultSize = _rect.sizeDelta;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _rect.DOKill();
        _rect.DOSizeDelta(_houverSize, _duration);
        print("Enter");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rect.DOKill();
        _rect.DOSizeDelta(_defaultSize, _duration);
        print("Exit");
    }
}
