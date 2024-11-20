using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlashVisual : MonoBehaviour
{
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
        {
            Destroy(this.gameObject);
        }
    }
}
