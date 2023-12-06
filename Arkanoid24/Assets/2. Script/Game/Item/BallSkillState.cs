using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class BallSkillState
{
    private int _laserFireCount = 5;
    private float _laserFireDelay = 0.3f;

    public int BallExtraPower = 0;
    public float BallExtraSpeed = 0f;
    public float BallIncreaseSpeed = 0f;

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
            ResetSkill(false);
            _currnetSkill = value;
        }
    }
    

    public void ResetSkill(bool isHardReset)
    {
        if (CurrentSkill == Items.Enlarge) UnEnalarge();
        if (isHardReset) BallExtraSpeed = 0f;
        _currnetSkill = Items.None;
        UnPowerUp();
        BallExtraPower = 0;
    }

    public void PowerUp(GameObject player)
    {
        //if (CurrentSkill == Items.Power) return; 
        CurrentSkill = Items.Power;
        BallExtraPower++;
        foreach (var ball in Managers.Ball.GetBallsForPlayer(player))
        {
            ball.GetComponent<Ball>().SetPower(BallExtraPower);
        }
    }

    private void UnPowerUp()
    {
        BallExtraPower = 0;
        foreach (var ball in Managers.Ball.GetAllBalls())
        {
            ball.GetComponent<Ball>().SetPower(BallExtraPower);
            ball.transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void Catch()
    {
        CurrentSkill = Items.Catch;
    }

    public void Slow(GameObject player)
    {
        BallExtraSpeed -= 1f;
        SetSpeed(player);
    }

    public void SetSpeed(GameObject player)
    {
        foreach (var ball in Managers.Ball.GetBallsForPlayer(player))
        {
            ball.GetComponent<Ball>().SetAdditionalCurrentSpeed(BallExtraSpeed + BallIncreaseSpeed);
        }
    }

    public void PlayerItem()
    {
        Managers.Game.LifeUp();
    }

    public void Lasers(GameObject player)
    {
        CoroutineHelper.StartCoroutine(LaserFire(player));
    }

    public IEnumerator LaserFire(GameObject player)
    {
        for(int i = 0; i < _laserFireCount; i++)
        {
            var bullet1 = Managers.Resource.Instantiate("Laser", player.transform.position);
            bullet1.transform.position += new Vector3(-0.5f, 0f, 0f);
            var bullet2 = Managers.Resource.Instantiate("Laser", player.transform.position);
            bullet2.transform.position += new Vector3(0.5f, 0f, 0f);
            yield return new WaitForSeconds(_laserFireDelay);
        }
        
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

    public void Disruption(GameObject player)
    {
        var ball = Managers.Ball.GetBallsForPlayer(player)[0];
        Rigidbody2D BallRb = ball.GetComponent<Rigidbody2D>();
        Vector2 BallVec = BallRb.velocity;

        InstantiateBall(ball, player, BallVec, false);
        InstantiateBall(ball, player, BallVec, true);
    }

    private void InstantiateBall(GameObject mainBall, GameObject player, Vector2 BallVec, bool IsLeft)
    {
        float directionMultiplier = IsLeft ? -1f : 1f;
        float seta = Mathf.Atan2(BallVec.y, BallVec.x);
        Vector2 ballPos = mainBall.transform.position + new Vector3(directionMultiplier, 0, 0);
        GameObject ball = Managers.Resource.Instantiate("BallPrefab", ballPos);
        ball.GetComponent<Ball>().BallState = BallPreference.BALL_STATE.LAUNCH;
        ball.GetComponent<Ball>().AssignPlayer(player);
        Managers.Ball.AssignBallToPlayer(player, ball);
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
