using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class BallSkillState
{
    public int BallExtraPower = 0;
    public float BallExtraSpeed = 0f;

    public GameObject Player;

    // Powers, Enlarge, Catch

    private Items _currnetSkill = Items.None;
    public Items CurrentSkill
    {
        get
        {
            return _currnetSkill;
        }
        set
        {
            ResetSkill();
            _currnetSkill = value;
        }
    }
    

    public void ResetSkill()
    {
        if (CurrentSkill == Items.Enlarge) UnEnalarge();
        _currnetSkill = Items.None;
        UnPowerUp();
        BallExtraPower = 0;
        BallExtraSpeed = 0f;
    }

    public void PowerUp()
    {
        //if (CurrentSkill == Items.Power) return; 
        CurrentSkill = Items.Power;
        BallExtraPower++;
        foreach (var ball in Managers.Game.CurrentBalls)
        {
            ball.GetComponent<Ball>().SetPower(BallExtraPower);
           
        }
    }

    private void UnPowerUp()
    {
        BallExtraPower = 0;
        foreach (var ball in Managers.Game.CurrentBalls)
        {
            ball.GetComponent<Ball>().SetPower(BallExtraPower);
            ball.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void Catch()
    {
        CurrentSkill = Items.Catch;
    }

    public void Slow()
    {
        BallExtraSpeed -= 1f;
        SetSpeed();
    }

    public void SetSpeed()
    {
        foreach (var ball in Managers.Game.CurrentBalls)
        {
            ball.GetComponent<Ball>().SetAdditionalCurrentSpeed(BallExtraSpeed);
        }
    }

    public void PlayerItem()
    {
        Managers.Game.LifeUp();
    }

    public void Lasers(GameObject player)
    {
        var bullet1 = Managers.Resource.Instantiate("Laser", player.transform.position);
        bullet1.transform.position += new Vector3(-0.5f, 0f, 0f);
        var bullet2 = Managers.Resource.Instantiate("Laser", player.transform.position);
        bullet2.transform.position += new Vector3(0.5f, 0f, 0f);
    }

    public void Enalarge(GameObject player)
    {
        //if (CurrentSkill == Items.Enlarge) return;
        CurrentSkill = Items.Enlarge;
        var playerScale = player.transform.localScale;
        player.transform.localScale = new Vector3(playerScale.x * 1.5f, playerScale.y, playerScale.z);
        Player = player;
    }
    
    public void UnEnalarge()
    {
        var playerScale = Player.transform.localScale;
        Player.transform.localScale = new Vector3(1, 1, 1);
    }

    public void Disruption()
    {
        var ball = Managers.Game.CurrentBalls[0];
        Rigidbody2D BallRb = ball.GetComponent<Rigidbody2D>();
        Vector2 BallVec = BallRb.velocity;

        InstantiateBall(ball, BallRb, BallVec, false);
        InstantiateBall(ball, BallRb, BallVec, true);
    }

    private void InstantiateBall(GameObject mainBall, Rigidbody2D BallRb, Vector2 BallVec, bool IsLeft)
    {
        float directionMultiplier = IsLeft ? -1f : 1f;
        float seta = Mathf.Atan2(BallVec.y, BallVec.x);
        Vector2 ballPos = mainBall.transform.position + new Vector3(directionMultiplier, 0, 0);
        GameObject ball = Managers.Resource.Instantiate("BallPrefab", ballPos);
        ball.GetComponent<Ball>().BallState = BallPreference.BALL_STATE.LAUNCH;
        Managers.Game.CurrentBalls.Add(ball);
        Rigidbody2D secondBallRb = ball.GetComponent<Rigidbody2D>();
        if (BallVec.x == 0)
        {
            secondBallRb.velocity = new Vector2(directionMultiplier * 1f * Mathf.Cos(45), 1f * Mathf.Sin(45));
        }
        else
        {
            secondBallRb.velocity = new Vector2(directionMultiplier * BallVec.x, BallVec.x * Mathf.Tan(seta - 45));
        }
    }
}
