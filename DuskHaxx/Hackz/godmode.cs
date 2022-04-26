using System.Collections.Generic;
using UnityEngine;

namespace DuskHaxx
{
    class Godmode : MonoBehaviour
    {
        bool aux_first_time = true;

        public void Update()
        {
            if (variables.CheatState.godmode_bool)
            {
                if (aux_first_time) aux_first_time = false;

                PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement) GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
                playerHealthManagement.myhealth = 1337f;
                playerHealthManagement.myarmor = 1337f;
            }
            else if (!aux_first_time)       // Restore normal health n shit
            {
                PlayerHealthManagement playerHealthManagement = (PlayerHealthManagement)GameObject.Find("Player").GetComponent(typeof(PlayerHealthManagement));
                playerHealthManagement.myhealth = 100f;
                playerHealthManagement.myarmor = 100f;

                aux_first_time = true;
            }
        }
    }
}
