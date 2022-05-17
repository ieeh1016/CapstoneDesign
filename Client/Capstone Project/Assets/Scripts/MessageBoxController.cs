using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MessageBoxController : MonoBehaviour
{
    [SerializeField]
    GameObject dialog_name;

    [SerializeField]
    GameObject dialog_text;

    [SerializeField]
    GameObject dialog_Avatar;

    [SerializeField]
    GameObject PressToContinue;

    XmlNodeList _xmlList;

    TypingEffect _textEffect;

    //string _state;



    int _clickCount;


    private void Start()
    {
        Init();

    }

    private void OnEnable()
    {
        Init();
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateMessageBox();

    }

    private void Init()
    {
        _textEffect = dialog_text.GetComponent<TypingEffect>();
        _xmlList = xmlRead();

        _clickCount = 0;
        DeployMessage(_clickCount);
        _clickCount++;
    }

    private void UpdateMessageBox()
    {

        if (_textEffect.isTyping == false)
        {
            if (!PressToContinue.activeSelf)
                PressToContinue.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                DeployMessage(_clickCount);
                _clickCount++;
            }
        }
        else
        {
            if (PressToContinue.activeSelf)
                PressToContinue.SetActive(false);

        }

    }


    private void DeployMessage(int seq)
    {
        try
        {
            if (_clickCount >= _xmlList.Count)
            {
                if (Managers.MessageBox.XmlNameToRead.Contains("Start"))
                {
                    GameObject stageScene = GameObject.Find("@Scene");
                    stageScene.GetComponent<StageScene>().StartGenerating();
                }
                else
                {
                    Managers.Stage.HandleSuccess();
                }
                Destroy(gameObject);
            }
            else
            //Debug.Log(_xmlList[seq]["text"].InnerText);
            {
                _textEffect.m_Message = _xmlList[seq]["text"].InnerText;
                dialog_name.GetComponent<Text>().text = _xmlList[seq]["name"].InnerText;
                StartCoroutine(_textEffect.Typing());
            }
        }
        catch(NullReferenceException e)
        {
            //Debug.Log($"end of dialog, current state is {_state}");

            //if (_state == Define.MessageBoxState.Start)
            //    _state = Define.MessageBoxState.End;
            //else
            //    _state = Define.MessageBoxState.Start;

            gameObject.SetActive(false);
        }

    }

    public XmlNodeList xmlRead()
    {

        string path = string.Format("DialogScript/{0}", Managers.MessageBox.XmlNameToRead);

        try
        {
            TextAsset asset = Resources.Load<TextAsset>(path);

            //Debug.Log($"xml content: {asset.text} ");

            XmlDocument xml = new XmlDocument();

            xml.LoadXml(asset.text); //"D:\\test\\config.xml" == @"D:\test\config.xml" 

            XmlNodeList xmlList = xml.SelectNodes("/Message/dialog");

            return xmlList;

        }
        catch (NullReferenceException e)
        {
            Debug.Log($"fail to load DialogScript from {path}");
            Clear();
            gameObject.SetActive(false);
            return null;
        }


        void Clear()
        {
            _clickCount = 0;
            _textEffect = null;
            _xmlList = null;
        }


        //foreach (XmlNode xnl in xmlList)
        //{
        //    _xml = xnl;
        //    dialog_text.GetComponent<Text>().text = _xml["text"].InnerText;
        //    Debug.Log($"{xnl.InnerText}");
        //    yield return null;
        //}
    }

}
