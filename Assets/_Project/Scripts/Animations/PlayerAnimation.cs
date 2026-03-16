using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [Header("Animator Params")]
    [SerializeField] private string _verticalSpeedParamName = "vSpeed";
    [SerializeField] private string _horizontalSpeedParamName = "hSpeed";
    [SerializeField] private string _walkBool = "isWalking";
    [SerializeField] private string _runBool = "isRunning";
    [SerializeField] private string _fallBool = "isFalling";
    [SerializeField] private string _jumpTrig = "JumpUp";
    [SerializeField] private string _landTrig = "Landing";

    [Header("Falling")]
    [SerializeField] private float _fallThreshold = -0.1f;

    private PlayerController _pc;
    private Rigidbody _rb;
    private Animator _anim;

    private bool _wasGrounded;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
        _pc = GetComponentInParent<PlayerController>();
        _rb = _pc.GetComponent<Rigidbody>();
        _wasGrounded = _pc.isGrounded;
    }

    private void SetHorizontalSpeedParam(float dirx)
    {
        if (_anim) _anim.SetFloat(_horizontalSpeedParamName, dirx);
    }

    private void SetVerticalSpeedParam(float diry)
    {
        if (_anim) _anim.SetFloat(_verticalSpeedParamName, diry);
    }

    private void SetDirectionalSpeedParams(Vector2 direction)
    {
        SetHorizontalSpeedParam(direction.x);
        SetVerticalSpeedParam(direction.y);
    }
    public void SetBoolParam(string stateParam, bool value)
    {
        if (_anim) _anim.SetBool(stateParam, value);
    }

    public void SetTriggerParam(string stateParam)
    {
        if (_anim) _anim.SetTrigger(stateParam);
    }
    public void TriggerJumpUp()
    {
        if (!_anim) return;
        _anim.ResetTrigger(_landTrig);
        _anim.SetBool(_fallBool, false);
        _pc.isFalling = false;
        _anim.SetTrigger(_jumpTrig); // attiva trigger per saltare ==> JumpUp
    }

    private void Update()
    {
        if (!_pc || !_anim || !_rb) return;

        // Check iniziale
        Vector3 dir = _pc.GetDirection(); // prendi direzione del player
        bool moving = dir.magnitude > 0.01f; // si sta muovendo ????
        bool grounded = _pc.isGrounded; // E' A TERRA ???
        float yVel = _rb.velocity.y; // PRENDO LA VELOCITà VERTICALE PER CAPIRE QUANDO STA' CADENDO

        if (_pc.isGrounded && !_pc.isFiring)
        {
            if (moving)
            { // si muove !!
                if (_pc.isRunning) // Sta correndo ???
                {
                    _anim.SetBool(_runBool, true);
                    _anim.SetBool(_walkBool, false);
                }
                else
                { // non corre allora cammina
                    _anim.SetBool(_walkBool, true);
                    _anim.SetBool(_runBool, false);
                }

                _anim.SetFloat(_horizontalSpeedParamName, dir.x);
                _anim.SetFloat(_verticalSpeedParamName, dir.z);
            }
            else
            { // non si muove
                _anim.SetBool(_walkBool, false);
                _anim.SetBool(_runBool, false);

                _anim.SetFloat(_horizontalSpeedParamName, 0f);
                _anim.SetFloat(_verticalSpeedParamName, 0f);
            }
        }
        else // NON è a terra quindi sta saltando o cadendo quindi disattivo le animazioni walking e running !!
        {
            _anim.SetBool(_walkBool, false);
            _anim.SetBool(_runBool, false);
        }

        if (_pc.isFiring) // ferma tutto e permetti di eseguire l'anim dello sparo senza sovrapposizioni di movimento !
        {
            _anim.SetBool(_walkBool, false);
            _anim.SetBool(_runBool, false);
            _anim.SetFloat(_horizontalSpeedParamName, 0f);
            _anim.SetFloat(_verticalSpeedParamName, 0f);
        }

        if (!grounded) // sei in aria
        {
            bool shouldFall = yVel < _fallThreshold;
            if (shouldFall != _pc.isFalling) // stai cadendo in verticale 
            {
                _pc.isFalling = shouldFall;
                _anim.SetBool(_fallBool, _pc.isFalling);
            }
        }
        else // ok allora sei a terra ora
        {
            // quindi falling deve essere off
            if (_pc.isFalling)
            {
                _pc.isFalling = false;
                _anim.SetBool(_fallBool, false);
            }
        }
        if (!_wasGrounded && grounded) // prima non era a terra ora lo è !!!
        {
            _anim.SetBool(_fallBool, false);
            _pc.isFalling = false;
            SetTriggerParam(_landTrig);
        }
        _wasGrounded = grounded;
    }
}