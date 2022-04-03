using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapManager
{
    //List<string> Map = new List<string>();
    Dictionary<int, GameObject> Map = new Dictionary<int, GameObject>();
    static int blockId = 0;

    const int _mapWidth = 20;

    enum Direction
    {
        up = 0,
        right = 1,
        down = 2,
        left = 3,
    }


    enum Setting // 맵 생성 위한 배경 타일과 블록 크기, 배경 타일에 대한 카메라 상대위치
    {
        BlockStartPosition = -75,
        BlockWidth = 4,
        CameraPositionY = 80,
    }

    float _blockStartHeight = 0.1f;
    float _cameraRotationX = 81f;


    public bool GenerateMap() // 씬 이름과 동일한 텍스트 파일 불러와서 맵 생성
    {
        string sceneName =  SceneManager.GetActiveScene().name;

        GameObject go = Managers.Resource.Instantiate("Island");
        if (go == null)
        {
            Debug.Log("Generating tile failed");
            return false;
        }

        int rowCount = 0;
        foreach (string line in System.IO.File.ReadLines(Application.dataPath + $"/Resources/MapGeneratingFiles/{sceneName}.txt"))
        {
            if (line.Length > 20) // 가로로 놓을 수 있는 블록의 최대 개수 20
            {
                Debug.Log("Too many Blocks on a row");
                return false;
            }

            float currentBlockZStartPosition = -(float)Setting.BlockStartPosition - (int)Setting.BlockWidth * rowCount;

            for (int colCount = 0; colCount < line.Length; colCount++)
            {
                string name = null;
                GameObject block = null;
                

                switch (line[colCount])
                {
                    case 'S':
                        name = "StartBlock";
                        break;
                    case 'B':
                        name = "SeaBlock";
                        break;
                    case '0':
                        name = "Nothing";
                        break;
                    case '1':
                        name = "Block";
                        break;
                    case 'E':
                        name = "EndBlock";
                        break;
                    default:
                        Debug.Log("Wrong charcter.");
                        return false;
                }

                if (name.Equals("Nothing"))
                {
                    Map.Add(blockId++, null);
                    continue;
                }
                else
                {
                    block = Managers.Resource.Instantiate($"{name}", go.transform);
                    block.transform.localPosition = new Vector3((float)Setting.BlockStartPosition + (int)Setting.BlockWidth * colCount, _blockStartHeight, currentBlockZStartPosition);
                    block.AddComponent<Block>();
                    block.GetComponent<Block>().BlockId = blockId;
                    Map.Add(blockId++, block);

                    if (name.Equals("StartBlock"))
                    {
                        Managers.TargetObject.GetTargetObject("Character").transform.position = block.transform.position;
                        Managers.TargetObject.GetTargetObject("Character").GetComponent<Character>().CurrentPositionInMap = colCount;
                    }
                }
            }
            rowCount++;
        }

        return true;
    }

    public Dictionary<int, GameObject> GetMap()
    {
        return Map;
    }

    public int Move(int direction, int velocity)
    {
        BE2_TargetObject targetObject = Managers.TargetObject.GetTargetObjectComponent();
        int currentPositionInMap = targetObject.CurrentPositionInMap;
        switch(direction)
        {
            case (int)Direction.up:
                direction = -_mapWidth;
                break;
            case (int)Direction.right:
                direction = 1;
                break;
            case (int)Direction.down:
                direction = _mapWidth;
                break;
            case (int)Direction.left:
                direction = -1;
                break;
            default:
                break;

        }


        while (velocity > 0)
        {

            int dy = direction / _mapWidth;
            int dx = direction % _mapWidth;

            GameObject block = null;
            if ((currentPositionInMap + dy + dx) >= 0 && (currentPositionInMap + dy) / _mapWidth < Map.Count && (currentPositionInMap % _mapWidth) + dx < _mapWidth)
            {
                if (Map.TryGetValue(currentPositionInMap + dy + dx, out block) && block != null)
                {
                    currentPositionInMap += direction;
                    targetObject.CurrentPositionInMap = currentPositionInMap;
                }

            }

            velocity--;
        }

        

        return currentPositionInMap;
    }

    public void Clear()
    {
        Map.Clear();
    }
}
