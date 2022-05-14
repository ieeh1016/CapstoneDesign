using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaterSelect : MonoBehaviour
{
    [SerializeField]
    CharacterSelectBtns[] _btns;
    int _progress;


    private void Start()
    {
        //_progress = Managers.User.ChallengeProgress;
        _progress = 5; //테스트용으로 넣음, 실제에선 Managers.User.ChallengeProgress사용

        _btns = gameObject.GetComponentsInChildren<CharacterSelectBtns>();

        SetBtnsState();

        //if(Managers.User.Name != null)
        //    SetBtnsState(Managers.User.ChallengeProgress);


    }


    private void OnEnable()
    {
        SetBtnsState();

        //if(Managers.User.Name != null)
        //    SetBtnsState(Managers.User.ChallengeProgress);
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

                if (_progress >= int.Parse(num))
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
