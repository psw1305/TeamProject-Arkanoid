using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSkillState
{
    public static BallSkillState instance = null;

    public int BallExtraPower = 0;
    public float BallExtraSpeed = 0f;

    public Items CurrentSkill
    {
        get; set;
    } = Items.None;

    private void ResetSkill()
    {

    }

    public void PowerUp()
    {
        BallExtraPower++;
        foreach (var ball in Managers.Game.CurrentBalls)
        {
            ball.GetComponent<ArkanoidBall>().SetPower(BallExtraPower);
        }
    }

    public void Catch()
    {

    }

    public void SlowItemUse()
    {
        BallExtraSpeed -= 1f;
        SetSpeed();
    }

    public void SetSpeed()
    {
        foreach (var ball in Managers.Game.CurrentBalls)
        {
            ball.GetComponent<ArkanoidBall>().SetMaxSpeed(BallExtraSpeed);
        }
    }
}
