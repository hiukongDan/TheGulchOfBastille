using System.Collections.Generic;
using UnityEngine;

namespace Gulch
{
    public class TakeDamageData : EventData
    {
        public GameObject go;
        public SpriteEffectType spriteEffectType;
        public TakeDamageData(GameObject go, SpriteEffectType spriteEffectType) : base(EventType.TakeDamage)
        {
            this.go = go;
            this.spriteEffectType = spriteEffectType;
        }
    }
}
