using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSkillState : MonoBehaviour 
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
    

    private void ResetSkill()
    {
        _currnetSkill = Items.None;
        UnPowerUp();
        UnEnalarge();
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

    public void SlowItemUse()
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

    public void Lasers(GameObject player, GameObject lazer)
    {
        var bullet1 = Instantiate(lazer, player.transform.position, Quaternion.identity);
        bullet1.transform.position += new Vector3(-0.5f, 1f, 0f);
        var bullet2 = Instantiate(lazer, player.transform.position, Quaternion.identity);
        bullet2.transform.position += new Vector3(0.5f, 1f, 0f);
    }

    public void Enalarge(GameObject player)
    {
        //if (CurrentSkill == Items.Enlarge) return;
        CurrentSkill = Items.Enlarge;
        var playerScale = player.transform.localScale;
        player.transform.localScale = new Vector3(playerScale.x * 1.5f, playerScale.y, playerScale.z);
    }
    
    public void UnEnalarge()
    {
        var playerScale = Player.transform.localScale;
        Player.transform.localScale = new Vector3(1, 1, 1);
    }
}
