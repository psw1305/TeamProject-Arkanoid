using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float _laserSpeed = 5f;

    public int _power = 1;


  
    void Update()
    {
        transform.position += new Vector3(0, _laserSpeed, 0) * Time.deltaTime;

        if (transform.position.y > 6)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            Destroy(gameObject);
        }
    }
}
