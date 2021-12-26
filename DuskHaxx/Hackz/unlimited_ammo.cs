using System;
using UnityEngine;

namespace DuskHaxx
{
    class InfAmmo : MonoBehaviour
    {
        private SelectionScript selectionScript;
        bool unlimited_ammo_bool = false;
        bool ammo_changer_bool = true;

        public void Update()
        {
            if (Input.GetKey(KeyCode.Insert) && Input.GetKeyDown("2"))
            {
                unlimited_ammo_bool = !unlimited_ammo_bool;
            }

            if (unlimited_ammo_bool)
            {
                selectionScript = (SelectionScript)GameObject.Find("WeaponAnimator").GetComponent(typeof(SelectionScript));
                for (int i = 0; i != selectionScript.ammoinventory.Length; i++)
                {
                    selectionScript.ammoinventory[i] = 420;
                }
                if (!ammo_changer_bool)
                {
                    ammo_changer_bool = true;
                }
            } else if (ammo_changer_bool) {
                for (int i = 0; i != selectionScript.ammoinventory.Length; i++)
                {
                    selectionScript.ammoinventory[i] = selectionScript.maxammo[i];
                }
                ammo_changer_bool = false;
            }
        }
    }
}
