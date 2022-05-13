using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Select_Btns : MonoBehaviour
{
    public enum CharBtnState
    {
        Chooseable = 0,
        Chosen = 1,
        Locked = 2,
    }

    [SerializeField]
    CharBtnState _state;

    GameObject Choose_Char;
    GameObject Current_Chosen;
    GameObject Locked;

    public void Awake()
    {
        Init();
    }

    public void Init()
    {
        Choose_Char = transform.Find("Choose_Char").gameObject;
        Current_Chosen = transform.Find("Current_Chosen").gameObject;
        Locked = transform.Find("Locked").gameObject;
    }

    public void CharacterSelectBtnState(CharBtnState state)
    {
        Choose_Char.SetActive(false);
        Current_Chosen.SetActive(false);
        Locked.SetActive(false);

        switch (state)
        {
            case CharBtnState.Chooseable:
                Choose_Char.SetActive(true);
                break;

            case CharBtnState.Chosen:
                Current_Chosen.SetActive(true);
                break;

            case CharBtnState.Locked:
            default:
                Locked.SetActive(true);
                break;
        }
    }
}
