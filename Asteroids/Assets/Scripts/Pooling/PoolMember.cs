using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolMember : MonoBehaviour {

    public event Action OnDestroyEvent;

    private void OnDisable()
    {
        if (OnDestroyEvent != null)
            OnDestroyEvent();
    }
}
