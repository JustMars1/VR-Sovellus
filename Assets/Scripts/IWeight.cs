using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IWeight
{
    public bool InsideScaleArea { get; }
    public int TotalWeight(HashSet<IWeight> weighted);
}
