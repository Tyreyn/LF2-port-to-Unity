namespace Scripts
{
    #region Usings

    using Scripts.InputSystem;
    using Scripts.Templates;
    using Scripts.StateMachine;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.InputSystem;

    #endregion

    public class Player : MonoBehaviour
    {
        #region Fields and Constants

        [Header("Movement Variables")]
        public float SpeedX;
        public float SpeedZ;
        public float Acc = 5f;
        public bool isGround;

        [Header("RayCast")]
        public Vector3 RayCastDirection;
        public Vector3 RayCastEndPoint;
        public float RayCastDistance;
        public Ray checkGround;
        public RaycastHit checkRaycast;

        [Header("State Variables")]
        public Scripts.StateMachine.StateMachine StateMachine;
        public Stack<CharacterActionHandler> ActionQueue = new();

        [Header("Combat Variables")]
        public float TimeBeforActionExpire = 2f;

        [Header("Scripts")]
        public SpriteRenderer SpriteRenderer;
        public Animator Animator;
        public Rigidbody Rigidbody;
        public PlayerControls playerControls;
        private InputAction jump;
        private InputAction move;

        [Header("Masks")]
        public LayerMask mask;

        [Header("Debug")]
        public bool debug = true;

        #endregion

        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        private void Awake()
        {
            this.StateMachine = new StateMachine.StateMachine();
            this.playerControls = new PlayerControls();
            this.StateMachine.SetState(StateMachine.Idle);
            this.Rigidbody = this.GetComponent<Rigidbody>();
            this.SpriteRenderer = this.GetComponent<SpriteRenderer>();
            this.Animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
            this.mask = LayerMask.GetMask("Floor");
            ComboHandler.OnActivate(StateMachine.Run);
        }

        /// <summary>
        /// On enable method.
        /// </summary>
        private void OnEnable()
        {
            this.playerControls.Enable();
            move = this.playerControls.Player.Move;
            move.Enable();
            move.started += MoveStart;
            jump = this.playerControls.Player.Jump;
            jump.Enable();
            jump.performed += DoJump;
            this.RayCastDistance = (this.SpriteRenderer.bounds.size.y / 2) + 0.2f;
            this.RayCastEndPoint = transform.position + (RayCastDistance * RayCastDirection);
        }
        // Update is called once per frame
        void Update()
        {
            if (!this.StateMachine.CanPlayerMove())
            {
                this.SpeedX = move.ReadValue<Vector2>().x;
                this.SpeedZ = move.ReadValue<Vector2>().y;
            }

            if (this.SpeedX > 0)
            {
                this.SpriteRenderer.flipX = false;
            }
            else if (this.SpeedX < 0)
            {
                this.SpriteRenderer.flipX = true;
            }

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
            if (this.ActionQueue.Count > 0)
            {
                this.UpdateQueue();
                TemplateState tmpState = ComboHandler.CheckForAction(this.ActionQueue.ToArray());
                if (tmpState != null) { this.StateMachine.ChangeState(tmpState); }
            }
        }

        /// <summary>
        /// Late update method.
        /// </summary>
        private void LateUpdate()
        {
            this.isGround = this.CheckGround();
        }

        /// <summary>
        /// On disable method.
        /// </summary>
        private void OnDisable()
        {
            this.jump.Disable();
            this.move.Disable();
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
        /// <param name="context">
        /// InputAction context.
        /// </param>
        private void DoJump(InputAction.CallbackContext context)
        {
            this.AddKeyToQueue('z');
            if (this.isGround)
            {
                this.isGround = false;
                this.StateMachine.ChangeState(this.StateMachine.Jump);
            }
        }

        /// <summary>
        /// Perform method on press move key.
        /// </summary>
        /// <param name="context">
        /// InputAction context.
        /// </param>
        private void MoveStart(InputAction.CallbackContext context)
        {
            switch (context.control.name)
            {
                case "a":
                    this.AddKeyToQueue('←');
                    break;
                case "d":
                    this.AddKeyToQueue('→');
                    break;
                case "w":
                    this.AddKeyToQueue('↑');
                    break;
                case "s":
                    this.AddKeyToQueue('↓');
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Remove action if they are expired.
        /// </summary>
        private void UpdateQueue()
        {
            Debug.Log(ActionQueue.Count);

            if (ActionQueue.Peek().CheckIfExpired()) ActionQueue.Pop();
        }

        /// <summary>
        /// Add pressed key to queue.
        /// </summary>
        /// <param name="keyToAdd">
        /// Pressed key to add.
        /// </param>
        private void AddKeyToQueue(char keyToAdd)
        {
            ActionQueue.Push(new CharacterActionHandler(keyToAdd, Time.time));
        }
        #endregion
    }
}
