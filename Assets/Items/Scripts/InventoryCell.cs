using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryCell : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    [SerializeField] TextMeshProUGUI _textField;
    [SerializeField] Image _image;

    [SerializeField] private Transform _originParent;
    [SerializeField] private Transform _draggingParent;
    public void Initialize(Transform draggingParent)
    {
        _originParent = transform.parent;
        _draggingParent = draggingParent;
    }
    public void Render(IItem item)
    {
        _textField.text = item.Name;
        _image.sprite = item.UIIcon;
    }


    private int _siblingIndex = -1;
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (_siblingIndex == -1)
            _siblingIndex = transform.GetSiblingIndex();

        transform.SetParent(_draggingParent);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ReturnToGrid();
    }

    private void ReturnToGrid()
    {
        transform.SetParent(_originParent);
        transform.SetSiblingIndex(_siblingIndex);

        _siblingIndex = -1;
    }

    private bool In(RectTransform rect)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rect, transform.position);
    }
}
