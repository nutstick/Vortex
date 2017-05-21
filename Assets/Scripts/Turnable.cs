using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnable : MonoBehaviour
{

    //public int m_ObjectNumber = 1;              // Used to identify which tank belongs to which player.  This is set by this tank's manager.
    public float m_TurnSpeed = 180f;            // How fast the tank turns in degrees per second.
    public float speed = 10f;
    public float rotationSpeed = 100f;

    private string m_TurnAxisName;              // The name of the input axis for turning.
    private Rigidbody m_Rigidbody;              // Reference used to move the tank.
    private float m_TurnInputValue;             // The current value of the turn input.
    private float m_OriginalPitch;              // The pitch of the audio source at the start of the scene.


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        // When the tank is turned on, make sure it's not kinematic.
        m_Rigidbody.isKinematic = false;

        // Also reset the input values.
        m_TurnInputValue = 0f;
    }


    private void OnDisable()
    {
        // When the tank is turned off, set it to kinematic so it stops moving.
        m_Rigidbody.isKinematic = true;
    }

    void Start()
    {
        m_TurnAxisName = "Horizontal";
    }

    // Update is called once per frame
    void Update()
    {
        m_TurnInputValue = 0.5f;
        Turn();
    }

    private void FixedUpdate()
    {
        // Adjust the rigidbodies position and orientation in FixedUpdate.
        Turn();
    }


    private void Turn()
    {
        // Determine the number of degrees to be turned based on the input, speed and time between frames.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        Space relativeTo = Space.Self;
        // Apply this rotation to the rigidbody's rotation.
        //m_Rigidbody.MoveRotation (m_Rigidbody.rotation * turnRotation);
        transform.Rotate(0, m_TurnInputValue, 0, relativeTo);
    }
}
