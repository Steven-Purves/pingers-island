using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Reuse_bone_Parts : PoolObject
{
    public Rigidbody myRigidbody;
    public override void OnObjReuse()
    {
        myRigidbody.velocity = new Vector3(0f, 0f, 0f);
        myRigidbody.angularVelocity = new Vector3(0f, 0f, 0f);
    }

}
