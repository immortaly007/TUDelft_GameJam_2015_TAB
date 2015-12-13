using UnityEngine;
using System.Collections;
using System;

public class GameObjectFlags : MonoBehaviour {
    [Flags]
    public enum MatrixFlags
    {
        StoreOnCheckpoint = 1
    }

    public MatrixFlags flags;
}
