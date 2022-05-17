using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager
{
    public static bool ToMain = false;
    public static bool ToMain2 = false;
    public static bool ToMain3 = false;
    public static bool ToMain4 = false;

    public static bool Basic = false;
    public static bool Codition = false;
    public static bool Loop = false;
    public static bool Challenge = false;


    List<I_CheckClear> _incompletedConditionList = new List<I_CheckClear>(); // 별 획득 조건 리스트 - 리스트에 추가된 클래스들의 CheckClear() 메소드를 통해 조건 만족했는지 체크
    List<string> _completedConditionList = new List<string>();

    public List<string> CompletedConditionList
    {
        get { return _completedConditionList; }
    }


    public Action<I_CheckClear> ConditionAction = null; // UI 등에서 Condition이 만족 되었을 때 알림을 받기 위한 Action
    public Action<I_CheckClear> ClearAction = null; // 스테이지가 Clear 되었을 때 알림을 받기 위한 Action



    public void ConditionSet() // 조건 설정
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.Contains("Challenge"))
        {
            _incompletedConditionList.Add(Managers.Coin);
            Managers.CodeBlock.BlockRestriction = (int)Enum.Parse(typeof(Define.StageBlock), sceneName);
            _incompletedConditionList.Add(Managers.CodeBlock);
            // TODO, Define.StageBlock에서 읽어온 값을 코드 블록을 관리하는 매니저에게 주고, 매니저는 자신의 지역 변수를 해당 값으로 설정, CheckClear 실행 시 해당 값과 코드 블록의 수 비교
        }
        _incompletedConditionList.Add(Managers.Map);
    }

    public void HandleSuccess()
    {
        UI_Finished popup = null;

        if (!SceneManager.GetActiveScene().name.Contains("Challenge")) // Challenge 스테이지가 아닐 시 팝업
        {
            GameObject go = Managers.Resource.Instantiate("StudyStage_Complete1");
            popup = go.AddComponent<UI_StudyClearPopup>();
            popup.Init();
        }
        else
        {
            GameObject go = Managers.Resource.Instantiate("ChallengeStage_Complete1");
            popup = go.AddComponent<UI_ClearPopup>();
            popup.Init();

            string sceneName = SceneManager.GetActiveScene().name;
            string tempName = Regex.Replace(sceneName, @"\D", "");
            byte challengeNum = byte.Parse(tempName);

            byte stars = 0;

            if (Managers.User.ChallangeStageInfo.TryGetValue(challengeNum, out stars))
            {
                int currentCount = Managers.Stage.CompletedConditionList.Count;
                if (currentCount > stars)
                {
                    stars = (byte)currentCount;
                    Managers.User.ChallangeStageInfo.Remove(challengeNum);
                    Managers.User.ChallangeStageInfo.Add(challengeNum, stars);
                    Managers.User.ChallangeStageInfo.Add((ushort)(challengeNum + 1), 0);
                    Managers.User.TotalStars += (ushort)currentCount;

                    C_ChallengeUpdateStars pkt = new C_ChallengeUpdateStars();
                    pkt.UId = Managers.User.UID;
                    pkt.stageId = challengeNum;
                    pkt.numberOfStars = stars;

                    Managers.Network.Send(pkt.Write());
                }
            }
            else
            {
                Debug.Log("Stage Num Error");
            }

        }
    }

    public void HandleFailed()
    {
        UI_Finished popup = null;
        GameObject go = Managers.Resource.Instantiate("Stage_fail1");
        popup = go.AddComponent<UI_FailedPopup>();
        popup.Init();
    }

    public bool CheckConditionCompleted() // 조건들이 충족되었는지 확인
    {
        foreach (I_CheckClear condition in _incompletedConditionList)
        {
            if (condition.CheckCleared() == true)
            {
                _completedConditionList.Add(condition.GetType().Name);
                //_incompletedConditionList.Remove(condition);

                if (condition is MapManager)
                {
                    //ClearAction.Invoke(condition);
                    //TODO: 서버에게 클리어, 만족한 클리어조건에 대한 정보 담은 패킷 보낸다.
                    return true;
                }
            }

        }

        return false;
    }


    public void Clear() // 씬 전환을 위한 Clear
    {
        _incompletedConditionList.Clear();
        _completedConditionList.Clear();
        ConditionAction = null;
        ClearAction = null;
    }




}
