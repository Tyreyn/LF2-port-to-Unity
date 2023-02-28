namespace Scripts.StateMachine.State
{
    #region Usings
    using UnityEngine;
    using Scripts.Templates;
    #endregion

    public class FastJump : TemplateState
    {
        #region Fields and Constants

        private bool m_Jumping = true;
        private bool m_Jumped = false;

        #endregion

        /// <summary>
        /// Initializes a new instatnce of the <see cref="FastJump"/> class.
        /// </summary>
        /// <param name="Player">
        /// The player gameobject.
        /// </param>
        /// <param name="StateMachine">
        /// The player statemachine.
        /// </param>
        public FastJump(GameObject Player, StateMachine StateMachine) : base(Player, StateMachine)
        {
            canMove = false;
        }

        #region Public Methods

        /// <summary>
        /// State on entry method.
        /// </summary>
        public override void OnEntry()
        {
            this.PlayerScript.Rigidbody.velocity = Vector3.zero;
            this.PlayerScript.Rigidbody.angularVelocity = Vector3.zero;
            m_Jumping = true;
            m_Jumped = false;
            Player.GetComponent<Player>().Animator.Play(this.ToString());
        }

        /// <summary>
        /// State main method.
        /// </summary>
        public override void DoState()
        {
            if (m_Jumping)
            {
                m_Jumping = false;
                this.PlayerScript.isGround = false;
                this.Rigidbody.AddForce(this.CheckDirection() * this.PlayerScript.Acc / 2, ForceMode.Impulse);
            }
            if (this.Player.transform.position.y > 1f)
            {
                m_Jumped = true;
            }
            if (this.PlayerScript.isGround && m_Jumped)
            {
                OnExit();
            }
        }

        /// <summary>
        /// State on exit method.
        /// </summary>
        public override void OnExit()
        {
            this.PlayerScript.Rigidbody.velocity = Vector3.zero;
            this.PlayerScript.Rigidbody.angularVelocity = Vector3.zero;
            StateMachine.ChangeState(StateMachine.Idle);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Check if direction of fast jump is proper
        /// because player can not only up and down in this state.
        /// </summary>
        /// <returns>
        /// Proper direction.
        /// </returns>
        private Vector3 CheckDirection()
        {
            float properDirection = this.PlayerScript.SpeedX;
            // If player want to jump up, change it to slant
            if (this.PlayerScript.SpeedZ != 0 && this.PlayerScript.SpeedX == 0)
            {
                properDirection = this.PlayerScript.SpriteRenderer.flipX ? -1 : 1;
            }

            return new Vector3(
                properDirection * 0.95f,
                1.25f,
                this.PlayerScript.SpeedZ * 0.95f);
        }

        #endregion
    }
}
