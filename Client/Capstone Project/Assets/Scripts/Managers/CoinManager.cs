using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinManager : I_CheckClear
{
    public Dictionary<int, GameObject> CoinMap = new Dictionary<int, GameObject>();

    public float coinHeight = 1.5f;

    public bool CheckCleared()
    {
        return CoinMap.Count == 0;
    }

    //public bool GenerateCoin() // txt파일을 읽어서 코인을 생성한다. Map 생성위한 txt파일과 형식은 똑같다 (0과 1로 코인 유무 나타냄)
    //{
    //    string sceneName = SceneManager.GetActiveScene().name;

    //    int coinId = 0;

    //    TextAsset asset = Resources.Load<TextAsset>($"MapGeneratingFiles/{sceneName}Coin");
    //    string str = asset.text;
    //    string[] splitLines = str.Split('\n');
    //    int lines = splitLines.Length;

    //    for (int i = 0; i < lines; i++)
    //        splitLines[i] = splitLines[i].Trim('\r');

    //    for (int i = 0; i < lines; i++/*string line in System.IO.File.ReadLines(Application.dataPath + $"/Resources/MapGeneratingFiles/{sceneName}Coin.txt")*/)
    //    {
    //        if (splitLines[i].Length > (int)Define.Map.MapWidth) // 가로로 놓을 수 있는 블록의 최대 개수 20
    //        {
    //            Debug.Log("Too many char on a row");
    //            return false;
    //        }

    //        for (int colCount = 0; colCount < splitLines[i].Length; colCount++)
    //        {
    //            GameObject coin = null;
    //            if (splitLines[i][colCount].Equals('1'))
    //            {
    //                coin = Managers.Resource.Instantiate($"Coin", GameObject.Find("Island").transform);
    //                GameObject parentBlock = null;

    //                if (Managers.Map.GetMap().TryGetValue(coinId, out parentBlock))
    //                {
    //                    if (parentBlock == null)
    //                    {
    //                        Debug.Log($"There is no block under the coin{coinId}");
    //                        return false;
    //                    }
    //                    coin.transform.localPosition = parentBlock.transform.localPosition + new Vector3(0, coinHeight, 0);
    //                    CoinMap.Add(coinId, coin);
    //                }

    //                coin.AddComponent<Coin>();
    //                coin.GetComponent<Coin>().CoinId = coinId;
    //            }

    //            else if (!splitLines[i][colCount].Equals('0'))
    //            {
    //                Debug.Log("Wrong character in coin map");
    //                return false;
    //            }
    //            coinId++;
    //        }
    //    }
    //    return true;
    //}

    public void AcquireCoin(int mapPosition) // 전달 받은 위치에서 얻을 코인이 있다면 얻는다
    {
        GameObject coin = null;
        if (CoinMap.TryGetValue(mapPosition, out coin))
        {
            coin.SetActive(false);
            CoinMap.Remove(mapPosition);
        }  
    }

    public void AcquireCoin(GameObject coinObject) // value 기반으로 코인 획득
    {
        int mapPosition = -1;

        foreach (int keyVar in CoinMap.Keys)
            if (CoinMap[keyVar] == coinObject)
                mapPosition = keyVar;

        if (mapPosition != -1)
        {
            coinObject.SetActive(false);
            CoinMap.Remove(mapPosition);
        }
    }

    public void Clear()
    {
        CoinMap.Clear();
    }
}
