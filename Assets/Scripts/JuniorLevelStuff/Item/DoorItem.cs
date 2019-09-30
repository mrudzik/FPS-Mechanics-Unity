using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorItem : Item
{
    [SerializeField]
    private DoorController 	door = null;

    public override void Action()
    {
        if (door != null)
            door.Functional();
    }
}
