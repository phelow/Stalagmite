using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerStalagmiteGeneration : MonoBehaviour
{
    [SerializeField]
    private Stalagmite m_stalagmite;
    // Use this for initialization
    void Start()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Body")
        {
            m_stalagmite.TriggerGeneration();
        }
    }
}
