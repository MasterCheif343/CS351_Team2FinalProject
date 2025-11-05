using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* This is for the prefab brush
 * for the plot of lands */

public class Zposition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y / 100);
    }

}
