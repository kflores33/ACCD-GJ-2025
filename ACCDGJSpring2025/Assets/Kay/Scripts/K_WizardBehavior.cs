using System.Collections;
using System.Data;
using UnityEngine;

public class K_WizardBehavior : MonoBehaviour
{
    [SerializeField] K_WizardStats wizardStats;

    float _currentMana;
    public float manaGain = 5;

    bool _bracedForImpact = false;

    Rigidbody _rb;
    Collider _col;

    //public Vector3 MaxZPosition;
    //public Vector3 MinZPosition;

    Vector3 _velocity = Vector3.zero;

    public enum WizardStates
    {
        Weak,
        Strong
    }
    public WizardStates CurrentState;

    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
        _col = gameObject.GetComponentInChildren<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case WizardStates.Weak:
                UpdateWeak();
                break;
            case WizardStates.Strong:
                UpdateStrong();
            break;
        }
    }

    void UpdateWeak()
    {
        // Logic for weak state
        if (_currentMana < wizardStats.MaxMana) // if current mana is less than max mana
        { 
            _currentMana += wizardStats.ManaRegenRate * Time.deltaTime; // regenerate mana over time
        }

        if (Input.GetKeyDown(KeyCode.Space) && _currentMana >= 5) // if input to switch state is pressed and there is enough mana
        {
            // switch animation state
            Debug.Log("Switching to strong state");
            CurrentState = WizardStates.Strong;
        }
    }
    void UpdateStrong()
    {
        // Logic for strong state
        if (_currentMana >= 0) // if current mana is greater than or equal to 0
        {
            _currentMana += wizardStats.ManaDepletionRate * Time.deltaTime; // deplete mana over time
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && !(_bracedForImpact)) // if input to brace for impact is pressed
        {
            // brace for impact
            _bracedForImpact = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) && _bracedForImpact) // if input to unbrace for impact is pressed
        {
            // unbrace for impact
            _bracedForImpact = false;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // if input to switch state is pressed
        {
            // switch animation state
            Debug.Log("Switching to weak state");
            CurrentState = WizardStates.Weak;
        }
    }

    private void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        float moveDir = 0;

        if (inputHorizontal != 0 || inputVertical != 0)
        {
            if (inputHorizontal < 0 || inputVertical > 0)
            {
                // move up
                moveDir = 1;
            }
            else
            {
                // move down
                moveDir = -1;
            }

            _velocity.z = Mathf.MoveTowards(_velocity.z, moveDir * wizardStats.Speed, wizardStats.Acceleration * Time.fixedDeltaTime);
        }
        else if (inputVertical == 0 && inputHorizontal == 0)
        {
            // decelerate
            _velocity = Vector3.MoveTowards(_velocity, Vector3.zero, wizardStats.Deceleration * Time.fixedDeltaTime);
        }

        //if (transform.position.z >= MaxZPosition.z || transform.position.z <= MinZPosition.z) { _velocity = Vector3.zero; return; } // set velocity to zero if above max vertical position

        ApplyMovement();
    }

    private void ApplyMovement() => _rb.linearVelocity = _velocity;
}
