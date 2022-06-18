using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSizeCommand : Command
{

    public ChangeSizeCommand(Entity entity) : base(entity){
    }

    public override void Excecute()
    {
        _entity.transform.localScale *= 2;
    }

    public override void Undo()
    {
        _entity.transform.localScale /= 2;
    }

   
}
