using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{
    [SerializeField] float projectileFireRate;
    [SerializeField] float distanceThreshold = 3.0f;
    float timeSinceLastFire = 0;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        if (projectileFireRate <= 0)
        {
            projectileFireRate = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.PlayerInstance) return;

        sr.flipX = (GameManager.Instance.PlayerInstance.transform.position.x < transform.position.x) ? true : false;

        AnimatorClipInfo[] curPlayingClips = anim.GetCurrentAnimatorClipInfo(0);

        float distance = Vector3.Distance(GameManager.Instance.PlayerInstance.transform.position, transform.position);

        if (distance <= distanceThreshold)
        {
            sr.color = Color.red;
            if (curPlayingClips[0].clip.name != "Fire")
            {
                if (Time.time >= timeSinceLastFire + projectileFireRate)
                {
                    anim.SetTrigger("Fire");
                    timeSinceLastFire = Time.time;
                }
            }
        }
        else
        {
            sr.color = Color.white;
        }
    }
}
