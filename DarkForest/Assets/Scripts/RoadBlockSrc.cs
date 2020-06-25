using UnityEngine;
using System.Collections.Generic;

public class RoadBlockSrc : MonoBehaviour
{
    public GameObject HexogenObj;

    private PlayerMovement PM;
    private GameManagerCanvas GM;
    private Vector3 moveVec;

    bool powerUpSpawn;

    public List<GameObject> PowerUps;
    
    void Start()
    {
        PowerUpController.CoinsPowerUpEvents += CoinsEvent;

        GM = FindObjectOfType<GameManagerCanvas>();
        PM = FindObjectOfType<PlayerMovement>();
        moveVec = new Vector3(-1, 0, 0);

        powerUpSpawn = Random.Range(0, 101) <= 10;
        if (powerUpSpawn)
        {
            PowerUps[Random.Range(0, PowerUps.Count)].SetActive(true);
        }
    }

    void Update()
    {
        if (PM.CanPlay)
        {
            transform.Translate(moveVec * Time.deltaTime * GM.SpeedMove);
        }
    }

    void CoinsEvent(bool activate)
    {
        if(activate)
        {
            HexogenObj.SetActive(true);
            return;
        }
        else
        {
            HexogenObj.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        PowerUpController.CoinsPowerUpEvents -= CoinsEvent;
    }
}
