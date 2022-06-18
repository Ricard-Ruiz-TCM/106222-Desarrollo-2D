using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Enum que determina el tipo de intercambio, Comprar o Vender
public enum TradingType {
    TRADING_BUY, TRADING_SELL
}


public class UITradingManager : MonoBehaviour {

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private TextMeshProUGUI _tradingTextHUD;
    [SerializeField]
    private Image[] _tradingArrows; 

    [SerializeField]
    private GameObject _tradingFlowHUD;
    [SerializeField]
    private GameObject TradingFlowUIPrefab;

    [SerializeField]
    private TradingType _currentTradingType;

    void OnEnable(){
        TradeNode.OnStartTrade += ShowTrading;
        TradeNode.OnExitTrade += HideTrading;

        BuyNode.OnSelectBuy += () => { _currentTradingType = TradingType.TRADING_BUY; };
        SellNode.OnSelectSell += () => { _currentTradingType = TradingType.TRADING_SELL; };

        ItemExhanger.OnBuyItem += OnBuyItem;
        ItemExhanger.OnSellItem += OnSellItem;
        ItemExhanger.OnEquipItem += OnEquipItem;
        ItemExhanger.OnUnequipItem += OnUnequipItem;

        GameManager.GameReset += HideTrading;
    }

    void OnDisable(){
        TradeNode.OnStartTrade -= ShowTrading;
        TradeNode.OnExitTrade -= HideTrading;

        BuyNode.OnSelectBuy -= () => { _currentTradingType = TradingType.TRADING_BUY; };
        SellNode.OnSelectSell -= () => { _currentTradingType = TradingType.TRADING_SELL; };

        ItemExhanger.OnBuyItem += OnBuyItem;
        ItemExhanger.OnSellItem += OnSellItem;
        ItemExhanger.OnEquipItem += OnEquipItem;
        ItemExhanger.OnUnequipItem += OnUnequipItem;

        GameManager.GameReset -= HideTrading;
    }

    void Awake(){
        _animator = GetComponent<Animator>();
    }

    void Start(){
        HideTrading();
    }

    public void ShowTrading(){
        _animator.SetBool("trading", true);

        if (_currentTradingType == TradingType.TRADING_BUY) SetFeedbackArrow("BUY", 1.0f);
        else SetFeedbackArrow("SELL", -1.0f);

    }

    public void HideTrading(){
        _animator.SetBool("trading", false);
    }

    private void SetFeedbackArrow(string text, float direction){
        _tradingTextHUD.text = text;
        _tradingArrows[1].transform.localScale = _tradingArrows[0].transform.localScale = new Vector2(1.0f, direction);
    }

    public void OnSellItem(Item it, bool success){
        if (success) MakeNewTradingFlowUIEntry(Color.green, "+ " + it.SellValue + " $");
        if (!success) MakeNewTradingFlowUIEntry(Color.red, "CAN'T SELL");
    }

    public void OnBuyItem(Item it, bool success){
        if (success) MakeNewTradingFlowUIEntry(Color.green, "+ " + it.Name);
        if (!success) MakeNewTradingFlowUIEntry(Color.red, "CAN'T BUY NO MONEY");
    }

    public void OnEquipItem(Item it){
        MakeNewTradingFlowUIEntry(Color.green, "Equiped <- " + it.Name);
    }

    public void OnUnequipItem(Item it){
        MakeNewTradingFlowUIEntry(Color.red, "Unequiped -> " + it.Name);
    }

    public void MakeNewTradingFlowUIEntry(Color color, string text){
        GameObject opt = Instantiate(TradingFlowUIPrefab, Vector3.zero, Quaternion.identity);
        opt.transform.SetParent(_tradingFlowHUD.transform);
        opt.GetComponent<TextMeshProUGUI>().color = color;
        opt.GetComponent<TextMeshProUGUI>().text = text;
        opt.transform.SetSiblingIndex(0);
    }

}
   