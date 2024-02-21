using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public abstract class Enemy : MonoBehaviour
{
    //private - private to the class that has created it and is a property only of that class. Even child classes cannot access this variable. Outside the class, even with reference to this object, we cannot grab a private variable or function.
    //public - its a party, everyone has access. This could be considered a public property of that class. Rigidbody.velocity is a vector3 public property on the rigidbody class. This is risk though because it allows a lot of changes to happen to your classes internal state from outside the class which can be difficult to test. 
    //protected - private but accessable to all child classes as well.

    protected SpriteRenderer sr;
    protected Animator anim;

    protected int health;
    protected int maxHealth;


    // Start is called before the first frame update
    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        if (maxHealth <= 0)
            maxHealth = 10;

        health = maxHealth;
    }

    public virtual void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            anim.SetTrigger("Death");
            Destroy(gameObject, 2);
        }
    }
}
