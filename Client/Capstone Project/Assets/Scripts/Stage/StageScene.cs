using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageScene : BaseScene
{
    GameObject be2ProgEnv = null;

    [SerializeField]
    private bool IncludeFuntionBlock = false;
    protected override void Init()
    {
        base.Init();
        SceneType = Define.Scene.Game;
        bool success = Managers.Map.GenerateMap();
        success = Managers.Coin.GenerateCoin();


        if (!success) // 맵, 코인 둘 중 하나라도 생성 실패 시
        {
            SceneManager.LoadScene("MainPage");
            StageManager.ToMain = true;
        }
        else
        {
            GameObject character = Managers.TargetObject.GetTargetObject("Character");
            Vector3 startPosition = character.transform.position;
            if (startPosition == null)
            {
                Debug.Log("StartPosition wasn't set");
                Managers.Scene.LoadScene(Define.Scene.Lobby);
            }

            if (IncludeFuntionBlock)
                be2ProgEnv = Managers.Resource.Instantiate("Blocks Engine 2 with function");
            else
                be2ProgEnv = Managers.Resource.Instantiate("Blocks Engine 2");

            Managers.CodeBlock.BE2ProgEnv = be2ProgEnv;
            if (be2ProgEnv == null)
            {
                Debug.Log("Wrong engine name");
                Managers.Scene.LoadScene(Define.Scene.Lobby);
            }

            else
            {
                Transform quarterViewCamera = be2ProgEnv.transform.Find("QuaterView Camera");
                if (quarterViewCamera != null)
                    quarterViewCamera.GetComponent<CameraController>().Player = character;
            }
                

        }


    }
    public override void Clear()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
