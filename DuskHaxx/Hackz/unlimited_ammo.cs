using System;
using UnityEngine;

namespace DuskHaxx
{
    class InfAmmo : MonoBehaviour
    {
        private SelectionScript selectionScript;
        bool aux_ammo_bool = true;

        public void Update()
        {
            if (variables.CheatState.unlimited_ammo_bool)
            {
                selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
                for (int i = 0; i != selectionScript.ammoinventory.Length; i++)
                {
                    selectionScript.ammoinventory[i] = 420;
                }
                aux_ammo_bool = true;
            }
            else if (aux_ammo_bool)
            {
                for (int i = 0; i != selectionScript.ammoinventory.Length; i++)
                {
                    selectionScript.ammoinventory[i] = selectionScript.maxammo[i];
                }
                aux_ammo_bool = false;
            }
        }
    }
}
