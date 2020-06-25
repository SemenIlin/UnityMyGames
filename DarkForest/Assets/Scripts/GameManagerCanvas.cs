using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerCanvas : MonoBehaviour
{
    public bool IsSound = true;
    public float PowerUpMultiplier;
    public float SpeedMove;
    public GameObject HomeMenu;
    public GameObject Result;
    public PlayerMovement playerMovement;
    public RoadSpawner RS;
    public List<Skin> Skins;
    
    public Text Score;
    public Text CointsTxt;
    public Text CoinsOneRunTxt;

    public int Coins = 0;

    private int CoinsOneRun = 0;

    private float pointsBaseValue = 3;
    private float pointsMultiplier = 1; 
    private float points = 0;

    public int CoinsMultiplier { get; private set; } = 1;
    
    public void StartGame()
    {
        playerMovement.Respawn();
        PowerUpMultiplier = 1;
        SpeedMove = 5;
        pointsBaseValue = 3;
        pointsMultiplier = 1;

        playerMovement.animator.SetTrigger("Respawn");
        StartCoroutine(FixTriger());

        HomeMenu.SetActive(false);
        RS.StartGame();
        points = 0;
        CoinsOneRun = 0;
        playerMovement.CanPlay = true;
    }

    IEnumerator FixTriger()
    {
        yield return null;
        playerMovement.animator.ResetTrigger("Respawn");
    }

    public void GoToHomeMenu()
    {
        Result.SetActive(false);
        HomeMenu.SetActive(true);
    }

    public void Show()
    {
        Result.SetActive(true);
        CoinsOneRunTxt.text = CoinsOneRun.ToString();
        SaveManager.Instance.SaveGame();
    }

    private void Update()
    {
        if (playerMovement.CanPlay)
        {
            points += pointsBaseValue * pointsMultiplier * PowerUpMultiplier * Time.deltaTime;

            if(points < 2000)
            {
                CoinsMultiplier = 1;
            }
            else if(points >= 2000 && points < 5000)
            {
                CoinsMultiplier = 2;
            }
            else if(points >= 5000 && points < 10000)
            {
                CoinsMultiplier = 3;
            }
            else
            {
                CoinsMultiplier = 4;
            }

            pointsMultiplier += 0.05f * Time.deltaTime;
            pointsMultiplier = Mathf.Clamp(pointsMultiplier, 1, 10);

           

            SpeedMove += 0.01f * Time.deltaTime;
            SpeedMove = Mathf.Clamp(SpeedMove, 1, 20);
        }

        Score.text = ((int)points).ToString();
        CointsTxt.text = Coins.ToString();
    }

    public void AddCoins(int number)
    {
        Coins += number;
        CoinsOneRun += number;
        TextRefresh();
    }

    public void TextRefresh()
    {
        CointsTxt.text = Coins.ToString();
    }

    public void ActivateSkin(int skinIndex, bool setTriger = false)
    {
        foreach(var skin in Skins)
        {
            skin.HideSkin();
        }

        Skins[skinIndex].ShowSkin();
        playerMovement.animator = Skins[skinIndex].Animator;

        if (setTriger)
        {
            playerMovement.animator.SetTrigger("Death");
        }
    }
}
