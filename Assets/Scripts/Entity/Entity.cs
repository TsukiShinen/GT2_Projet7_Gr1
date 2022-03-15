using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : StateMachine
{
    [Header("Entity")]
    [SerializeField]
    private EntityData EntityData;
    public Slider lifeBar;
    public ParticleSystem HurtParticle;

    public int MaxLife { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }

    public int Life { get; set; }

    public bool IsAlive { get { return Life > 0; } } 

    public Rigidbody2D Rigidbody { get; set; }
    public Animator Animator { get; set; }

    public virtual void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        if (Rigidbody == null) { Debug.LogWarning("RIGIDBODY MISSING ON : " + gameObject.name); }
        Animator = GetComponentInChildren<Animator>();

        MaxLife = EntityData.MaxLife;
        Life = MaxLife;
        if (lifeBar != null) { lifeBar.maxValue = MaxLife; lifeBar.value = Life; }
        Attack = EntityData.Attack;
        Defense = EntityData.Defense;
    }

    public void Burn()
    {
        Life -= 1;
        if (lifeBar != null) { lifeBar.value = Life; }
    }

    public virtual void Hit(Vector2 knockBack, int damage)
    {
        Life -= damage - Defense;
        if (lifeBar != null) { StartCoroutine(DecreaseLifeBar(Life)); }
        string animToPlayer = IsAlive ? "Hit" : "Die";
        Animator.SetTrigger(animToPlayer);
        StartCoroutine(GetHit(knockBack));
    }

    private IEnumerator DecreaseLifeBar(float newValue)
    {
        while (lifeBar.value != newValue)
        {
            lifeBar.value -= Time.deltaTime * 4;
            if (lifeBar.value < newValue)
            {
                lifeBar.value = newValue;
            }

            yield return null;
        }
    }

    private IEnumerator GetHit(Vector2 knockBack)
    {
        Rigidbody.AddForce(knockBack);
        if (HurtParticle != null) { HurtParticle.Play(); }
        yield return new WaitForSeconds(0.5f);
        Rigidbody.velocity = new Vector2(0f, Rigidbody.velocity.y);
    }
}
