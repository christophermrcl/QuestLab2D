using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    private float duration = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (duration > 0f)
        {
            duration -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
