using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<ShopItem> Items;
    public ShopItem.ItemType ActiveSkin;

    public GameObject DeathScreen;

    private int currentItem = 0;
    private int prevItem = 0;

    public void OpenShop()
    {
        CheckItemButtons();
        gameObject.SetActive(true);
        DeathScreen.SetActive(false);
    }

    public void CloseShop()
    {
        gameObject.SetActive(false);
        DeathScreen.SetActive(true);
    }

    public void CheckItemButtons()
    {
        foreach(ShopItem item in Items)
        {
            item.SM = this;
            item.Init();
            item.CheckButtons();
            item.gameObject.SetActive(false);
        }
        
        Items[currentItem].gameObject.SetActive(true);
    }

    public void NextItem()
    {
        if(Items.Count <= 1)
        {
            return;
        }

        prevItem = currentItem;
        ++currentItem;
        if(currentItem > Items.Count - 1)
        {
            currentItem = 0;
        }
        Items[prevItem].gameObject.SetActive(false);
        Items[currentItem].gameObject.SetActive(true);
    }

    public void PreviousItem()
    {
        if (Items.Count <= 1)
        {
            return;
        }

        prevItem = currentItem;
        --currentItem;
        if (currentItem < 0)
        {
            currentItem = Items.Count - 1;
        }
        Items[prevItem].gameObject.SetActive(false);
        Items[currentItem].gameObject.SetActive(true);
    }
}
