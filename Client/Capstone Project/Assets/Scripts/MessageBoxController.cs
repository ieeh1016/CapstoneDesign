using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class MessageBoxController : MonoBehaviour
{
    [SerializeField]
    GameObject dialog_name;

    [SerializeField]
    GameObject dialog_text;

    [SerializeField]
    GameObject dialog_Avatar;

    XmlNodeList _xmlList;

    TextEffect _textEffect;

    int clickCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _textEffect = dialog_text.GetComponent<TextEffect>();
        _xmlList = xmlRead();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(_xmlList[clickCount]["text"].InnerText);
                _textEffect.m_Message = _xmlList[clickCount]["text"].InnerText;
                dialog_name.GetComponent<Text>().text = _xmlList[clickCount]["name"].InnerText;
                StartCoroutine(_textEffect.Typing());
                
                clickCount++;
            }
        }
        catch(NullReferenceException e)
        {
            Debug.Log($"end of index, length = {_xmlList.Count}");
        }

    }

    public void DeployMessageBox()
    {

    }

    public XmlNodeList xmlRead(string path = "")
    {
        string temp = "";

        XmlDocument xml = new XmlDocument();

        xml.Load("D:\\uni\\22-1\\Challenge1StartDialog.xml"); //"D:\\test\\config.xml" == @"D:\test\config.xml" 

        XmlNodeList xmlList = xml.SelectNodes("/Message/dialog");

        return xmlList;

        //foreach (XmlNode xnl in xmlList)
        //{
        //    _xml = xnl;
        //    dialog_text.GetComponent<Text>().text = _xml["text"].InnerText;
        //    Debug.Log($"{xnl.InnerText}");
        //    yield return null;
        //}
    }

}
