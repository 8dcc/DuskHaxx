using System;
using UnityEngine;

namespace DuskHaxx
{
    class RapidFire : MonoBehaviour
    {
        private AttackScript attackScript;
        bool old_unlimited_ammo_bool, first_time_aux = true;

        public void Update()
        {
            if (variables.CheatState.rapidfire_bool)
            {
                if (first_time_aux)
                {
                    old_unlimited_ammo_bool = variables.CheatState.unlimited_ammo_bool;
                    variables.CheatState.unlimited_ammo_bool = true;
                    first_time_aux = false;
                }
                attackScript = (AttackScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(AttackScript));
                attackScript.firespeed *= 3f;
                attackScript.firespeedtimer = 300f;
            } else {
                if (!first_time_aux)
                {
                    variables.CheatState.unlimited_ammo_bool = old_unlimited_ammo_bool;
                    first_time_aux = true;
                }
                attackScript.firespeed = 0f;
                attackScript.firespeedtimer = 0f;
            }
        }
    }
}
