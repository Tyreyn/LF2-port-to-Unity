// <copyright file="Player.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts
{
    #region Usings

    using System.Collections.Generic;
    using System.Linq;
    using Assets.Scripts.InputSystem;
    using Assets.Scripts.StateMachine;
    using Assets.Scripts.Templates;
    using TMPro;
    using UnityEditor.XR;
    using UnityEngine;
    using UnityEngine.InputSystem;

    #endregion Usings

    /// <summary>
    /// Main player class.
    /// </summary>
    public class Player : MonoBehaviour
    {
        #region Fields and Constants

        [Header("Player information")]
        /// <summary>
        /// The player index.
        /// </summary>
        public int playerIndex = 0;

        public string CharacterName = "Henry";

        [Header("Movement Variables")]
        [SerializeField]
        private float speedX;
        [SerializeField]
        private float speedZ;
        public float Acc = 5f;
        public bool isGround;
        public bool isDefending;
        public bool isSprinting;
        public bool isOnFire;
        public bool isOnIce;
        public bool isAttacking;
        public bool isJumping;
        public bool isCatching;
        public bool isCaught;
        public bool isGettingHit;
        public bool canGetHit;
        public bool isEnemyOnHeadLevel;
        public bool isEnemyOnLegLevel;
        public bool isObject;

        /// <summary>
        /// RayCast direction for checking ground.
        /// </summary>
        [Header("RayCast")]
        [SerializeField]
        private Vector3 GroundCheckrayCastDirection;
        public Vector3 GroundRayCastEndPoint;
        public float GroundRayCastDistance;
        public Ray CheckGroundValue;
        public RaycastHit CheckGroundRaycast;
        [SerializeField]
        private Vector3 AHeadCheckRayCastDirection;
        [SerializeField]
        private float AHeadCastDistance;
        public Ray CheckHeadValue;
        public RaycastHit CheckHeadRaycast;
        public Ray CheckLegValue;
        public RaycastHit CheckLegRaycast;
        private Vector3 HeadRayCastStartPoint;
        private Vector3 HeadRayCastEndPoint;
        private Vector3 LegRayCastStartPoint;
        private Vector3 LegRayCastEndPoint;

        [Header("State Variables")]
        public StateMachineClass StateMachine;
        public Stack<CharacterActionHandler> ActionQueue = new();

        [Header("Combat Variables")]
        public float TimeBeforActionExpire = 2f;

        [Header("Scripts")]
        public SpriteRenderer SpriteRenderer;
        public ComboHandler comboHandler;
        public Animator Animator;
        public Rigidbody Rigidbody;

        /// <summary>
        /// Player input class.
        /// </summary>
        [SerializeField]
        public PlayerControls PlayerControls;
        public PlayerInput playerInput;
        private InputAction jump;
        private InputAction move;
        private InputAction defend;
        private InputAction attack;

        /// <summary>
        /// The ground layer mask.
        /// </summary>
        [Header("Masks")]
        [SerializeField]
        private LayerMask groundMask;
        [SerializeField]
        private LayerMask enemyMask;
        [SerializeField]
        private LayerMask objectMask;

        #endregion Fields and Constants

        #region Constructs and Deconstructs
        #endregion

        #region Public Methods

        /// <summary>
        /// Start is called before the first frame update.
        /// </summary>
        public void Awake()
        {
            // Player Components
            this.Animator = this.GetComponent<Animator>();
            this.Rigidbody = this.GetComponent<Rigidbody>();
            this.SpriteRenderer = this.GetComponent<SpriteRenderer>();
            this.PlayerControls = new PlayerControls();
            this.playerInput = this.GetComponent<PlayerInput>();
            this.playerIndex = this.playerInput.playerIndex;
            //this.playerInput.SwitchCurrentActionMap(string.Format("Player{0}", this.playerIndex));
            this.StateMachine = new StateMachineClass(this.gameObject);
            this.StateMachine.SetState(this.StateMachine.Idle);
            this.groundMask = LayerMask.GetMask("Floor");
            this.enemyMask = LayerMask.GetMask("Enemy");
            this.objectMask = LayerMask.GetMask("Object");
            this.isCaught = false;
            this.isAttacking = false;
            this.isCatching = false;
            this.isDefending = false;

        }

        /// <summary>
        /// On enable method.
        /// </summary>
        public void OnEnable()
        {
            this.PlayerControls.Enable();
            if (this.playerIndex == 0)
            {
                this.move = this.PlayerControls.Player.Move;
                this.jump = this.PlayerControls.Player.Jump;
                this.attack = this.PlayerControls.Player.Attack;
                this.defend = this.PlayerControls.Player.Defend;
            }
            else if (this.playerIndex == 1)
            {
                this.move = this.PlayerControls.Player1.Move;
                this.jump = this.PlayerControls.Player1.Jump;
                this.attack = this.PlayerControls.Player1.Attack;
                this.defend = this.PlayerControls.Player1.Defend;
            }

            this.move.Enable();
            this.move.started += this.MoveStart;
            this.jump.Enable();
            this.jump.performed += this.DoJump;
            this.attack.Enable();
            this.attack.started += this.DoAttack;
            this.defend.Enable();
            this.defend.started += this.DoDefend;
            this.defend.canceled += this.EndDefend;

            this.GroundRayCastDistance = (this.SpriteRenderer.bounds.size.y / 2) + 0.2f;
            this.GroundCheckrayCastDirection = new Vector3(0, -1, 0);
            this.AHeadCastDistance = 0.35f;
        }

        public void Start()
        {
            this.comboHandler = new ComboHandler(this);
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        public void Update()
        {
            this.speedX = this.move.ReadValue<Vector2>().x;
            this.speedZ = this.move.ReadValue<Vector2>().y;

            if (this.speedX > 0)
            {
                this.SpriteRenderer.flipX = false;
                this.AHeadCheckRayCastDirection = new Vector3(1, 0, 0);
            }
            else if (this.speedX < 0)
            {
                this.SpriteRenderer.flipX = true;
                this.AHeadCheckRayCastDirection = new Vector3(-1, 0, 0);

            }

            this.StateMachine.DoState();
        }

        /// <summary>
        /// Fixed update method.
        /// </summary>
        public void FixedUpdate()
        {
            if (this.isCaught)
            {
                this.StateMachine.ChangeState(this.StateMachine.Caught);
            }

            if (this.ActionQueue.Count > 0)
            {
                TemplateState tmpState = this.comboHandler.CheckForAction(this.ActionQueue.Reverse().ToArray());
                if (tmpState != null)
                {
                    this.StateMachine.ChangeState(tmpState);
                    this.ActionQueue.Clear();
                }
            }
        }

        /// <summary>
        /// Late update method.
        /// </summary>
        public void LateUpdate()
        {
            this.HeadRayCastStartPoint = new Vector3(
                this.transform.position.x,
                this.transform.position.y,
                this.transform.position.z + 0.05f);
            this.HeadRayCastEndPoint = new Vector3(
                this.transform.position.x + (this.AHeadCastDistance * this.AHeadCheckRayCastDirection.x),
                this.transform.position.y,
                this.transform.position.z + 0.05f);
            this.LegRayCastStartPoint = new Vector3(
                this.transform.position.x,
                this.transform.position.y,
                this.transform.position.z - 0.05f);
            this.LegRayCastEndPoint = new Vector3(
                this.transform.position.x + (this.AHeadCastDistance * this.AHeadCheckRayCastDirection.x),
                this.transform.position.y,
                this.transform.position.z - 0.05f);

            this.isGround = this.CheckGround();
            this.isEnemyOnHeadLevel = Physics.Raycast(
                origin: this.HeadRayCastStartPoint,
                direction: this.AHeadCheckRayCastDirection,
                out this.CheckHeadRaycast,
                maxDistance: this.AHeadCastDistance,
                layerMask: this.enemyMask);
            this.isEnemyOnLegLevel = Physics.Raycast(
                origin: this.LegRayCastStartPoint,
                direction: this.AHeadCheckRayCastDirection,
                out this.CheckLegRaycast,
                maxDistance: this.AHeadCastDistance,
                layerMask: this.enemyMask);
            this.isObject = Physics.Raycast(this.HeadRayCastStartPoint, this.AHeadCheckRayCastDirection, this.AHeadCastDistance, this.objectMask);

            if (this.ActionQueue.Count != 0)
            {
                this.UpdateQueue();
            }
        }

        /// <summary>
        /// On disable method.
        /// </summary>
        public void OnDisable()
        {
            this.jump.Disable();
            this.move.Disable();
            this.attack.Disable();
            this.defend.Disable();
        }

        /// <summary>
        /// Get current player speed.
        /// </summary>
        /// <returns>
        /// Player speed (x: speedX, y: SpeedY).
        /// </returns>
        public Vector2 GetPlayerSpeed()
        {
            return new Vector2(this.speedX, this.speedZ);
        }

        /// <summary>
        /// Get current player position.
        /// </summary>
        /// <returns>
        /// Player position.
        /// </returns>
        public Vector3 GetPlayerPosition()
        {
            return this.transform.position;
        }

        /// <summary>
        /// Set current player animator.
        /// </summary>
        /// <param name="newAnimator">
        /// Animator to set.
        /// </param>
        public void SetAnimator(RuntimeAnimatorController newAnimator)
        {
            this.Animator.runtimeAnimatorController = newAnimator;
        }

        /// <summary>
        /// Creates attack object.
        /// </summary>
        public void CreateAttackObject()
        {
            if (this.CharacterName == "Henry")
            {
                string tmpPath = "Sprite/Character/Weapons/arrow";
                Vector3 arrowStartPosition = new Vector3(
                    this.transform.position.x + 0.15f,
                    this.transform.position.y,
                    this.transform.position.z);
                GameObject newArrow = Instantiate(
                    Resources.Load<GameObject>(tmpPath),
                    arrowStartPosition,
                    this.transform.rotation);

                newArrow.SendMessage("SetWearer", this.gameObject);
            }
        }

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Draw debug gizmos.
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.transform.position, this.GroundRayCastEndPoint);
            Gizmos.color = this.isEnemyOnHeadLevel ? Color.red : Color.green;
            Gizmos.DrawLine(this.HeadRayCastStartPoint, this.HeadRayCastEndPoint);
            if (this.isEnemyOnLegLevel)
            {
                Gizmos.color = Color.red;
            }
            else if (this.isObject)
            {
                Gizmos.color = Color.blue;
            }
            else
            {
                Gizmos.color = Color.green;
            }

            Gizmos.DrawLine(this.LegRayCastStartPoint, this.LegRayCastEndPoint);
        }

        /// <summary>
        /// Check for ground.
        /// </summary>
        /// <returns>
        /// True if is there ground.
        /// </returns>
        private bool CheckGround()
        {
            this.GroundRayCastEndPoint = this.transform.position +
                (this.GroundRayCastDistance * this.GroundCheckrayCastDirection);
            this.CheckGroundValue = new Ray(
                this.transform.position,
                this.GroundCheckrayCastDirection);

            if (Physics.Raycast(
                ray: this.CheckGroundValue,
                hitInfo: out this.CheckGroundRaycast,
                maxDistance: this.GroundRayCastDistance,
                layerMask: this.groundMask))
            {
                this.Rigidbody.useGravity = false;
                this.GroundRayCastEndPoint = this.CheckGroundRaycast.point;
                return true;
            }
            else
            {
                this.Rigidbody.useGravity = true;
                this.GroundRayCastEndPoint = this.transform.position +
                    (this.GroundRayCastDistance * this.GroundCheckrayCastDirection);
                return false;
            }
        }

        /// <summary>
        /// Perform jump.
        /// </summary>
        /// <param name="context">
        /// InputAction context.
        /// </param>
        private void DoJump(InputAction.CallbackContext context)
        {
            this.AddKeyToQueue('J');
            this.isJumping = true;
        }

        /// <summary>
        /// Perform attack.
        /// </summary>
        /// <param name="context">
        /// InputAction context.
        /// </param>
        private void DoAttack(InputAction.CallbackContext context)
        {
            this.AddKeyToQueue('A');
            this.isAttacking = true;
        }

        /// <summary>
        /// Perform defend.
        /// </summary>
        /// <param name="context">
        /// InputAction context.
        /// </param>
        private void DoDefend(InputAction.CallbackContext context)
        {

            this.AddKeyToQueue('D');
            this.isDefending = true;

        }

        /// <summary>
        /// On end defend.
        /// </summary>
        /// <param name="context">
        /// InputAction context.
        /// </param>
        private void EndDefend(InputAction.CallbackContext context)
        {
            this.isDefending = false;
        }

        /// <summary>
        /// Perform method on press move key.
        /// </summary>
        /// <param name="context">
        /// InputAction context.
        /// </param>
        private void MoveStart(InputAction.CallbackContext context)
        {
            Vector2 tmp = context.action.ReadValue<Vector2>();
            if (tmp.x > 0)
            {
                this.AddKeyToQueue('→');
            }
            else if (tmp.x < 0)
            {
                this.AddKeyToQueue('←');
            }
            else if (tmp.y > 0)
            {
                this.AddKeyToQueue('↑');
            }
            else if (tmp.y < 0)
            {
                this.AddKeyToQueue('↓');
            }
        }

        /// <summary>
        /// Remove action if they are expired.
        /// </summary>
        private void UpdateQueue()
        {
            if (this.ActionQueue.Peek().CheckIfExpired())
            {
                this.ActionQueue.Pop();
            }
        }

        /// <summary>
        /// Add pressed key to queue.
        /// </summary>
        /// <param name="keyToAdd">
        /// Pressed key to add.
        /// </param>
        private void AddKeyToQueue(char keyToAdd)
        {
            this.ActionQueue.Push(new CharacterActionHandler(keyToAdd, Time.time));
        }

        #endregion Private Methods
    }
}