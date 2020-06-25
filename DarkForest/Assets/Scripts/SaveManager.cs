using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public ShopManager SM;
    public GameManagerCanvas GM;
    public static SaveManager Instance;

    private string filePath;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        GM = FindObjectOfType<GameManagerCanvas>();
        filePath = Application.persistentDataPath + "data5.gamesave";

        LoadGame();
        SaveGame();
    }
    
    public void SaveGame()
    {

        using (var fs = new FileStream(filePath, FileMode.Create))
        {
            var bf = new BinaryFormatter();
            var save = new Save();
            save.Coins = GM.Coins;
            save.ActiveSkinIndex = (int)SM.ActiveSkin;
            save.SaveBoughtItems(SM.Items);

            bf.Serialize(fs, save);
        }
    }

    public void LoadGame()
    {
        if (!File.Exists(filePath))
        {
            return;
        }

        using(var fs = new FileStream(filePath, FileMode.Open))
        {
            var bf = new BinaryFormatter();

            var save = (Save)bf.Deserialize(fs);

            GM.Coins = save.Coins;
            SM.ActiveSkin = (ShopItem.ItemType)save.ActiveSkinIndex;

            for(int i = 0; i < save.BoughtItems.Count; ++i)
            {
                SM.Items[i].IsBought = save.BoughtItems[i];
            }
        }

        GM.TextRefresh();
        GM.ActivateSkin((int)SM.ActiveSkin);
    }

    [System.Serializable]
    public class Save
    {
        public int Coins;
        public int ActiveSkinIndex;
        public List<bool> BoughtItems = new List<bool>();

        public void SaveBoughtItems(List<ShopItem> items)
        {
            foreach(var item in items)
            {
                BoughtItems.Add(item.IsBought);
            }
        }
    }
}
