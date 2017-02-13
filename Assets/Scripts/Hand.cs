using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D m_handRigidbody;

    [SerializeField]
    private GameObject m_hand;

    [SerializeField]
    private DistanceJoint2D m_joint;

    [SerializeField]
    private BoxCollider2D m_collider;

    private List<FixedJoint2D> m_stickingPoints;

    [SerializeField]
    private LineRenderer m_ropeRenderer;

    private bool m_deployed = false;


    private const float m_armProjectionForce = 10000.0f;
    private const float m_retractedDistance = 1.0f;
    private const float m_extendedDistance = 15.0f;

    void Awake()
    {
        m_stickingPoints = new List<FixedJoint2D>();

        CreateDebugRope();
    }

    private void CreateDebugRope()
    {
        m_ropeRenderer = gameObject.AddComponent<LineRenderer>();
        m_ropeRenderer.numPositions = 2;
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_ropeRenderer.SetPosition(0, transform.position);
        m_ropeRenderer.SetPosition(1, PlayerController.ms_instance.GetPlayerBody().transform.position);
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

            Vector3 forceVec = forceDirection.normalized * m_armProjectionForce;

            m_handRigidbody.AddForce(forceVec);
            m_collider.enabled = true;
            m_deployed = true;
        }
        else
        {
            //Destroy all springJoints
            foreach(FixedJoint2D stickingPoint in m_stickingPoints)
            {
                StartCoroutine(DestroyObjectAfterOneFrame(stickingPoint));
            }

            m_stickingPoints = new List<FixedJoint2D>();


            m_joint.distance = m_retractedDistance;
            Vector3 forceDirection = PlayerController.ms_instance.GetPlayerBody().transform.position - m_hand.transform.position;

            Vector3 forceVec = targetPosition.normalized * m_armProjectionForce;

            m_handRigidbody.AddForce(forceVec);
            m_collider.enabled = false;
            m_deployed = false;
            m_joint.autoConfigureDistance = false;
        }
    }

    /// <summary>
    /// Destroys an object one frame later
    /// </summary>
    /// <param name="destroyableObject">The object to be destroyed</param>
    /// <returns></returns>
    private IEnumerator DestroyObjectAfterOneFrame(Object destroyableObject)
    {
        yield return new WaitForEndOfFrame();
        Destroy(destroyableObject);
    }



    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Sticky" && m_deployed)
        {
            FixedJoint2D joint = coll.gameObject.AddComponent<FixedJoint2D>();
            joint.connectedBody = m_handRigidbody;
            m_stickingPoints.Add(joint);
            m_joint.distance = Vector2.Distance(transform.position, PlayerController.ms_instance.GetPlayerBody().transform.position);

        }

    }

}
