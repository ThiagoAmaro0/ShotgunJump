using System;
using UnityEngine;
public abstract class Strategy
{
    public abstract void Start(BaseEnemy baseAI);
    public abstract void Update(BaseEnemy baseAI);
    public abstract void Exit(BaseEnemy baseAI);
}
