using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
public class Player : MonoBehaviour
{
    [Header("Movement Variables")]
    public float SpeedX;
    public float SpeedZ;
    public float Acc = 5f;
    public bool isGround;
    private InputAction inputAction;
    public Queue<CharacterAction> ActionQueue = new Queue<CharacterAction>();

    [Header("RayCast")]
    public Vector3 RayCastDirection;
    public Vector3 RayCastEndPoint;
    public float RayCastDistance;
    public Ray checkGround;
    public RaycastHit checkRaycast;

    [Header("State Variables")]
    public StateMachine.StateMachine StateMachine;

    [Header("Combat Variables")]
    public float ComboTimeDuration = 0.5f;
    double CurrentComboTime = 0;

    [Header("Scripts")]
    public PlayerControls playerControl;
    public Animator Animator;
    public Rigidbody Rigidbody;

    [Header("Masks")]
    public LayerMask mask;

    // Start is called before the first frame update
    void Awake()
    {
        this.StateMachine = new StateMachine.StateMachine();
        this.StateMachine.SetState(StateMachine.Idle);
        this.Rigidbody = this.GetComponent<Rigidbody>();
        this.Animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        this.mask = LayerMask.GetMask("Floor");
        this.playerControl = new PlayerControls();
        this.inputAction = this.playerControl.Player.Move;
        this.playerControl.Player.Jump.performed += DoJump;
    }
    void OnEnable()
    {
        this.inputAction.Enable();
        this.playerControl.Player.Jump.Enable();
        RayCastDistance = this.GetComponent<SpriteRenderer>().bounds.size.y / 2 + 0.2f;
        //RayCastDistance = 5;
        this.RayCastEndPoint = transform.position + (RayCastDistance * RayCastDirection);
    }
    // Update is called once per frame
    void Update()
    {
        this.SpeedX = inputAction.ReadValue<Vector2>().x * Acc;
        this.SpeedZ = inputAction.ReadValue<Vector2>().y * Acc;
        if (Time.time - CurrentComboTime >= ComboTimeDuration && this.ActionQueue.Count > 0) this.ActionQueue.Dequeue();
        this.StateMachine.DoState();
    }

    #region Public Methods

    /// <summary>
    /// Check for ground.
    /// </summary>
    /// <returns>
    /// True if is there ground.
    /// </returns>
    public bool CheckGround()
    {
        Debug.print("sprawdzam");
        checkGround = new Ray(transform.position, RayCastDirection);
        if (Physics.Raycast(ray: checkGround, hitInfo: out checkRaycast, maxDistance: RayCastDistance))
        {
            this.Rigidbody.useGravity = false;
            this.RayCastEndPoint = checkRaycast.point;
            return true;
        }
        else
        {
            this.Rigidbody.useGravity = true;
            this.RayCastEndPoint = transform.position + (RayCastDistance * RayCastDirection);
            return false;
        }
    }
    #endregion

    #region Private Methods

    /// <summary>
    /// Fixed update method.
    /// </summary>
    private void FixedUpdate()
    {
        this.isGround = this.CheckGround();
    }

    /// <summary>
    /// Draw debug gizmos.
    /// </summary>
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, this.RayCastEndPoint);
    }

    /// <summary>
    /// Perform jump. 
    /// </summary>
    private void DoJump(InputAction.CallbackContext context)
    {
        this.AddKeyToQueue(CharacterAction.Jump);
        if (this.isGround)
        {
            this.isGround = false;
            this.StateMachine.ChangeState(this.StateMachine.Jump);
        }
    }

    private void AddKeyToQueue(CharacterAction keyToAdd)
    {
        if (this.ActionQueue.Count > 10) ActionQueue = new Queue<CharacterAction>(this.ActionQueue.Take(this.ActionQueue.Count - 1));
        ActionQueue.Enqueue(keyToAdd);
        CurrentComboTime = Time.time;
    }
    #endregion
}

