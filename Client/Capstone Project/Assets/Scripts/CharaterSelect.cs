using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterSelect : MonoBehaviour
{
    private void Start()
    {
        
    }

    public void SetCharacter(int characterNum)
    {
        
        Managers.User.Character = "Character" + characterNum;
        Debug.Log($"set character: {Managers.User.Character} ");
    }
}
