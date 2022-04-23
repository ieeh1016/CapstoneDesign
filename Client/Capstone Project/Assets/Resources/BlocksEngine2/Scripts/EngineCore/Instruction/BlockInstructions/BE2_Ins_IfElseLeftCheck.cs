using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE2_Ins_IfElseLeftCheck : BE2_InstructionBase, I_BE2_Instruction
{
    //protected override void OnAwake()
    //{
    //
    //}

    //protected override void OnStart()
    //{
    //    
    //}

    //protected override void OnUpdate()
    //{
    //
    //}

    I_BE2_BlockSectionHeaderInput _input0;
    string _value;
    bool _isFirstPlay = true;

    protected override void OnButtonStop()
    {
        _isFirstPlay = true;
    }

    public override void OnStackActive()
    {
        _isFirstPlay = true;
    }

    public new void Function()
    {
        if (_isFirstPlay)
        {

            if (TargetObject.AbleLeft())
            {
                _isFirstPlay = false;
                ExecuteSection(0);
            }
            else
            {
                _isFirstPlay = false;
                ExecuteSection(1);
            }
        }
        else
        {
            _isFirstPlay = true;
            ExecuteNextInstruction();
        }
    }
}
