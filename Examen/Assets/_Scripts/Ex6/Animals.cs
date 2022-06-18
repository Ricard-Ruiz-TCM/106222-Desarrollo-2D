using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Animals
{

    protected Animals(){}

    public abstract string Name { get; }
    public abstract bool Fly { get; }
}
