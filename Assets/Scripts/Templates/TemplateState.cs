namespace Scripts.Templates
{
    #region Usings
    using UnityEngine;
    using Scripts.StateMachine;
    #endregion

    /// <summary>
    /// The template character state class.
    /// </summary>
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(GameObject))]
    public abstract class TemplateState
    {
        #region Fields and Constants

        public GameObject Player;
        public StateMachine StateMachine;
        public Rigidbody Rigidbody;
        public Player PlayerScript;
        public bool canMove;

        #endregion

        #region Constructors and Destructors
        /// <summary>
        /// Initializes a new instatnce of the <see cref="TemplateState"/> class.
        /// </summary>
        /// <param name="Player">
        /// The player gameobject.
        /// </param>
        /// <param name="StateMachine">
        /// The player statemachine.
        /// </param>
        protected TemplateState(GameObject Player, StateMachine StateMachine)
        {
            this.Player = Player;
            this.StateMachine = StateMachine;
            this.PlayerScript = this.Player.GetComponent<Player>();
            this.Rigidbody = this.Player.GetComponent<Rigidbody>();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// State on entry method.
        /// </summary>
        public virtual void OnEntry()
        {
            Player.GetComponent<Player>().Animator.Play(this.GetType().Name.ToString());
        }

        /// <summary>
        /// State main method.
        /// </summary>
        public virtual void DoState()
        {
        }

        /// <summary>
        /// State on exit method.
        /// </summary>
        public virtual void OnExit()
        {
        }

        #endregion
    }
}