using UnityEngine;
using System;

public class ShowRewardedVideo : MonoBehaviour
{
    public GameManagerCanvas GM;
    public GameObject ReclamaBtn;

    private int QuantityShowAd = 0;

    public static String REWARDED_INSTANCE_ID = "0";

    // Use this for initialization
    void Start()
    {
        //Add Rewarded Video Events
        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;

        IronSourceEvents.onRewardedVideoAdLoadedDemandOnlyEvent += this.RewardedVideoAdLoadedDemandOnlyEvent;
        IronSourceEvents.onRewardedVideoAdLoadFailedDemandOnlyEvent += this.RewardedVideoAdLoadFailedDemandOnlyEvent; 
    }

    /************* RewardedVideo API *************/
    public void ShowRewardedVideoButtonClicked()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
    }

    void LoadDemandOnlyRewardedVideo()
    {
        IronSource.Agent.loadISDemandOnlyRewardedVideo(REWARDED_INSTANCE_ID);
    }

    void ShowDemandOnlyRewardedVideo()
    {
        if (IronSource.Agent.isISDemandOnlyRewardedVideoAvailable(REWARDED_INSTANCE_ID))
        {
            IronSource.Agent.showISDemandOnlyRewardedVideo(REWARDED_INSTANCE_ID);
        }
    }

    /************* RewardedVideo Delegates *************/
    void RewardedVideoAvailabilityChangedEvent(bool canShowAd)
    {
        ReclamaBtn.SetActive(canShowAd);
    }

    void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
    {       
        GM.Coins += ssp.getRewardAmount() * 10;
        SaveManager.Instance.SaveGame();
        QuantityShowAd++;
        FacenookManager.Istance.ShowAdQuantity(QuantityShowAd);
    }

    void RewardedVideoAdLoadedDemandOnlyEvent(string instanceId)
    {
        ReclamaBtn.SetActive(true);
    }

    void RewardedVideoAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        ReclamaBtn.SetActive(false);
    }
}
