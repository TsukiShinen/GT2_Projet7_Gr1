using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public IState HandleInput();
    public void Update();
    public void FixedUpdate();
    public void Enter();
    public void Exit();
}
