using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormLordAttackState : IState
{
    private StormLordController _stormLordController;

    private bool _isAttacking;

    public StormLordAttackState(StormLordController controller)
    {
        _stormLordController = controller;
    }

    public IState HandleInput()
    {
        if (!_isAttacking) { return _stormLordController.TargetState; }

        return this;
    }

    public void Update()
    {
        if (Mathf.Sign((_stormLordController.Detection.PlayerPosition - _stormLordController.transform.position).x) != Mathf.Sign(_stormLordController.transform.GetChild(0).localScale.x) && _stormLordController.Rigidbody.velocity.x != 0)
        {
            _stormLordController.Flip();
        }
    }

    public void FixedUpdate()
    {

    }

    public void Enter()
    {
        _isAttacking = true;
        _stormLordController.Rigidbody.velocity = Vector3.zero;
        _stormLordController.StartCoroutine(Attack());
    }

    public IEnumerator Attack()
    {
        string name = Random.Range(0, 3) == 0 ? "Attack1" : "Attack2";
        _stormLordController.Animator.SetTrigger(name);
        if (name == "Attack1")
        {
            AudioManager.Instance.Play("StormLordHit1_1");
            yield return new WaitForSeconds(1.25f);
            _stormLordController.FirstAttackBox.SetActive(true);
            AudioManager.Instance.Play("StormLordHit1_2");
            yield return new WaitForSeconds(0.333f);
            _stormLordController.FirstAttackBox.SetActive(false);
            yield return new WaitForSeconds(0.25f);
        }
        else if(name == "Attack2")
        {
            yield return new WaitForSeconds(0.5f);
            _stormLordController.SecondAttackBox.SetActive(true);
            AudioManager.Instance.Play("StormLordHit2");
            yield return new WaitForSeconds(0.1666f);
            _stormLordController.SecondAttackBox.SetActive(false);
            yield return new WaitForSeconds(0.08333f);
        }
        _isAttacking = false;
    }

    public void Exit()
    {

    }
}
