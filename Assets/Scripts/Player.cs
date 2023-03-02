// <copyright file="Player.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts
{
    #region Usings

    using System.Collections.Generic;
    using Assets.Scripts.InputSystem;
    using Assets.Scripts.StateMachine;
    using Assets.Scripts.Templates;
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

        [Header("Movement Variables")]
        private float speedX;
        private float speedZ;
        public float Acc = 5f;
        public bool isGround;
        public bool isShooting;

        /// <summary>
        /// RayCast direction for checking ground.
        /// </summary>
        [Header("RayCast")]
        [SerializeField]
        private Vector3 GroundCheckrayCastDirection;

        public Vector3 RayCastEndPoint;
        public float RayCastDistance;
        public Ray CheckGroundValue;
        public RaycastHit CheckRaycast;

        [Header("State Variables")]
        public StateMachineClass StateMachine;

        public Stack<CharacterActionHandler> ActionQueue = new ();

        [Header("Combat Variables")]
        public float TimeBeforActionExpire = 2f;

        [Header("Scripts")]
        public SpriteRenderer SpriteRenderer;

        public Animator Animator;
        public Rigidbody Rigidbody;

        /// <summary>
        /// Player input class.
        /// </summary>
        [SerializeField]
        public PlayerControls PlayerControls;
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

        #endregion Fields and Constants

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

            this.StateMachine = new StateMachineClass();
            this.StateMachine.SetState(this.StateMachine.Idle);
            this.groundMask = LayerMask.GetMask("Floor");

            this.GroundCheckrayCastDirection = new Vector3(0, -10, 0);
            ComboHandler.OnActivate(this.StateMachine.Run);
        }

        /// <summary>
        /// On enable method.
        /// </summary>
        public void OnEnable()
        {
            this.PlayerControls.Enable();
            this.move = this.PlayerControls.Player.Move;
            this.move.Enable();
            this.move.started += this.MoveStart;
            this.jump = this.PlayerControls.Player.Jump;
            this.jump.Enable();
            this.jump.performed += this.DoJump;
            this.attack = this.PlayerControls.Player.Attack;
            this.attack.Enable();
            this.attack.started += this.DoAttack;
            this.defend = this.PlayerControls.Player.Defend;
            this.defend.Enable();
            this.defend.started += this.DoDefend;
            this.RayCastDistance = (this.SpriteRenderer.bounds.size.y / 2) + 0.2f;
            this.RayCastEndPoint = this.transform.position + (this.RayCastDistance * this.GroundCheckrayCastDirection);
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
            }
            else if (this.speedX < 0)
            {
                this.SpriteRenderer.flipX = true;
            }

            this.StateMachine.DoState();
        }

        /// <summary>
        /// Fixed update method.
        /// </summary>
        public void FixedUpdate()
        {
            if (this.ActionQueue.Count > 0)
            {
                TemplateState tmpState = ComboHandler.CheckForAction(this.ActionQueue.ToArray());
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
            this.isGround = this.CheckGround();

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
        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Draw debug gizmos.
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(this.transform.position, this.RayCastEndPoint);
        }

        /// <summary>
        /// Check for ground.
        /// </summary>
        /// <returns>
        /// True if is there ground.
        /// </returns>
        private bool CheckGround()
        {
            this.CheckGroundValue = new Ray(this.transform.position, this.GroundCheckrayCastDirection);
            if (Physics.Raycast(ray: this.CheckGroundValue, hitInfo: out this.CheckRaycast, maxDistance: this.RayCastDistance))
            {
                this.Rigidbody.useGravity = false;
                this.RayCastEndPoint = this.CheckRaycast.point;
                return true;
            }
            else
            {
                this.Rigidbody.useGravity = true;
                this.RayCastEndPoint = this.transform.position + (this.RayCastDistance * this.GroundCheckrayCastDirection);
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
            this.AddKeyToQueue('h');
            if (this.isGround)
            {
                this.isGround = false;
                this.StateMachine.ChangeState(this.StateMachine.Jump);
            }
        }

        /// <summary>
        /// Perform attack.
        /// </summary>
        /// <param name="context">
        /// InputAction context.
        /// </param>
        private void DoAttack(InputAction.CallbackContext context)
        {
            this.AddKeyToQueue('j');
            if (this.isGround)
            {
                this.StateMachine.ChangeState(this.StateMachine.Attack);
            }
            else
            {
                this.StateMachine.ChangeState(this.StateMachine.JumpAttack);
            }
        }

        /// <summary>
        /// Perform defend.
        /// </summary>
        /// <param name="context">
        /// InputAction context.
        /// </param>
        private void DoDefend(InputAction.CallbackContext context)
        {
            this.AddKeyToQueue('k');
            this.StateMachine.ChangeState(this.StateMachine.Idle);
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

        /// <summary>
        /// Creates bullet.
        /// </summary>
        private void CreateRangeBullet()
        {
            this.isShooting = true;
        }
        #endregion Private Methods
    }
}