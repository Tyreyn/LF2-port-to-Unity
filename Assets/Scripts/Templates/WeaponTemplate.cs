// <copyright file="WeaponTemplate.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.Templates
{
    #region Usings

    using UnityEngine;
    using Assets.Scripts.Variables.Enums;
    using System;
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
        public float launchForce;

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
        /// The wearer of weapon main script.
        /// </summary>
        protected Player playerScript;

        /// <summary>
        /// The mana cost of used weapon.
        /// </summary>
        protected int manaCost;

        /// <summary>
        /// The damego of used weapon.
        /// </summary>
        protected int damage;

        /// <summary>
        /// True if wearer of weapon is in air.
        /// </summary>
        [SerializeField]
        protected bool inAir;

        /// <summary>
        /// 1 if player is facing to right.
        /// </summary>
        protected double facing;

        /// <summary>
        /// True if weapon is range.
        /// </summary>
        [SerializeField]
        private bool isRange;

        /// <summary>
        /// The wearer of weapon position.
        /// </summary>
        [SerializeField]
        private Vector3 characterPosition;


        /// <summary>
        /// Initializes a new instance of the <see cref="WeaponTemplate"/> class.
        /// </summary>
        protected WeaponTemplate()
        {
            this.damage = 0;
            this.manaCost = 0;
            this.launchForce = 0;
        }

        /// <summary>
        /// Set weapon wearer.
        /// </summary>
        /// <param name="player">
        /// Weapon wearer.
        /// </param>
        public virtual void SetWearer(GameObject player)
        {
            this.player = player;
            this.rigidbody = this.GetComponent<Rigidbody>();
            this.playerScript = this.player.GetComponent<Player>();
            this.inAir = !this.playerScript.isGround;
            this.facing = this.playerScript.Facing();
            this.characterPosition = this.playerScript.GetPlayerPosition();
            this.properDirection = this.playerScript.GetPlayerSpeed();
            this.gameObject.layer = this.player.layer;

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
                this.properDirection.x = (float)this.facing;
            }
            else if (this.properDirection.x == 0)
            {
                this.properDirection.x = (float)this.facing;
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
        /// Performs when an arrow enters a collision.
        /// </summary>
        /// <param name="collision">
        /// Collision gameobject.
        /// </param>
        protected void OnCollisionEnter(Collision collision)
        {
            try
            {
                LayerMask collisionLayerMask = collision.collider.gameObject.layer;
                string collisionLayerMaskString = Enum.GetName(
                    typeof(TeamsLayerMasks),
                    collisionLayerMask.value);
                this.rigidbody.useGravity = false;
                this.rigidbody.velocity = Vector3.zero;
                this.rigidbody.angularVelocity = Vector3.zero;

                if (Enum.IsDefined(typeof(TeamsLayerMasks), collisionLayerMask.value)
                    && !this.player.layer.Equals(collisionLayerMask)
                    && collision.collider.gameObject.GetComponent<Player>().canGetHit)
                {
                    collision.collider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    collision.collider.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    Debug.Log(string.Format(
                        "[{0}][Collision][{1}] collision with: {2}",
                        this.name,
                        this.player.name,
                        collisionLayerMaskString));
                    this.TakeHp(collision.collider.gameObject);
                    this.PerformActionAfterHit();
                }

                this.StartRemoval();
            }
            catch (Exception ex)
            {
                Debug.LogError(string.Format(
                    "[{0}][Collision][{1}] Unrecognize enum: {2}",
                    this.name,
                    this.player.name,
                    ex));
            }
        }

        /// <summary>
        /// Performs when object is set to destroy.
        /// </summary>
        protected virtual void StartRemoval()
        {
            this.startRemovalTime = Time.time;
            Debug.Log(string.Format(
                "[Weapon][{0}] Start removal time: {1}",
                this.player.name,
                this.startRemovalTime));
        }

        /// <summary>
        /// Take HP on hit.
        /// </summary>
        protected virtual void TakeHp(GameObject enemy)
        {
            Player playerScript = enemy.GetComponent<Player>();
            Debug.Log(string.Format(
                "[Combat] {0} hit {1} with {2}[{3}dmg]",
                this.player.name,
                enemy.name,
                this.name,
                this.damage));
            playerScript.CharacterHP -= this.damage;
            playerScript.isGettingHit = true;
            playerScript.SpriteRenderer.flipX = this.facing == 1 ? true : false;
        }

        /// <summary>
        /// Take MP wearer.
        /// </summary>
        protected virtual void TakeMp()
        {
            Debug.Log(string.Format(
                "[Combat] {0} costs {1} {2}",
                this.name,
                this.player.name,
                this.manaCost));
            this.playerScript.CharacterMP -= this.manaCost;
        }

        /// <summary>
        /// Performs action after weapon hit enemy.
        /// </summary>
        protected virtual void PerformActionAfterHit() { }
    }
}
