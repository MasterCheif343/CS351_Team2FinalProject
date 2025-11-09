using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbagePickUp : ToolHit
{
    public override void Hit()
    {
        Destroy(gameObject);
    }
}
