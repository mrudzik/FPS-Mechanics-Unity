﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
    public bool    isDead;
    
    public void Die()
    {
        isDead = true;
    }
}
