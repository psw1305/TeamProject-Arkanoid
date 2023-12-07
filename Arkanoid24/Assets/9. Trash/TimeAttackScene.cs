using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeAttackScene : MonoBehaviour
{
    private StageBlueprint stageBlueprint;
    private EdgeCollider2D screenEdge;

    #region MonoBehaviour

    private void Awake()
    {
        Managers.Resource.Initialize();
        Managers.Game.Initialize();

        screenEdge = GetComponent<EdgeCollider2D>();
        //screenEdge.GenerateCameraBounds();
    }

    private void Start()
    {
        InitTAGame();
    }


    #endregion

    private void InitTAGame()
    {
        // #1. 라이프 3개로 설정 
        Managers.Game.Life = 3;

        // #2. 스코어 0점으로 시작
        //Managers.Game.Score = 0;

        // #3. 현재 레벨에 맞는 스테이지 생성
        CreateStage();

        // #4. 공 생성 후 대기
        Managers.Ball.CreateBalls();
    }

    /// <summary>
    /// 설계도에서 받은 스테이지 맵 생성
    /// </summary>
    private void CreateStage()
    {
        stageBlueprint = Managers.Game.GetCurrentStage();
        Instantiate(stageBlueprint.StageMap);
    }

}
