using System.Collections.Generic;
using UnityEngine;

namespace Gulch
{
    public class TakeDamageData : EventData
    {
        public GameObject go;
        public SpriteEffectType spriteEffectType;
        public float spriteEffectDuration;
        public TakeDamageData(GameObject go, SpriteEffectType spriteEffectType, float spriteEffectDuration=0.1f) : base(EventType.TakeDamage)
        {
            this.go = go;
            this.spriteEffectType = spriteEffectType;
            this.spriteEffectDuration = spriteEffectDuration;
        }
    }
}
