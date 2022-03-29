using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Defines behaviour for board components that can flash.
/// </summary>
public interface IFlashable
{
    IEnumerator Flash();
}
