using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

namespace Login_Util
{
    public class LoginManager : MonoBehaviour

    {
        public GameObject Btn_Login;
        public GameObject Btn_Logout;
        public GameObject User_name_UI;
        public GameObject Btn_MyPage_Login;
        public GameObject Btn_MyPage_Logout;
        public GameObject Btn_Play_Login;
        public GameObject Btn_Play_Logout;

        private bool bWaitingForAuth = false;

        private void Awake()
        {
            // 구글 게임서비스 활성화 (초기화)
            PlayGamesPlatform.InitializeInstance(new PlayGamesClientConfiguration.Builder().Build());
            PlayGamesPlatform.DebugLogEnabled = true;
            PlayGamesPlatform.Activate();
        }

        private void Start()
        {
            //게임시작시 자동로그인
            doAutoLogin();
        }

        // 자동로그인
        public void doAutoLogin()
        {
            if (bWaitingForAuth)
                return;
            //구글 로그인이 되어있지 않다면 
            if (!Social.localUser.authenticated)
            {
                bWaitingForAuth = true;
                //로그인 인증 처리과정 (콜백함수)
                Social.localUser.Authenticate(AuthenticateCallback);
            }
        }

        // 수동로그인 
        public void OnBtnLoginClicked()
        {
            //이미 인증된 사용자는 바로 로그인 성공된다. 
            if (Social.localUser.authenticated)
            {
                Login_State();
            }
            else
                Social.localUser.Authenticate((bool success) =>
                {
                    if (success)
                    {
                        Login_State();
                    }
                    else
                    {
                        Logout_State();
                    }
                });
        }

        // 수동 로그아웃 
        public void OnBtnLogoutClicked()
        {
            ((PlayGamesPlatform)Social.Active).SignOut();
            Logout_State();
        }


        // 인증 callback
        void AuthenticateCallback(bool success)
        {
            if (success)
            {
                // 사용자 이름을 띄어줌 
                Login_State();
            }
            else
            {
                Logout_State();
            }
        }

        public void Login_State()
        {
            Btn_Login.SetActive(false);
            Btn_Logout.SetActive(true);
            User_name_UI.SetActive(true);
            User_name_UI.GetComponent<Text>().text = Social.localUser.userName;
            Btn_MyPage_Login.SetActive(true);
            Btn_MyPage_Logout.SetActive(false);
            Btn_Play_Login.SetActive(true);
            Btn_Play_Logout.SetActive(false);
        }

        public void Logout_State()
        {
            Btn_Login.SetActive(true);
            Btn_Logout.SetActive(false);
            User_name_UI.SetActive(false);
            Btn_MyPage_Login.SetActive(false);
            Btn_MyPage_Logout.SetActive(true);
            Btn_Play_Login.SetActive(false);
            Btn_Play_Logout.SetActive(true);
        }

        public void before_Test()
        {
            Btn_Login.SetActive(true);
            Btn_Logout.SetActive(false);
            User_name_UI.SetActive(false);
            Btn_MyPage_Login.SetActive(false);
            Btn_MyPage_Logout.SetActive(true);
            Btn_Play_Login.SetActive(false);
            Btn_Play_Logout.SetActive(true);
        }
    }
}