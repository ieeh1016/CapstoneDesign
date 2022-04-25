﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class BE2_TargetObject : MonoBehaviour, I_BE2_TargetObject
{
    public enum Direction
    {
        up = 0,
        right = 1,
        down = 2,
        left = 3,
        size = 4,

    }

    enum CharacterState
    {
        Idle = 0,
        Moving = 1,
        Turn = 2,

    }

    CharacterState _state;

    int currentPositionInMap;

    public int CurrentPositionInMap
    {
        get { return currentPositionInMap; }
        set { currentPositionInMap = value; }
    }

    int _velocity = 1;
    bool interactable = true;
    GameObject currentBlock = null;

    public GameObject CurrentBlock
    {
        get { return currentBlock; }
        set { currentBlock = value; }
    }


    public int currentDirection = (int)Direction.up;

    public Transform Transform => transform;
    public I_BE2_ProgrammingEnv ProgrammingEnv { get; set; }

    Animator anim;

    Queue<Vector3> TargetQueue = new Queue<Vector3>();
    Vector3 _target;
    Vector3 _startPosition;

    CameraController cameraController;

    void Start()
    {
        _target = _startPosition = this.gameObject.transform.position;


        _state = CharacterState.Idle;
        anim = GetComponent<Animator>();
        startAnimation(_state);
        isMoved = false;

        cameraController = GameObject.Find("QuaterView Camera").GetComponent<CameraController>();
    }
    
    void Update()
    {
        moveToTarget();
    }

    void startAnimation(CharacterState state)
    {
        switch (state)
        {

            case CharacterState.Idle:
                //idle
                anim.SetInteger("animation", 1);
                break;

            case CharacterState.Moving:
                //run
                anim.SetInteger("animation", 12);
                break;

            case CharacterState.Turn:
                break;
        }
    }

    [SerializeField]
    private bool isMoved;

    //캐릭터가 이동하는 모습을 위한 메서드
    void moveToTarget()
    {
        Vector3 dir = _target - this.gameObject.transform.position;

        if (dir.magnitude < 0.001f)
        {

            if (TargetQueue.Count > 0)
            {
                _target = TargetQueue.Dequeue();
                //Debug.Log($"TargetQueue.Count: {TargetQueue.Count}   _target: {_target}");
            }

            else
            {
                _state = CharacterState.Idle;
                startAnimation(_state);

                if (isMoved)
                {
                    cameraController.ChangeToOverViewWithDelay();
                    isMoved = false;
                    Debug.Log("move finished");
                }
            }
        }
        else
        {
            if (!isMoved)
                isMoved = true;
            _state = CharacterState.Moving;
            startAnimation(_state);
            this.gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, _target, _speed * Time.deltaTime);
            this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
        }
    }

    [SerializeField]
    float _speed = 10f;
    float _counter = 1.0f;


    void I_BE2_TargetObject.Move()
    {
        GameObject newBlock = null;
        int blockId = Managers.Map.Move(currentDirection, _velocity);
        if (blockId != currentBlock.GetComponent<Block>().BlockId && Managers.Map.GetMap().TryGetValue(blockId, out newBlock))
        {
            if (newBlock == null)
                return;


            Vector3 targetPostion = newBlock.transform.position + new Vector3(0, 0.9f, 0);
            TargetQueue.Enqueue(targetPostion);
            //Debug.Log($"Enqueue: {targetPostion}");

            Vector3 dir = targetPostion - this.gameObject.transform.position;

            currentBlock = newBlock;
        }

    }

    void I_BE2_TargetObject.Turn(bool clockWise)
    {
        if (clockWise)
        {
            currentDirection = (currentDirection + 1) % (int)Direction.size;
        }
        else
        {
            currentDirection = (currentDirection + (int)Direction.size - 1) % (int)Direction.size;
        }
    }





    bool I_BE2_TargetObject.AbleRight()
    {
        GameObject newBlock = null;
        int blockId = Managers.Map.MoveCheck((currentDirection + 1) % (int)Direction.size, _velocity);
        if (blockId != currentBlock.GetComponent<Block>().BlockId && Managers.Map.GetMap().TryGetValue(blockId, out newBlock))
        {
            if (newBlock == null)
            {
                //Debug.Log("오른쪽 불가능1");
                return false;

            }

            //Debug.Log("오른쪽 가능");
            return true;
        }

        //Debug.Log("오른쪽 불가능2");
        return false;
    }

    bool I_BE2_TargetObject.AbleLeft()
    {
        GameObject newBlock = null;
        int blockId = Managers.Map.MoveCheck((currentDirection - 1) % (int)Direction.size, _velocity);
        if (blockId != currentBlock.GetComponent<Block>().BlockId && Managers.Map.GetMap().TryGetValue(blockId, out newBlock))
        {
            if (newBlock == null)
            {
                //Debug.Log("왼쪽 불가능1");
                return false;

            }

            //Debug.Log("왼쪽 가능");
            return true;
        }

        //Debug.Log("왼쪽 불가능2");
        return false;
    }

    bool I_BE2_TargetObject.AbleForward()
    {
        GameObject newBlock = null;
        int blockId = Managers.Map.MoveCheck(currentDirection, _velocity);
        if (blockId != currentBlock.GetComponent<Block>().BlockId && Managers.Map.GetMap().TryGetValue(blockId, out newBlock))
        {
            if (newBlock == null)
            {
                //Debug.Log("전진 불가능1");
                return false;

            }

            //Debug.Log("전진 가능");
            return true;
        }

        //Debug.Log("전진 불가능2");
        return false;
    }

}
