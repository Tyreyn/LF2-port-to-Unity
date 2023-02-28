namespace Scripts.StateMachine.State
{
    #region Usings

    using UnityEngine;
    using Scripts.Templates;

    #endregion

    /// <summary>
    /// Character idle state.
    /// </summary>
    public class Idle : TemplateState
    {
        #region Fields and Constants
        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instatnce of the <see cref="Idle"/> class.
        /// </summary>
        /// <param name="Player">
        /// The player gameobject.
        /// </param>
        /// <param name="StateMachine">
        /// The player statemachine.
        /// </param>
        public Idle(GameObject Player, StateMachine StateMachine) : base(Player, StateMachine)
        {
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// State main method.
        /// </summary>
        public override void DoState()
        {
            if (Mathf.Abs(PlayerScript.SpeedX) != 0
                || Mathf.Abs(PlayerScript.SpeedZ) != 0)
            {
                StateMachine.ChangeState(StateMachine.Walk);
            }
            if (Mathf.Abs(PlayerScript.SpeedX) == 2 * Mathf.Abs(PlayerScript.Acc)
                || Mathf.Abs(PlayerScript.SpeedZ) == 2 * Mathf.Abs(PlayerScript.Acc))
            {
                StateMachine.ChangeState(StateMachine.Run);
            }
            if (Input.GetKey(KeyCode.X))
            {
                StateMachine.ChangeState(StateMachine.Attack);
            }
        }

        #endregion
    }
}