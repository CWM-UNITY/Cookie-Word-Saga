//using Facebook.Unity;
//using FacebookManager;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class FacebookMenuController : MonoBehaviour {
//    public LeaderBoardController leaderBoardController;

//    private FacebookService facebookService;

//    public Button buttonLogin = null;

//    public Sprite spritebuttonLogin = null;
//    public Sprite spritebuttonLeaderboard = null;

//    public bool isTesting = false;

//    private void Awake()
//    {
//        facebookService = ServiceManager.Instance.facebookService;
//    }

//    private void OnEnable()
//    {
//        FacebookService.OnLoginCompleted += OnLoginCompleted;
//        FacebookService.OnLogOutCompoleted += OnLogOutCompoleted;

//        FBUser.OnGetNameCompleted += OnGetNameCompleted;

//        buttonLogin.image.sprite = facebookService.IsLoggedIn == true ? spritebuttonLeaderboard : spritebuttonLogin;
//    }

//    private void OnDisable()
//    {
//        FacebookService.OnLoginCompleted -= OnLoginCompleted;
//        FacebookService.OnLogOutCompoleted -= OnLogOutCompoleted;

//        FBUser.OnGetNameCompleted -= OnGetNameCompleted;
//    }

//    public void LogIn()
//    {
//        if (facebookService.IsUserLogIn() == true)
//            this.ShowLeaderBoard();
//        else
//            facebookService.FBLogin();
//    }

//    private void OnLoginCompleted(IResult result)
//    {
//        this.facebookService.fbUser.GetName();
//    }

//    private void OnLogOutCompoleted()
//    {
        
//    }

//    public void Share()
//    {
//        facebookService.fbShare.ShareLink(new System.Uri("https://play.google.com/store/apps/details?id=com.sompom.puzzle.match.farm.garden&hl=en"));
//    }

//    public void Invite()
//    {
//        facebookService.fbAppRequest.FBAppRequest("test title", "test message", "send");
//    }

//    public void PublicPermission()
//    {
//        this.facebookService.fbFriendPermission.PerformRequestPublishPermission((bool isOk) => {
//            Debug.Log("PerformRequestPublishPermission : " + isOk);
//            this.facebookService.fbScore.GetScore();
//        });
//    }

//    private void OnGetNameCompleted(string result)
//    {
//        //_name.text = this.facebookService.fbUser.FBName;
//        //email.text = this.facebookService.fbUser.FBEmail;
//        //this.facebookService.fbDownloadProfile.LoadProfileByURL(this.facebookService.fbUser.FBID, this.facebookService.fbUser.FBUserProfile, this.profile);
//        buttonLogin.image.sprite = facebookService.IsUserLogIn() == true ? spritebuttonLeaderboard : spritebuttonLogin;
//        //if (this.facebookService.fbFriendPermission.ContainsPermission(FacebookService.PUBLISH_ACTIONS_PERMISSION) == false)
//        //{
//        //    PublicPermission();
//        //}
//        Debug.Log("Login completed ");
//        GameData.Instance.facebookData.ID = this.facebookService.fbUser.FBID;
//        GameData.Instance.facebookData.Name = this.facebookService.fbUser.FBName;
//        GameData.Instance.facebookData.Score = 0;
//        ServiceManager.Instance.fireBaseManager.LoadData();
//    }

//    public void ShowLeaderBoard()
//    {
//        if (isTesting == false)
//        {
//            if (this.facebookService.fbFriendPermission.ContainsPermission(FacebookService.PUBLISH_ACTIONS_PERMISSION) == true)
//            {
//                leaderBoardController.gameObject.SetActive(true);
//            }
//            else
//            {
//                this.facebookService.fbFriendPermission.PerformRequestPublishPermission((bool isOk) =>
//                {
//                    Debug.Log("PerformRequestPublishPermission : " + isOk);
//                    leaderBoardController.gameObject.SetActive(true);
//                });
//            }
//        }
//        else {
//            leaderBoardController.gameObject.SetActive(true);
//        }
//    }
//}
