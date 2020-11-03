using System.Collections;
using UnityEngine;

namespace Gulch
{
    public class SpriteEffectHandler : MonoBehaviour
    {
        public float BlinkDuration = 0.1f;
        public Material MatBlink;

        void OnEnable()
        {
            GameEventListener.Instance.OnTakeDamageHandler += this.OnTakeDamageHandler;
        }

        void OnTakeDamageHandler(TakeDamageData data)
        {
            if(data.go == null)
            {
                return;
            }

            switch (data.spriteEffectType)
            {
                case SpriteEffectType.Blink:
                    StartCoroutine(DoBlink(data.go));
                    break;
                default:
                    break;
            }
        }

        IEnumerator DoBlink(GameObject go)
        {
            var sp = go.GetComponent<SpriteRenderer>();
            var matOld = sp.material;
            
            sp.material = MatBlink;

            yield return new WaitForSeconds(BlinkDuration);

            sp.material = matOld;
        }

        void OnDisable()
        {
            GameEventListener.Instance.OnTakeDamageHandler -= this.OnTakeDamageHandler;
        }
    }
}
