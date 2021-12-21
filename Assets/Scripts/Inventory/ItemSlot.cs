using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler {
    private DraggableItem _item;

    public delegate void ItemChanged(DraggableItem item);

    public static event ItemChanged OnItemChanged;

    public void OnDrop(PointerEventData eventData) {
        if (eventData.pointerDrag == null) return;

        DraggableItem draggableItem = eventData.pointerDrag.GetComponent<DraggableItem>();

        if (draggableItem) {
            if (draggableItem == _item) {
                // DO NOTHING
                this.SetItem(draggableItem);
            } else if (MustSwapWith(draggableItem)) {
                // SWAP MANAGEMENT
                draggableItem.ItemSlot.SetItem(_item);
                this.SetItem(draggableItem);
            } else if (!this._item) {
                // EMPTY SLOT
                draggableItem.ItemSlot.Clear();
                this.SetItem(draggableItem);
            }
        }
    }

    public void SetItem(DraggableItem item) {
        this._item = item;
        this._item.transform.parent = this.transform;
        this._item.SetAnchoredPosition(Vector2.zero);
        this._item.ItemSlot = this;
        OnItemChanged?.Invoke(this._item);
    }

    public void Clear() {
        this._item = null;
    }

    private bool MustSwapWith(DraggableItem draggableItem) {
        return this._item && this._item != draggableItem && draggableItem.ItemSlot;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        if (eventData.pointerDrag == null) return;

        Debug.Log("HOVER");
    }
}