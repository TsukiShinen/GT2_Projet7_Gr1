using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormLordController : Enemy
{
    public float Range;

    public GameObject FirstAttackBox;
    public GameObject SecondAttackBox;

    public StormLordTargetState TargetState { get; private set; }
    public StormLordWanderState WanderState { get; private set; }
    public StormLordAttackState AttackState { get; private set; }

    public override void Awake()
    {
        base.Awake();

        TargetState = new StormLordTargetState(this);
        WanderState = new StormLordWanderState(this);
        AttackState = new StormLordAttackState(this);
    }

    public override void Start()
    {
        base.Start();

        FirstAttackBox.GetComponent<HitPlayer>().damage = Attack;
        SecondAttackBox.GetComponent<HitPlayer>().damage = Attack;

        ChangeState(WanderState);
    }

    public override void Hit(Vector2 knockBack, int damage)
    {
        base.Hit(knockBack, damage);
        AudioManager.Instance.Play("EnemyHurt");
    }

    public override IEnumerator Death()
    {
        AudioManager.Instance.Stop("BossMusic");
        AudioManager.Instance.Play("VictoryMusic");
        return base.Death();
    }
}
