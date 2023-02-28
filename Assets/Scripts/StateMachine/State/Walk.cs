namespace Scripts.StateMachine.State
{
    #region Usings

    using UnityEngine;
    using Scripts.Templates;
    using Scripts.StateMachine;

    #endregion

    /// <summary>
    /// Character walk state.
    /// </summary>
    public class Walk : TemplateState
    {
        #region Fields and Constants
        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instatnce of the <see cref="Walk"/> class.
        /// </summary>
        /// <param name="Player">
        /// The player gameobject.
        /// </param>
        /// <param name="StateMachine">
        /// The player statemachine.
        /// </param>
        public Walk(GameObject Player, StateMachine StateMachine) : base(Player, StateMachine)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// State main method.
        /// </summary>
        public override void DoState()
        {
            if (PlayerScript.SpeedX == 0 && PlayerScript.SpeedZ == 0)
            {
                StateMachine.ChangeState(StateMachine.Idle);
            }

            this.Rigidbody.MovePosition(
                new Vector3(
                    Player.transform.position.x + (PlayerScript.SpeedX * Time.deltaTime * this.PlayerScript.Acc),
                    Player.transform.position.y,
                    Player.transform.position.z + (PlayerScript.SpeedZ * Time.deltaTime * this.PlayerScript.Acc)));
        }
        #endregion
    }
}