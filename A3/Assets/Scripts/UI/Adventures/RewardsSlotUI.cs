using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardsSlotUI : MonoBehaviour {

    // Icono del slot
    public Image Image;

    [SerializeField]
    private TextMeshProUGUI AmountText;

    // Método para cargar la información en el panel
    // @param InventorySlot -> Item  con el total a cagrar
    public void LoadData(InventorySlot item){
        Image.sprite = item.GetItem().Icon;
        AmountText.text = item.GetAmount().ToString();
        AmountText.enabled = item.GetAmount() > 1;
    }

}
