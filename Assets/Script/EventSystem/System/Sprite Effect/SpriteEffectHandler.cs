using System.Collections;
using UnityEngine;

namespace Gulch
{
    public class SpriteEffectHandler : MonoBehaviour
    {
        public float BlinkDuration = 0.1f;
        public Material MatBlink;
        public Material MatBinkDark;
        public Material MatNeon;
        public Material MatNormal;

        void OnEnable()
        {
            GameEventListener.Instance.OnTakeDamageHandler += this.OnTakeDamageHandler;
            GameEventListener.Instance.OnSpriteEffectHandler += this.OnTakeDamageHandler;
        }
        void OnDisable()
        {
            GameEventListener.Instance.OnTakeDamageHandler -= this.OnTakeDamageHandler;
            GameEventListener.Instance.OnSpriteEffectHandler -= this.OnTakeDamageHandler;
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
                    StartCoroutine(DoBlink(data.go, MatBlink, data.spriteEffectDuration));
                    break;
                case SpriteEffectType.BlinkDark:
                    StartCoroutine(DoBlink(data.go, MatBinkDark, data.spriteEffectDuration));
                    break;
                case SpriteEffectType.NeonColor:
                    StartCoroutine(SwapMat(data.go, MatNeon, data.spriteEffectDuration));
                    break;
                default:
                    break;
            }
        }

        IEnumerator SwapMat(GameObject go, Material swapMat, float duration){
            var sp = go.GetComponent<SpriteRenderer>();
            sp.material = swapMat;
            yield return new WaitForSeconds(duration);
            sp.material = MatNormal;
        }

        IEnumerator DoBlink(GameObject go, Material blinkMat, float duration)
        {
            var sp = go.GetComponent<SpriteRenderer>();
            var matOld = sp.material;
            
            while(duration >= 0){
                sp.material = blinkMat;
                yield return new WaitForSeconds(BlinkDuration/2);
                sp.material = matOld;
                yield return new WaitForSeconds(BlinkDuration/2);
                duration -= BlinkDuration;
            }
            
            sp.material = matOld;
        }


    }
}
