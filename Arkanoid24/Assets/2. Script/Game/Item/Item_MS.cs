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


        var child = GameObject.FindWithTag("Ball");
        if (child.CompareTag("Ball"))
        {

            //child.GetComponent<ArkanoidBall>().currentItemSkill = Items.Catch;

        }


    }

    private void PowerItemUse(GameObject player)
    {
        Transform[] childs = GetBallsChildrenArray();
        foreach (var child in childs)
        {
            if (child.CompareTag("Ball"))
            {

                //child.GetComponent<ArkanoidBall>().power += 1;

            }
        }
    }

    private Transform[] GetBallsChildrenArray()
    {
        Transform[] childArray = balls.GetComponentsInChildren<Transform>();
        return childArray;
    }


}