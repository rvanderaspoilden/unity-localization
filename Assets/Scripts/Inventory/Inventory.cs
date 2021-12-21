using UnityEngine;

public class Inventory : MonoBehaviour {
    [SerializeField] private Canvas canvas;
    [SerializeField] private DraggableItem draggableItemPrefab;
    [SerializeField] private DraggableItem draggableItemPrefab2;
    [SerializeField] private ItemSlot[] _slots;

    private void Awake() {
        this._slots = GetComponentsInChildren<ItemSlot>();
    }

    // Start is called before the first frame update
    void Start() {
        this._slots[0].SetItem(Instantiate(this.draggableItemPrefab, this.transform));
        this._slots[1].SetItem(Instantiate(this.draggableItemPrefab2, this.transform));
    }
}