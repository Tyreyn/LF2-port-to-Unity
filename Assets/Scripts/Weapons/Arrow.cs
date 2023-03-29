// <copyright file="Arrow.cs" company="GG-GrubsGaming">
// Copyright (c) GG-GrubsGaming. All rights reserved.
// </copyright>

namespace Assets.Scripts.Weapons
{
    #region Usings

    using Assets.Scripts.Templates;
    using UnityEngine;

    #endregion Usings

    public class Arrow : WeaponTemplate
    {


        public Arrow()
        {
            this.launchForce = 2.0f;
        }

        public void Start()
        {
            Vector3 tmp = this.CheckDirection(this.properDirection) * this.launchForce;
            Debug.Log("arrow direction vector: " + tmp);
            this.GetComponent<Rigidbody>().AddForce(
                tmp,
                ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            //TODO

            this.collision1 = collision;
            this.rigidbody.useGravity = false;
            this.rigidbody.velocity = Vector3.zero;
            this.rigidbody.angularVelocity = Vector3.zero;
        }
    }
}