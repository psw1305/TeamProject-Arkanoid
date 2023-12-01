using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class ArkanoidBall : MonoBehaviour
{
    public Items currentItemSkill = Items.None;

    //private bool isCatch = false;

    private int _power;
    private int _maxPower;

    //private void CheckCatchActivation()
    //{
    //    if (currentItemSkill == Items.Catch)
    //    {
    //        isCatch = true;
    //    }
    //    else
    //    {
    //        isCatch = false;
    //    }

    //}

    public void CatchBall()
    {
        var ballPos = transform.position - paddleFire.transform.position;
        transform.position = paddleFire.transform.position + new Vector3(0f, 0.5f, 0f);
        ballBody.velocity = Vector3.zero;
    }

    public void ChangePower()
    {

    }
}

