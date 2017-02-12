using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    [SerializeField]
    private Rigidbody2D m_handRigidbody;

    [SerializeField]
    private GameObject m_hand;

    [SerializeField]
    private DistanceJoint2D m_joint;

    private bool m_deployed = false;


    private const float m_armProjectionForce = 10000.0f;
    private const float m_retractedDistance = 1.0f;
    private const float m_extendedDistance = 10.0f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// If retracted shoot towards targetPosition, else retract
    /// </summary>
    /// <param name="targetPosition"></param>
    public void InputTarget(Vector3 targetPosition)
    {
        if (m_deployed == false)
        {
            m_joint.distance = m_extendedDistance;
            Vector3 forceDirection = targetPosition - m_hand.transform.position;

            Vector3 forceVec = targetPosition.normalized * m_armProjectionForce;
            
            m_handRigidbody.AddForce(forceVec);
            m_deployed = true;
        }
        else
        {
            m_deployed = false;
            m_joint.distance = m_retractedDistance;
            Vector3 forceDirection = PlayerController.ms_instance.GetPlayerBody().transform.position - m_hand.transform.position;

            Vector3 forceVec = targetPosition.normalized * m_armProjectionForce;

            m_handRigidbody.AddForce(forceVec);
        }
    }

}
