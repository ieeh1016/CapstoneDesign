using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BE2_Ins_BreakLoop : BE2_InstructionBase, I_BE2_Instruction
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

    I_BE2_Instruction _parentLoopInstruction;
    I_BE2_Instruction[] _parentConditionInstructions;

    protected override void OnButtonStop()
    {
        _parentLoopInstruction = BE2_BlockUtils.GetParentInstructionOfType(this, BlockTypeEnum.loop);
        _parentConditionInstructions = BE2_BlockUtils.GetParentInstructionOfTypeAll(this, BlockTypeEnum.condition).ToArray();
    }

    public override void OnStackActive()
    {
        
        _parentLoopInstruction = BE2_BlockUtils.GetParentInstructionOfType(this, BlockTypeEnum.loop);
        _parentConditionInstructions = BE2_BlockUtils.GetParentInstructionOfTypeAll(this, BlockTypeEnum.condition).ToArray();
    }

    public new void Function()
    {

        Transform parent = transform.parent.parent.parent;

        Debug.Log($"parent: {parent}");

        if(parent != null)
        {
            parent.GetComponent<BE2_Ins_Repeat>().ExecuteNextInstruction();
        }
        //부모에 있는 반복문 섹션을 찾으

            //if (_parentLoopInstruction != null)
            //{


            //    // v2.4 - bugfix: fixed condition blocks not being reset on a loop break
            //    foreach (I_BE2_Instruction condIns in _parentConditionInstructions)
            //    {

            //        condIns.InstructionBase.OnStackActive();
            //    }

            //    _parentLoopInstruction.InstructionBase.ExecuteNextInstruction();
            //}
        else
        {
            ExecuteNextInstruction();
        }
    }
}
