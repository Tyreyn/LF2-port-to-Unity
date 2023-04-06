// <copyright file="NormalAttack.cs" company="GG-GrubsGaming">
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
    /// The normal attack gameobject.
    /// </summary>
    public class NormalAttack : WeaponTemplate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NormalAttack"/> class.
        /// </summary>
        public NormalAttack()
        {
        }

        /// <summary>
        /// Performs once, on gameobject start.
        /// </summary>
        public void Start()
        {
            this.launchForce = 0f;
            this.manaCost = 0;
            this.damage = 5;
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
            Debug.Log(string.Format(
                "[{0}][{1}] Initialize",
                this.player.name,
                this.name));
        }

        /// <summary>
        /// Performs action after weapon hit enemy.
        /// </summary>
        protected override void PerformActionAfterHit()
        {
            this.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
