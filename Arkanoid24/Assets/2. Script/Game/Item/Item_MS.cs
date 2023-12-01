using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public partial class Item : MonoBehaviour
{
    private void CatchItemUse()
    {
        //Transform[] childs = GetBallsChildrenArray();
        //foreach (var child in childs)
        //{
        //    if (child.CompareTag("Ball"))
        //    {
        //        child.GetComponent<ArkanoidBall>().currentItemSkill = Items.Catch;
        //    }
        //}

        //Transform[] childs = GetBallsChildrenArray();


      
            Managers.Skill.CurrentSkill = itemType;



    }


    private Transform[] GetBallsChildrenArray()
    {
        Transform[] childArray = balls.GetComponentsInChildren<Transform>();
        return childArray;
    }


}