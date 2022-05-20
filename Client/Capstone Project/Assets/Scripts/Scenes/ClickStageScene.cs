using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClickStageScene : MonoBehaviour
{
    GameObject canvas;
    GameObject codeBlockImage;

    byte horizontalButtonNum = 10;
    byte verticalButtonNum = 5;
    int buttonStartXPosition = -860;
    int buttonStartYPosition = 384;
    int buttonInterval = 154;
    int buttonWidthHeight = 134;
    int imageStartXPosition = 0;
    int imageStartYPosition = -465;

    Dictionary<GameObject, bool> buttonWithTrueFalse = new Dictionary<GameObject, bool>();

    // Start is called before the first frame update
    void Start()
    {
        canvas = Managers.Resource.Instantiate("ClickStageCanvas");



        string sceneName = SceneManager.GetActiveScene().name;

        // 코드 블록 이미지 로딩

        canvas.transform.Find("CodeBlockImage").GetComponent<Image>().sprite = Resources.Load<Sprite>($"MapGeneratingFiles/ClickStage/Image/{sceneName}CodeBlock") as Sprite;

        //codeBlockImage = Managers.Resource.Instantiate($"MapGeneratingFiles/ClickStage/{sceneName}CodeBlock");
        //codeBlockImage.GetOrAddComponent<RectTransform>().sizeDelta = new Vector2()

        TextAsset asset = Resources.Load<TextAsset>($"MapGeneratingFiles/ClickStage/{sceneName}");
        string str = asset.text;
        string[] splitLines = str.Split('\n');
        int lines = splitLines.Length;

        for (int i = 0; i < lines; i++)
            splitLines[i] = splitLines[i].Trim('\r');

        for (int i = 0; i < lines; i++)
        {
            if (splitLines[i].Length > horizontalButtonNum)
            {
                Debug.Log("Horizontal button exceed limit");
                // Scene Change
            }
            for (int j = 0; j < splitLines[i].Length; j++)
            {
                GameObject obj;

                if (splitLines[i][j] == 'S') // 캐릭터 이미지 로드
                {
                    obj = Managers.Resource.Instantiate("ClickStageCharacterImage", canvas.transform);
                    obj.GetOrAddComponent<RectTransform>().sizeDelta = new Vector2(buttonWidthHeight, buttonWidthHeight);
                    obj.GetComponent<Image>().sprite = Resources.Load<Sprite>($"MapGeneratingFiles/ClickStage/Image/{Managers.User.Character}") as Sprite;
                }

                else
                {
                    obj = Managers.Resource.Instantiate("ClickStageButton", canvas.transform);

                    Button btn = obj.GetOrAddComponent<Button>();

                    if (splitLines[i][j] == '0') // 실패 버튼
                        btn.onClick.AddListener(Failed);
                    else // 성공 버튼
                        btn.onClick.AddListener(Success);
                }

                obj.transform.localPosition = new Vector3(buttonStartXPosition + buttonInterval * j, buttonStartYPosition - buttonInterval * i, 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Success()
    {
        Debug.Log("Success");
        Managers.Stage.HandleSuccess();
    }

    void Failed()
    {
        Debug.Log("Failed");
        Managers.Stage.HandleFailed();
    }
}