using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSkillState : MonoBehaviour
{
    public static BallSkillState instance = null;
    private List<GameObject> _balls;

    public int _ballExtraPower
    {
        get
        {
            return _ballExtraPower;
        }
        private set { }
    }
    public float _ballExtraSpeed
    {
        get
        {
            return _ballExtraSpeed;
        }
        private set { }
    }


    public Items currentSkill
    {
        set
        {
            ResetSkill();
            currentSkill = value;
        }
        get
        {
            return currentSkill;
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        // _balls 가져오기


        _ballExtraPower = 0;
        _ballExtraSpeed = 0f;
    }

    private void ResetSkill()
    {

    }
}
