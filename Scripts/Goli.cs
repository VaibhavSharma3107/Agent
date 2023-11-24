using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goli : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag ==  "Zombie")
        {
            Debug.Log("Hurrrrrrrrrrrrrrrrrr");
        }
    }
}
