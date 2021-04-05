using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Gulch
{
    public class GameEventListener
    {
        private static GameEventListener instance = new GameEventListener();
        public static GameEventListener Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameEventListener();
                }
                return instance;
            }
        }

        public delegate void TakeDamageHandler(TakeDamageData data);
        public delegate void SpriteEffectHandler(TakeDamageData data);

        public event TakeDamageHandler OnTakeDamageHandler;
        public event Action OnPlayerDeadHandler;
        public event SpriteEffectHandler OnSpriteEffectHandler;

        public void OnTakeDamage(TakeDamageData data)
        {
            OnTakeDamageHandler?.Invoke(data);
        }

        public void OnPlayerDead(){
            OnPlayerDeadHandler?.Invoke();
        }


        // TODO: chang ethis datatype to a dedicated one
        public void OnSpriteEffect(TakeDamageData data){
            OnSpriteEffectHandler?.Invoke(data);
        }

    }

}
