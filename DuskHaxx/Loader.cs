using UnityEngine;

namespace DuskHaxx
{
    public class Loader
    {
        public static void Init()
        {
            Loader.Load = new GameObject();
            Loader.Load.AddComponent<Main>();
            Loader.Load.AddComponent<Aimbot>();
            Loader.Load.AddComponent<InfAmmo>();
            Loader.Load.AddComponent<RapidFire>();
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
