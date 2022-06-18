using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {

    // Icono del slot
    public Image Image;

    [SerializeField]
    private TextMeshProUGUI AmountText;
    [SerializeField]
    private GameObject _itemValue;

    // Objecto
    protected Item _item;

    // Inventario contenedor
    private InventoryUI _inventoryUI;

    // Varaiables de gestion para el Drag del canvas
    protected Canvas _canvas;
    protected Transform _parent;
    protected GraphicRaycaster _graphicRaycaster;

    // Obsever para la ejecución del intercambio de objetos
                        //    <   Origin  ,   Destiny  ,    Item>
    public static event Action<InventoryUI, InventoryUI, Item> OnTradeItem;

    public void LoadData(InventorySlot item){
        Image.sprite = item.GetItem().Icon;
        AmountText.text = item.GetAmount().ToString();
        AmountText.enabled = item.GetAmount() > 1;
    }

    // Método para cargar la información en el icono
    public void LoadData(InventorySlot slot, InventoryUI invenory) {
        Image.sprite = slot.GetItem().Icon;
        AmountText.text = slot.GetAmount().ToString();
        AmountText.enabled = slot.GetAmount() > 1;
        _item = slot.GetItem();
        _inventoryUI = invenory;
        // Deshabilitamos el tooltip del precio
        SetValueTooltip(false, 0);
    }

    // Método para gestionar el "Soltar" del item
    public void OnEndDrag(PointerEventData eventData) {
        //find objects within canvas
        var results = new List<RaycastResult>();
        _graphicRaycaster.Raycast(eventData, results);
        foreach (var hit in results) {
            var inv = hit.gameObject.GetComponent<InventoryUI>();
                                            //    (Origin, Destiny, Item)
            if (inv != null) OnTradeItem?.Invoke(_inventoryUI, inv, _item);
        }
        // Changing parent back to slot.
        transform.SetParent(_parent.transform);
        // And centering item position.
        transform.localPosition = Vector3.zero;
        _itemValue.SetActive(false);
    }

    // Método para gestionar el "Arrastrando" del item
    public void OnDrag(PointerEventData eventData) {
        // Continue moving object around screen.
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0) / transform.lossyScale.x;
    }

    // Método para gsetionar el "Agarrar" del item
    public void OnBeginDrag(PointerEventData eventData) {
        // Habilitamos el tooltip del precio dependiendo si estamos vendiendo o comprando
        SetValueTooltip(true, (_inventoryUI.Inventory.Owner == "Player" ? _item.SellValue : _item.BuyCost));
        // Start moving object from the beginning!
        _parent = transform.parent;
        // Thanks to the canvas scaler we need to devide pointer delta by canvas scale to match pointer movement.
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0) / transform.lossyScale.x; 
        // We need a few references from UI.
        if (!_canvas) {
            _canvas = GetComponentInParent<Canvas>();
            _graphicRaycaster = _canvas.GetComponent<GraphicRaycaster>();
        }
        // Change parent of our item to the canvas.
        transform.SetParent(_canvas.transform, true);
        // And set it as last child to be rendered on top of UI.
        transform.SetAsLastSibling();
    }

    // Método para activar o desactivar el precio del item
    // @param bool active -> true activar | false desactivar
    // @param int price -> valor del objeto
    private void SetValueTooltip(bool active, int price){
        _itemValue.SetActive(active);
        _itemValue.GetComponentInChildren<TextMeshProUGUI>().text = price.ToString();
    }

}
