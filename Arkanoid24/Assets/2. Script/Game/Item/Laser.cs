using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float _laserSpeed = 5f;


    void Start()
    {
        
    }

  
    void Update()
    {
        transform.position += new Vector3(0, _laserSpeed, 0) * Time.deltaTime;

        if (transform.position.y > 6)
            Destroy(gameObject);
    }
}
