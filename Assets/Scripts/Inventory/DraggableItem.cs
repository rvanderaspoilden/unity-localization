using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    [SerializeField] private Canvas canvas;

    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;
    private ItemSlot _itemSlot;

    private void Awake() {
        this._rectTransform = GetComponent<RectTransform>();
        this._canvasGroup = GetComponent<CanvasGroup>();
        this.canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        Debug.Log("Start drag");
        this._canvasGroup.alpha = .6f;
        this._canvasGroup.blocksRaycasts = false;
        this.transform.parent = this.canvas.transform;
        this.transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData) {
        this._rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log("Click");
    }

    public void OnEndDrag(PointerEventData eventData) {
        this._canvasGroup.alpha = 1;
        this._canvasGroup.blocksRaycasts = true;

        // Manage drop out of slot
        if (IsOutOfSlot(eventData)) {
            this.transform.parent = this._itemSlot.transform;
            this.SetAnchoredPosition(Vector2.zero);
        }
    }

    public ItemSlot ItemSlot {
        get => _itemSlot;
        set => _itemSlot = value;
    }

    public void SetAnchoredPosition(Vector2 pos) {
        this._rectTransform.anchoredPosition = pos;
    }

    private bool IsOutOfSlot(PointerEventData eventData) {
        return this._itemSlot && (!eventData.pointerCurrentRaycast.isValid || eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>() == null);
    }
}