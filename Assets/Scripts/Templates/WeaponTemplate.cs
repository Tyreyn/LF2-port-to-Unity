// <copyright file="WeaponTemplate.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.Templates
{
    #region Usings

    using UnityEngine;

    #endregion Usings

    public abstract class WeaponTemplate : MonoBehaviour
    {
        [SerializeField]
        public Collision collision1;

        /// <summary>
        /// Weapon rigidbody.
        /// </summary>
        protected Rigidbody rigidbody;

        /// <summary>
        /// Force of launching bullet.
        /// </summary>
        [SerializeField]
        protected float launchForce = 0f;

        /// <summary>
        /// Calculated proper direction of throw/shot weapon.
        /// </summary>
        protected Vector2 properDirection;

        /// <summary>
        /// The wearer of weapon.
        /// </summary>
        [SerializeField]
        protected GameObject player;

        /// <summary>
        /// Start removal timestamp.
        /// </summary>
        protected float startRemovalTime;

        /// <summary>
        /// True if player is facing to right.
        /// </summary>
        private bool facing;

        /// <summary>
        /// True if weapon is range.
        /// </summary>
        [SerializeField]
        private bool isRange;

        /// <summary>
        /// The wearer of weapon main script.
        /// </summary>
        private Player playerScript;

        /// <summary>
        /// The wearer of weapon position.
        /// </summary>
        [SerializeField]
        private Vector3 characterPosition;

        /// <summary>
        /// True if wearer of weapon is in air.
        /// </summary>
        [SerializeField]
        private bool inAir;

        protected WeaponTemplate()
        {
            this.launchForce = launchForce;
        }

        /// <summary>
        /// Set weapon wearer.
        /// </summary>
        /// <param name="player">
        /// Weapon wearer.
        /// </param>
        public void SetWearer(GameObject player)
        {
            this.player = player;
            this.rigidbody = this.GetComponent<Rigidbody>();
            this.playerScript = this.player.GetComponent<Player>();
            this.inAir = !this.playerScript.isGround;
            this.facing = this.player.GetComponent<SpriteRenderer>().flipX;
            this.characterPosition = this.playerScript.GetPlayerPosition();
            this.properDirection = this.playerScript.GetPlayerSpeed();
            this.gameObject.layer = this.player.layer;
            this.GetComponent<SpriteRenderer>().flipX = this.facing;

            if (this.inAir)
            {
                var rotationVector = this.transform.rotation.eulerAngles;
                rotationVector.z = this.facing ? 45f : -45f;
                this.transform.rotation = Quaternion.Euler(rotationVector);
            }
        }

        /// <summary>
        /// Check if direction of fast jump is proper
        /// because player can not only up and down in this state.
        /// </summary>
        /// <returns>
        /// Proper direction.
        /// </returns>
        protected Vector3 CheckDirection(Vector2 properDirection)
        {
            // If player want to jump up, change it to slant
            if (this.properDirection.y != 0 && this.properDirection.x == 0)
            {
                this.properDirection.x = this.facing ? -1 : 1;
            }
            else if (this.properDirection.x == 0)
            {
                this.properDirection.x = this.facing ? -1 : 1;
            }

            return this.inAir
                ? new Vector3(
                    this.properDirection.x * 0.75f,
                    -0.5f,
                    this.properDirection.y * 0.05f)
                : new Vector3(
                    this.properDirection.x * 0.95f,
                    0,
                    this.properDirection.y * 0.5f);
        }

        /// <summary>
        /// Performs when object is set to destroy.
        /// </summary>
        protected void StartRemoval()
        {
            this.startRemovalTime = Time.time;
            Debug.Log("[Weapon] Start removal time: " + this.startRemovalTime);
        }
    }
}
