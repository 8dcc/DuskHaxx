using UnityEngine;

namespace DuskHaxx
{
    public class Loader
    {
        public static void Init()
        {
            Loader.Load = new GameObject();

            // Main
            Loader.Load.AddComponent<Main>();
            Loader.Load.AddComponent<Console>();

            // Hackz
            Loader.Load.AddComponent<Godmode>();
            Loader.Load.AddComponent<Aimbot>();
            Loader.Load.AddComponent<RapidFire>();
            Loader.Load.AddComponent<InfAmmo>();
            Loader.Load.AddComponent<Tracers>();

            // Misc
            Loader.Load.AddComponent<DebugPlayerPos>();
            Loader.Load.AddComponent<DebugPlayerFov>();

            UnityEngine.Object.DontDestroyOnLoad(Loader.Load);
        }

        public static void Unload()
        {
            _Unload();
        }

        private static void _Unload()
        {
            GameObject.Destroy(Load);
        }

        private static GameObject Load;
    }
}
