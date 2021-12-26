using System;
using UnityEngine;

namespace DuskHaxx
{
    class RapidFire : MonoBehaviour
    {
        private AttackScript attackScript;
        bool rapidfire_bool = false;

        public void Update()
        {
            if (Input.GetKey(KeyCode.Insert) && Input.GetKeyDown("3"))
            {
                rapidfire_bool = !rapidfire_bool;
            }

            if (rapidfire_bool)
            {
                attackScript = (AttackScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(AttackScript));
                attackScript.firespeed *= 3f;
                attackScript.firespeedtimer = 300f;
            } else {
                attackScript.firespeed = 0f;
                attackScript.firespeedtimer = 0f;
            }
        }
    }
}
