using System.Collections;
using System.Data;
using UnityEngine;

public class K_WizardBehavior : MonoBehaviour
{
    K_GameManager _gameManager;
    [SerializeField] K_WizardStats wizardStats;

    [SerializeField]float _currentMana;
    public float manaGain = 5;

    bool _bracedForImpact = false;
    bool _mustDelay = true;
    bool _canDepleteRegen = false;  

    Rigidbody _rb;
    Collider _col;

    Coroutine _braceForImpactCoroutine;
    Coroutine _delayCoroutine;

    float _braceForImpactCD = 0;

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
        _gameManager = FindFirstObjectByType<K_GameManager>();
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
            if (_mustDelay) { _delayCoroutine = StartCoroutine(Delay(wizardStats.ManaRegenDelay)); } // if can delay, wait for delay time
            
            if (_canDepleteRegen)_currentMana += wizardStats.ManaRegenRate * Time.deltaTime; // regenerate mana over time
        }
        else _currentMana = wizardStats.MaxMana; // if current mana is greater than max mana, set it to max mana

        if (Input.GetKeyDown(KeyCode.Space) && _currentMana >= 5) // if input to switch state is pressed and there is enough mana
        {
            // switch animation state
            Debug.Log("Switching to strong state");
            _mustDelay = true;

            CurrentState = WizardStates.Strong;
        }
    }
    void UpdateStrong()
    {
        if (_braceForImpactCD >= 0) _braceForImpactCD -= Time.deltaTime; // decrement cooldown for bracing for impact

        // Logic for strong state
        if (_currentMana >= 0) // if current mana is greater than or equal to 0...
        {
            if (_mustDelay) { _delayCoroutine = StartCoroutine(Delay(wizardStats.ManaDepletionDelay)); } // if can delay, wait for delay time

            if (_canDepleteRegen) _currentMana -= wizardStats.ManaDepletionRate * Time.deltaTime; // deplete mana over time
        }
        else _currentMana = 0;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !(_bracedForImpact) && _braceForImpactCD <= 0) // if input to brace for impact is pressed
        {
            // brace for impact
            _braceForImpactCoroutine = StartCoroutine(BraceForImpact());
        }        

        // if enemy hits while braced for impact, reduce cooldown of ability to 0 and recover mana
        // else, recover half of the mana the player would have gained if braced for impact

        if (Input.GetKeyDown(KeyCode.Space) || _currentMana < 1) // if input to switch state is pressed
        {
            // switch animation state
            Debug.Log("Switching to weak state");
            _mustDelay = true;

            CurrentState = WizardStates.Weak;
        }
    }

    private IEnumerator Delay(float time)
    {
        _canDepleteRegen = false; // stop mana regen while waiting for delay
        yield return new WaitForSeconds(time);

        _mustDelay = false;
        _canDepleteRegen = true;

        //StopCoroutine(_delayCoroutine);
        _delayCoroutine = null;
    }

    private IEnumerator BraceForImpact()
    {
        _bracedForImpact = true;
        Debug.Log("Bracing for impact");

        yield return new WaitForSeconds(wizardStats.BraceForImpactDuration);

        _braceForImpactCD = wizardStats.BraceForImpactCooldown; // set cooldown for bracing for impact

        Debug.Log("time's up, unbraced");
        _bracedForImpact = false;

        _braceForImpactCoroutine = null;
    }

    private void TakeDamage(float dmg)
    {
        _currentMana -= dmg; // reduce current mana by damage taken
        _gameManager.AddToStat(timesHit: 1); // add to times hit stat
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<C_Enemy>() != null) // if braced for impact and collides with enemy
        {
            if (_bracedForImpact)
            {
                // reduce cooldown of ability to 0 and recover mana
                Debug.Log("Reduced cooldown and recovered mana");
                _currentMana += manaGain; // example mana gain, replace with enemy's mana gain value

                // cancel coroutine
                StopCoroutine(_braceForImpactCoroutine);
                _bracedForImpact = false;
                _braceForImpactCoroutine = null;
            }
            else
            {
                _currentMana += (manaGain / 2);
            }

            _gameManager.AddToStat(enemiesKilled: 1);
        }

        if (collision.gameObject.GetComponent<C_Arrow>() != null)
        {
            // take damage
            TakeDamage(5); // example damage value
            _delayCoroutine = StartCoroutine(Delay(wizardStats.ManaRegenDelay));

            Debug.Log("ouchie zawa!!");
        }
    }

    private void ApplyMovement() => _rb.linearVelocity = _velocity;
}
