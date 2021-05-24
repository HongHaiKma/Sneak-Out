using System.Collections;

namespace PFramework
{
    public class PCoroutine : HardSingleton<PCoroutine>
    {
        public static void PStartCoroutine(IEnumerator coroutine)
        {
            SafeInstance.StartCoroutine(coroutine);
        }

        public static void PStopCoroutine(IEnumerator coroutine)
        {
            SafeInstance.StopCoroutine(coroutine);
        }
    }
}