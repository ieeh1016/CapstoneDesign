using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MessageBoxManager
{
    bool _replayStatus = false;
    string _xmlNameToRead = null;

    public bool ReplayStatus
    {
        get { return _replayStatus; }
        set { _replayStatus = value; }
    }

    public string XmlNameToRead
    {
        get { return _xmlNameToRead; }
        set { _xmlNameToRead = value; }
    }

    public void ShowStartMessageBox()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        _xmlNameToRead = sceneName + "StartDialog";
        GameObject mb = Managers.Resource.Instantiate("MessageBox");
        MessageBoxController mbc = mb.GetComponent<MessageBoxController>();
    }

    public void ShowEndMessageBox()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        _xmlNameToRead = sceneName + "EndDialog";
        GameObject mb = Managers.Resource.Instantiate("MessageBox");
        MessageBoxController mbc = mb.GetComponent<MessageBoxController>();
    }

    public void Clear()
    {

    }
}