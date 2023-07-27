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
    private GameObject _whenPlayClick;


    public void PlayEvents()
    {

        
        //���� ��ų�� ���ٸ� �н� 
        if(_section.childCount != 0)
        {
            //�ڵ� ���� ����
            Managers.CodingArea.SaveArea();

            BE2_BlocksStack bE2_BlocksStack = _whenPlayClick.GetComponent<BE2_BlocksStack>();
            bE2_BlocksStack._isStart = false;

            //ī�޶� ��ȯ
            GameObject.Find("QuaterView Camera").GetComponent<CameraController>().ChangeToQuarterView();


            //��� ���� ���� ����
            GameObject.Find("Canvas Blocks Selection").GetComponent<Canvas>().enabled = false;


            //����
            BE2_ExecutionManager e2ExecutionManager = GameObject.Find("BE2 Execution Manager").GetComponent<BE2_ExecutionManager>();
            e2ExecutionManager.totalNumOfBlocks = GameObject.Find("HorizontalBlock Ins WhenPlayClicked").GetComponent<BE2_BlocksStack>().InstructionsArray.Length; // ���� ������ ��� ���� ����
            Debug.Log(e2ExecutionManager.totalNumOfBlocks);
            e2ExecutionManager.PlayAfterDelay();


            //�ߴ� ��ư ��ġ ����
            _stopButton.transform.localPosition = gameObject.transform.localPosition;


            //���� ��ư, �̼� ��ư, ���� ��ư ����
            gameObject.SetActive(false);
            _missionButton.SetActive(false);
            _questionButton.SetActive(false);


        }




    }

}
