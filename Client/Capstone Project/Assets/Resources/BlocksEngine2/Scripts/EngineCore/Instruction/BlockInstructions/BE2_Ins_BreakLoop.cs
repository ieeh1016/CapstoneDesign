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

    public Transform getParentBlock(Transform transform)
    {


        return transform.parent.parent.parent;
    }

    public new void Function()
    {
        Transform parent = transform;

        for(int i = 0; i < 100; i++)
        {

            parent = getParentBlock(parent);
            Debug.Log($"parent: {parent}");

            if (parent.GetComponent<BE2_Block>().Type == BlockTypeEnum.loop)
                break;
            if (parent.GetComponent<BE2_Block>().Type == BlockTypeEnum.trigger || parent == null)
            {
                parent = null;
                break;
            }

        }


        //Debug.Log($"parent: {parent}");

        if(parent != null)
        {
            Debug.Log($"{parent.name}");
            if(parent.name.Equals("HorizontalBlock Ins Repeat"))
                parent.GetComponent<BE2_Ins_Repeat>().ExecuteNextInstruction();
            else if(parent.name.Equals("HorizontalBlock Ins RepeatForever"))
                parent.GetComponent<BE2_Ins_RepeatForever>().ExecuteNextInstruction();
        }

        else
        {
            ExecuteNextInstruction();
        }
    }
}
