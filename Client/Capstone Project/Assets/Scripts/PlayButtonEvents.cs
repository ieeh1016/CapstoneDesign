using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonEvents : MonoBehaviour
{

    [SerializeField]
    private Transform _section;
    [SerializeField]
    private GameObject _stopButton;
    [SerializeField]
    private GameObject _missionButton;
    [SerializeField]
    private GameObject _questionButton;
    [SerializeField]
    private GameObject _whenPlayClicked;


    public void PlayEvents()
    {

        
        //실행 시킬게 없다면 패스 
        if(_section.childCount != 0)
        {
            try
            {
                _whenPlayClicked.GetComponent<BE2_BlocksStack>()._isStart = false;


                //코딩 영역 저장
                Managers.CodingArea.SaveArea();


                //카메라 전환
                GameObject.Find("QuaterView Camera").GetComponent<CameraController>().ChangeToQuarterView();


                //블록 선택 영역 제거
                GameObject.Find("Canvas Blocks Selection").GetComponent<Canvas>().enabled = false;


                //실행
                BE2_ExecutionManager e2ExecutionManager = GameObject.Find("BE2 Execution Manager").GetComponent<BE2_ExecutionManager>();
                e2ExecutionManager.totalNumOfBlocks = GameObject.Find("HorizontalBlock Ins WhenPlayClicked").GetComponent<BE2_BlocksStack>().InstructionsArray.Length; // 실행 시점의 블록 개수 저장
                Debug.Log(e2ExecutionManager.totalNumOfBlocks);
                e2ExecutionManager.PlayAfterDelay();


                //중단 버튼 위치 변경
                _stopButton.transform.localPosition = gameObject.transform.localPosition;


                //시작 버튼, 미션 버튼, 설명 버튼 제거
                gameObject.SetActive(false);
                _missionButton.SetActive(false);
                _questionButton.SetActive(false);
            }
            catch (NullReferenceException e)
            { Debug.Log("error on start game"); }



        }




    }

}
