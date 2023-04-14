// <copyright file="Arrow.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.Weapons
{
    #region Usings

    using Assets.Scripts.Templates;
    using Assets.Scripts.Variables.Enums;
    using System;
    using UnityEngine;

    #endregion Usings

    /// <summary>
    /// The arrow game object.
    /// </summary>
    public class Arrow : WeaponTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Arrow"/> class.
        /// </summary>
        public Arrow()
        {
        }

        /// <summary>
        /// Performs once, on gameobject start.
        /// </summary>
        public void Start()
        {
            this.launchForce = 10.0f;
            this.manaCost = 5;
            this.damage = 10;
            Vector3 tmp = this.CheckDirection(this.properDirection) * this.launchForce;
            Debug.Log(string.Format(
                "[{0}] direction vector: {1}",
                this.name,
                tmp));
            this.GetComponent<Rigidbody>().AddForce(
                tmp,
                ForceMode.Impulse);
            this.TakeMp();
        }

        /// <summary>
        /// Performs once per frame.
        /// </summary>
        public void Update()
        {
            if (this.startRemovalTime != 0
                && Time.time - this.startRemovalTime > 5f)
            {
                Destroy(this.gameObject);
            }
        }

        /// <summary>
        /// Set weapon wearer.
        /// </summary>
        /// <param name="player">
        /// Weapon wearer.
        /// </param>
        public override void SetWearer(GameObject player)
        {
            base.SetWearer(player);
            this.GetComponent<SpriteRenderer>().flipX = this.facing == 1 ? false : true;
            if (this.inAir)
            {
                var rotationVector = this.transform.rotation.eulerAngles;
                rotationVector.z = this.facing == -1 ? 45f : -45f;
                this.transform.rotation = Quaternion.Euler(rotationVector);
            }
        }

        /// <summary>
        /// Performs action after weapon hit enemy.
        /// </summary>
        protected override void PerformActionAfterHit()
        {
            this.GetComponent<CapsuleCollider>().enabled = false;
            Destroy(this.gameObject);
        }
    }
}