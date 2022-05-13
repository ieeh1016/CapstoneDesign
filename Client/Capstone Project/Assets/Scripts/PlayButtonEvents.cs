using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonEvents : MonoBehaviour
{

    [SerializeField]
    private Transform _section = null;


    public void PlayEvents()
    {
        _section = GameObject.Find("HorizontalBlock Ins WhenPlayClicked").transform;
        _section = _section.GetChild(0).GetChild(1);


        
        //실행 시킬게 없다면 패스 
        if(_section.childCount != 0)
        {
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

            //isMoved = true 명령 블록 조합이 캐릭터가 움직이지 않는 경우 강제조정
            BE2_TargetObject bE2_TargetObject = Managers.TargetObject.GetTargetObjectComponent();
            bE2_TargetObject.SetIsMovedTrueWithDelay(1.5f);


            //시작 버튼 제거
            gameObject.SetActive(false);

        }




    }

}
