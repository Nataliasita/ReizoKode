using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObstacle : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        transform.Rotate(new Vector3(0f, 40f, 0f) * Time.deltaTime);
    }
}
