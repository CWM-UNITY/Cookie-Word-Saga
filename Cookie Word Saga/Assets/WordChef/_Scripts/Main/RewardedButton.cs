using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedButton : MonoBehaviour
{
    public GameObject content;
    public GameObject adAvailableTextHolder;
    public TimerText timerText;

    private const string ACTION_NAME = "rewarded_video";
    private bool isEventAttached;

    private void Start()
    {
        if (timerText != null) timerText.onCountDownComplete += OnCountDownComplete;

#if UNITY_ANDROID || UNITY_IOS
        //Timer.Schedule(this, 0.1f, AddEvents);

        //if (!IsAvailableToShow())
        //{
        //    content.SetActive(false);
        //    if (IsAdAvailable() && !IsActionAvailable())
        //    {
        //        int remainTime = (int)(ConfigController.Config.rewardedVideoPeriod - CUtils.GetActionDeltaTime(ACTION_NAME));
        //        ShowTimerText(remainTime);
        //    }
        //}

        //InvokeRepeating("IUpdate", 1, 1);
#else
        content.SetActive(false);
#endif
    }

    private void OnEnable()
    {
        //UnityAdsManager.OnFinished += OnRewardCompleted;
        ////CharboostService.onCompleteRewardedVideo += OnRewardCompleted;
        //AplovinManager.onRewardCompleted += OnRewardCompleted;
    }

    private void OnDisable()
    {
        //UnityAdsManager.OnFinished -= OnRewardCompleted;
        ////CharboostService.onCompleteRewardedVideo -= OnRewardCompleted;
        //AplovinManager.onRewardCompleted -= OnRewardCompleted;
    }

    private void OnRewardCompleted()
    {
        HandleRewardBasedVideoRewarded(null, null);
    }

    private void AddEvents()
    {
        if (AdmobController.instance.rewardBasedVideo != null)
        {
            AdmobController.instance.rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        }
    }

    private void IUpdate()
    {
        content.SetActive(IsAvailableToShow());
    }

    public void OnClick()
    {
        //if (ServiceManager.Instance.charboostService.HasRewardedVideo() == true)
        //{
        //    CharboostService.onCompleteRewardedVideo = OnRewardCompleted;
        //    ServiceManager.Instance.charboostService.ShowRewardedVideo();
        //}
        //else
        //{
        //    ServiceManager.Instance.charboostService.LoadRewardedVideo();
        //    //if (AdmobController.instance.rewardBasedVideo.IsLoaded() == true)
        //    //{
        //    //    AdmobController.instance.ShowRewardBasedVideo();
        //    //}
        //    if (ServiceManager.Instance.unityAdsManager.IsReady() == true)
        //    {
        //        ServiceManager.Instance.unityAdsManager.ShowAd();
        //    }
        //}
        Sound.instance.PlayButton();
    }

    private void ShowTimerText(int time)
    {
        if (adAvailableTextHolder != null)
        {
            adAvailableTextHolder.SetActive(true);
            timerText.SetTime(time);
            timerText.Run();
        }
    }

    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        //content.SetActive(false);
        //ShowTimerText(ConfigController.Config.rewardedVideoPeriod);
        CurrencyController.CreditBalance(ConfigController.Config.rewardedVideoAmount);
        if (CurrencyController.onBalanceChanged != null)
            CurrencyController.onBalanceChanged.Invoke();
    }

    private void OnCountDownComplete()
    {
        adAvailableTextHolder.SetActive(false);
        if (IsAdAvailable())
        {
            content.SetActive(true);
        }
    }

    public bool IsAvailableToShow()
    {
        if (IsActionAvailable() == true && IsAdAvailable() == true)
            return true;
        return false;
    }

    private bool IsActionAvailable()
    {
        return CUtils.IsActionAvailable(ACTION_NAME, ConfigController.Config.rewardedVideoPeriod);
    }

    private bool IsAdAvailable()
    {
        if (AdmobController.instance.rewardBasedVideo == null) return false;
        bool isLoaded = AdmobController.instance.rewardBasedVideo.IsLoaded();
        return isLoaded;
    }

    private void OnDestroy()
    {
#if UNITY_ANDROID || UNITY_IOS
        if (AdmobController.instance.rewardBasedVideo != null)
        {
            AdmobController.instance.rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;
        }
#endif
    }

    private void OnApplicationPause(bool pause)
    {
        if (!pause)
        {
            if (adAvailableTextHolder.activeSelf)
            {
                int remainTime = (int)(ConfigController.Config.rewardedVideoPeriod - CUtils.GetActionDeltaTime(ACTION_NAME));
                ShowTimerText(remainTime);
            }
        }
    }
}
