using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodingAreaManager
{
    [SerializeField]
    public Transform _mainAreaSaved { get; set; } = null;

    [SerializeField]
    public Transform _functionAreaSaved { get; set; } = null;


    public Transform _mainArea;
    public Transform _functionArea;


    public void Init()
    {
        GameObject g = GameObject.Find("ProgrammingEnv");

        _mainArea = g.transform.Find("HorizontalBlock Ins WhenPlayClicked").Find("Section0").Find("Body");
        _functionArea = g.transform.Find("FunctionArea").Find("Section0").Find("Body");
    }


    //저장된 코드들 게임에 넣기
    public void PutArea()
    {
        //Debug.Log($"{_mainAreaSaved == null}");
        if (_mainAreaSaved != null && _mainAreaSaved.childCount > 0)
        {
            //Debug.Log($"{_mainAreaSaved.childCount}");
            while (_mainAreaSaved.transform.childCount != 0)
            {
                Transform child = _mainAreaSaved.transform.GetChild(0);
                child.SetParent(_mainArea.transform, false);
            }
        }

        if (_functionAreaSaved != null && _functionAreaSaved.childCount > 0)
        {
            //Debug.Log($"{_functionAreaSaved.childCount}");
            while (_functionAreaSaved.transform.childCount != 0)
            {
                Transform child = _functionAreaSaved.transform.GetChild(0);
                child.SetParent(_functionArea.transform, false);
            }
        }


        if (_mainAreaSaved != null)
            Managers.Resource.Destroy(_mainAreaSaved.gameObject);
        if (_functionAreaSaved != null)
            Managers.Resource.Destroy(_functionAreaSaved.gameObject);

        RefreshCodingArea(_mainArea);
        RefreshCodingArea(_functionArea);

    }

    public void SaveArea()
    {
        GameObject go = GameObject.Find("ProgrammingEnv");

        try
        {
            _mainAreaSaved = GameObject.Instantiate(go.transform.Find("HorizontalBlock Ins WhenPlayClicked").Find("Section0").Find("Body"));

            GameObject.DontDestroyOnLoad(_mainAreaSaved);
            _mainAreaSaved.name = "mainAreaSaved";
            //Debug.Log($"{_mainAreaSaved.name}");

            _functionAreaSaved = GameObject.Instantiate(go.transform.Find("FunctionArea").Find("Section0").Find("Body"));
            GameObject.DontDestroyOnLoad(Managers.CodingArea._functionAreaSaved);
            _functionAreaSaved.name = "functionAreaSaved";
            //Debug.Log($"{_functionAreaSaved.name}");
        }
        catch(NullReferenceException e)
        {
            Debug.Log($"failed to SaveArea: {e}");
        }
        
        
    }

    void RefreshCodingArea(Transform Area)
    {
        //Debug.Log("refresh called");
        if (Area != null)
        {
            int count = Area.childCount;
            GameObject[] copyObject = new GameObject[count];

            //Debug.Log("${mainBody.childCount}");

            for (int i = 0; i < count; i++)
            {
                GameObject go = Area.GetChild(i).gameObject;
                copyObject[i] = GameObject.Instantiate(go);
                copyObject[i].name = go.name;
                Managers.Resource.Destroy(go);
            }

            for (int i = 0; i < count; i++)
            {
                copyObject[i].transform.SetParent(Area, false);
            }
        }

    }

    public void Clear()
    {
        if (_mainAreaSaved != null)
            Managers.Resource.Destroy(_mainAreaSaved.gameObject);
        if (_functionAreaSaved != null)
            Managers.Resource.Destroy(_functionAreaSaved.gameObject);

        _mainArea = null;
        _functionArea = null;
    }

}