using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField]
    private Hand m_leftHand;

    [SerializeField]
    private Hand m_rightHand;

    [SerializeField]
    private GameObject m_playerBody;

    public static PlayerController ms_instance;

    public void Awake()
    {
        ms_instance = this;
    }


    public GameObject GetPlayerBody()
    {
        return m_playerBody;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        if (Input.GetMouseButtonDown(0))
        {
            m_leftHand.InputTarget(worldPosition);
        }


        if (Input.GetMouseButtonDown(1))
        {
            m_rightHand.InputTarget(worldPosition);
        }
    }
}
