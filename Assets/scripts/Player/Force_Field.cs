using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Force_Field : MonoBehaviour
{
    public AudioClip forceFieldUp, forceFieldDown, forceFieldHit;
    public Player player;
    public SphereCollider sphereCollider;

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
        sphereCollider.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();

        if (damageable != null)
        {
            damageable.TakeHit(100, true);
            AudioManger.Instance.PlaySfx2D(forceFieldHit);
        }
    }

    public void ShieldsUp()
    {
        player.isVulnerable = false;
        LeanTween.scale(gameObject, new Vector3(4, 4, 4), 2f).setEase(LeanTweenType.easeOutElastic);
        sphereCollider.enabled = true;
        AudioManger.Instance.PlaySfx2D(forceFieldUp);
        Invoke(nameof(ShieldsDown), 5);
    }

    private void ShieldsDown()
    {
        AudioManger.Instance.PlaySfx2DWithDelay(forceFieldDown,1.5f);
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 2).setEase(LeanTweenType.easeInElastic);
        player.isVulnerable = false;
        sphereCollider.enabled = false;
    }
}
