using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharaterSelect : MonoBehaviour
{
    [SerializeField]
    CharacterSelectBtns[] _btns;
    int _characterProgress;


    private void Start()
    {
        _characterProgress = GetCharProgress(Managers.User.ChallangeStageInfo);

        _btns = gameObject.GetComponentsInChildren<CharacterSelectBtns>();

        SetBtnsState();

        //if(Managers.User.Name != null)
        //    SetBtnsState(Managers.User.ChallengeProgress);


    }


    private void OnEnable()
    {
        _characterProgress = GetCharProgress(Managers.User.ChallangeStageInfo);
        
        SetBtnsState();

        //if(Managers.User.Name != null)
        //    SetBtnsState(Managers.User.ChallengeProgress);
    }

    int GetCharProgress(Dictionary<ushort, byte> dict)
    {

        //if (dict.Count <= 1)
        //{
        //    //Debug.Log($"max: {1}");

        //    return 1;
        //}


        //var max = dict.Keys.Max();
        ////Debug.Log($"max: {max}");

        //return max - 1;
        return 9999;
    }


    public void SetCharacter(int characterNum)
    {

        if (characterNum != 1)
            Managers.User.Character = "Character" + characterNum;
        else
            Managers.User.Character = "Character";


        SetBtnsState();
        Debug.Log($"set character: {Managers.User.Character} ");
    }

    public void SetBtnsState()
    {
        string num;

        try
        {
            foreach (CharacterSelectBtns btn in _btns)
            {
                num = btn.gameObject.name.Substring(3);

                if (_characterProgress >= int.Parse(num))
                {
                    if (btn.transform.parent.name.Equals(Managers.User.Character))
                        btn.CharacterSelectBtnState(Define.CharBtnState.Chosen);
                    else
                        btn.CharacterSelectBtnState(Define.CharBtnState.Chooseable);
                }
                else
                {
                    btn.CharacterSelectBtnState(Define.CharBtnState.Locked);
                }
            }
        }
        catch(Exception e)
        {
            Debug.Log($"fail to set Character select buttons: {e}");
        }

    }
}
