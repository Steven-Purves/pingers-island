using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UpdateNavMesh : MonoBehaviour
{
    public NavMeshSurface navmeshSurface;

    // Update is called once per frame
    
    void Start()
    {
        navmeshSurface.BuildNavMesh();
    }
}
