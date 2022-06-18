using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorCommand : Command
{
    private Color _lastColor;

    public ChangeColorCommand(Entity entity) : base(entity){
    }

    public override void Excecute()
    {
        Color c = new Color(Random.Range(0, 100) / 100.0f, Random.Range(0, 100) / 100.0f,Random.Range(0, 100) / 100.0f, 1.0f );
        _lastColor = _entity.GetComponent<SpriteRenderer>().color;
        _entity.GetComponent<SpriteRenderer>().color = c;
    }

    public override void Undo()
    {
        _entity.GetComponent<SpriteRenderer>().color = _lastColor;
    }

   
}
