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

    public class Arrow : WeaponTemplate
    {


        public Arrow()
        {
            this.launchForce = 2.0f;
        }

        /// <summary>
        /// Performs once, on gameobject start.
        /// </summary>
        public void Start()
        {
            Vector3 tmp = this.CheckDirection(this.properDirection) * this.launchForce;
            Debug.Log("[Weapon] Arrow direction vector: " + tmp);
            this.GetComponent<Rigidbody>().AddForce(
                tmp,
                ForceMode.Impulse);
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
        /// Performs when an arrow enters a collision
        /// </summary>
        /// <param name="collision">
        /// Collision gameobject.
        /// </param>
        private void OnCollisionEnter(Collision collision)
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
                    && !this.player.layer.Equals(collisionLayerMask))
                {
                    collision.collider.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    collision.collider.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                    Destroy(this.gameObject);
                    Debug.Log("[Weapon][Collision] collision with: "
                        + collisionLayerMaskString);
                    this.GetComponent<CapsuleCollider>().enabled = false;
                }

                this.StartRemoval();
            }
            catch (Exception ex)
            {
                Debug.LogError("[Weapon][Collision] Unrecognize enum: " + ex);
            }
        }
    }
}