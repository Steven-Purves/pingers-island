using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_Shell_Eject : MonoBehaviour
{
    public GameObject shell;
    public Transform shellEjection;

    void Start()
    {
        PoolManager.Instance.CreatePool(shell, 25);
    }

    public void EjectShell()
    {
        PoolManager.Instance.ReuseObject(shell, shellEjection.position, shellEjection.rotation);
    }
}
