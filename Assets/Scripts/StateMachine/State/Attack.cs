namespace Scripts.StateMachine.State
{
    #region Usings

    using UnityEngine;
    using Scripts.Templates;
    using Scripts.StateMachine;

    #endregion

    /// <summary>
    /// Character attack state.
    /// </summary>
    public class Attack : TemplateState
    {
        #region Fields and Constants

        private int Combo = 0;
        private int Time = 0;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instatnce of the <see cref="Attack"/> class.
        /// </summary>
        /// <param name="Player">
        /// The player gameobject.
        /// </param>
        /// <param name="StateMachine">
        /// The player statemachine.
        /// </param>
        public Attack(GameObject Player, StateMachine StateMachine) : base(Player, StateMachine)
        {
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// State on entry method.
        /// </summary>
        public override void OnEntry()
        {
            Debug.Log(Combo);
            Player.GetComponent<Player>().Animator.Play(this.ToString() + Combo.ToString());
        }

        /// <summary>
        /// State main method.
        /// </summary>
        public override void DoState()
        {
            Time++;
            if (Time >= 360)
            {
                OnExit();
                Combo++;
            }
        }

        /// <summary>
        /// State on exit method.
        /// </summary>
        public override void OnExit()
        {
            if (Input.GetKey(KeyCode.X))
            {
                Time = 0;
                this.OnEntry();
            }
            else
            {
                Time = 0;
                Combo = 0;
                StateMachine.ChangeState(StateMachine.Idle);
            }
        }
        #endregion
    }
}