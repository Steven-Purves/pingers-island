using UnityEngine;

public class Script_Flag_Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);
    } 
}