using System;
using UnityEngine;

namespace DuskHaxx
{
    class Aimbot : MonoBehaviour
    {
        bool turn_camera_bool = false;
        public void Update()
        {
            if (Input.GetKey(KeyCode.Insert) && Input.GetKeyDown("1"))
            {
                turn_camera_bool = !turn_camera_bool;
            }

            if (turn_camera_bool)
            {
                MyMouseLook myMouseLook = (MyMouseLook)GameObject.Find("MainCamera").GetComponent(typeof(MyMouseLook));
                myMouseLook.yRotation = myMouseLook.yRotation + 5;
            }
        }
    }
}
