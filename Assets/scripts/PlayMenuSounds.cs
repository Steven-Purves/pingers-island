using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayMenuSounds : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip select, hover;

    public void Click()
    {
        AudioManger.Instance.PlaySfx2D(select);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (gameObject.activeInHierarchy)
        {
            AudioManger.Instance.PlaySfx2D(hover);
        }
    }
}
