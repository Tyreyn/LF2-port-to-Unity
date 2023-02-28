namespace Scripts.StateMachine.State
{
    #region Usings

    using UnityEngine;
    using Scripts.Templates;

    #endregion

    /// <summary>
    /// Character run state.
    /// </summary>
    public class Run : TemplateState
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instatnce of the <see cref="Run"/> class.
        /// </summary>
        /// <param name="Player">
        /// The player gameobject.
        /// </param>
        /// <param name="StateMachine">
        /// The player statemachine.
        /// </param>
        public Run(GameObject Player, StateMachine StateMachine) : base(Player, StateMachine)
        {
        }

        #endregion

        #region Public Methods
        public override void DoState()
        {
            if (this.PlayerScript.SpeedX == 0)
            {
                OnExit();
            }
        }

        public override void OnExit()
        {
            base.OnExit();
            this.StateMachine.ChangeState(StateMachine.Idle);
        }

        #endregion
    }
}
