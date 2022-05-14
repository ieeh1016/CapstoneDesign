using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectBtns : MonoBehaviour
{


    [SerializeField]
    Define.CharBtnState _state;

    GameObject Choose_Char;
    GameObject Current_Chosen;
    GameObject Locked;

    public void Awake()
    {
        Init();
        CharacterSelectBtnState(_state);
    }

    public void Init()
    {
        _state = Define.CharBtnState.Locked;
        Choose_Char = transform.Find("Choose_Char").gameObject;
        Current_Chosen = transform.Find("Current_Chosen").gameObject;
        Locked = transform.Find("Locked").gameObject;
    }

    public void CharacterSelectBtnState(Define.CharBtnState state)
    {
        Choose_Char.SetActive(false);
        Current_Chosen.SetActive(false);
        Locked.SetActive(false);

        switch (state)
        {
            case Define.CharBtnState.Chooseable:
                Choose_Char.SetActive(true);
                break;

            case Define.CharBtnState.Chosen:
                Current_Chosen.SetActive(true);
                break;

            case Define.CharBtnState.Locked:
            default:
                Locked.SetActive(true);
                break;
        }

        _state = state;
    }

    public void CharacterChosen()
    {
        CharacterSelectBtnState(Define.CharBtnState.Chosen);
    }
}
