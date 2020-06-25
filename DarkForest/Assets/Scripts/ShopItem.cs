using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public enum ItemType
    {
        FIRST_SKIN,
        SECOND_SKIN,
        THERD_SKIN,
        FOURTH_SKIN
    }

    public ItemType Type;
    public Button Buy;
    public Text BuyText;
    public Button Activate;
    public bool IsBought;
    public int Cost;

    bool IsActive
    {
        get
        {
            return Type == SM.ActiveSkin;
        }
    }

    public ShopManager SM;

    private GameManagerCanvas gm;

    private void Awake()
    {
        BuyText.text = Cost.ToString();
    }

    public void Init()
    {
        gm = FindObjectOfType<GameManagerCanvas>();
    }

    public void CheckButtons()
    {
        Buy.gameObject.SetActive(!IsBought);
        Buy.interactable = CanBuy();

        Activate.gameObject.SetActive(IsBought);
        Activate.interactable =!IsActive;
    }

    bool CanBuy()
    {
        return gm.Coins >= Cost;
    }

    public void BuyItem()
    {
        if (!CanBuy())
        {
            return;
        }

        IsBought = true;
        gm.Coins -= Cost;

        CheckButtons();

        SaveManager.Instance.SaveGame();
        gm.TextRefresh();
    }

    public void ActivateItem()
    {
        SM.ActiveSkin = Type;
        SM.CheckItemButtons();

        switch (Type)
        {
            case ItemType.FIRST_SKIN:
                gm.ActivateSkin(0, true);
                break;
            case ItemType.SECOND_SKIN:
                gm.ActivateSkin(1, true);
                break;
            case ItemType.THERD_SKIN:
                gm.ActivateSkin(2, true);
                break;
            case ItemType.FOURTH_SKIN:
                gm.ActivateSkin(3, true);
                break;
        }

        SaveManager.Instance.SaveGame();
    }
}
