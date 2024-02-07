using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum PickupType
    {
        Powerup,
        Score,
        Life
    }

    [SerializeField] PickupType currentCollectible;
    [SerializeField] float timeToDestory = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController pc = collision.GetComponent<PlayerController>();

            switch (currentCollectible)
            {
                case PickupType.Powerup:
                    pc.StartJumpForceChange();
                    break;
                case PickupType.Score:
                    pc.score++;
                    break;
                case PickupType.Life:
                    pc.lives++;
                    break;
            }
            Destroy(gameObject, timeToDestory);
        }
    }
}
