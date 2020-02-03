using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CycleOffseter : MonoBehaviour
{

    public float TimeOffset;

    // Start is called before the first frame update
    void Start()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("TimeOffset", TimeOffset);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
