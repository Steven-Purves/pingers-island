using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerUtilities : MonoBehaviour
{
    internal int GetSpawnNumber(int level)
    {
        switch (level)
        {
            case 1:
                return 1;
            case 2:
                return 3;
            case 3:
                return 5;
            case 4:
                return 7;
            default:
                return 3;
        }
    }
}
