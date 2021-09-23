using Cinemachine;
using System.Collections;
using UnityEngine;

public class Level_Camera : MonoBehaviour
{
    public CinemachineVirtualCamera vStartCam;
    public CinemachineVirtualCamera vGameCam;
    public CinemachineVirtualCamera vDeadCam;

    public GameObject spinner;

    private void Awake()
    {
        vStartCam.gameObject.transform.position = new Vector3(Random.Range(-100, 100), Random.Range(0, 10), Random.Range(-100, 100));
        Player.OnPlayerDied += PlayerDied;
    }

    private void Start()
    {
        vGameCam.gameObject.SetActive(true);
        vStartCam.gameObject.SetActive(false);
        vDeadCam.gameObject.SetActive(false);
    }

    private void PlayerDied()
    {
        vGameCam.gameObject.SetActive(false);
        vDeadCam.gameObject.SetActive(true);

        spinner.SetActive(true);
    }
}
