using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        Rotation();
    }

    void Rotation()
    {
        Vector3 rotate = new Vector3(15, 30, 45);

        transform.Rotate(rotate * Time.deltaTime);
    }
}
