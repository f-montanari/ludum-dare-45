using System;
using UnityEngine;

/// <summary>
/// Base class meant to be overwritten. This class contains the actions
/// for when the word is completed.
/// </summary>
public interface IWordAction
{
    void DoAction(Entity source, Entity target);    
}
