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
        public delegate void PlayerDeadHandler();

        public event TakeDamageHandler OnTakeDamageHandler;
        public event PlayerDeadHandler OnPlayerDeadHandler;

        public void OnTakeDamage(TakeDamageData data)
        {
            OnTakeDamageHandler?.Invoke(data);
        }

        public void OnPlayerDead(){
            OnPlayerDeadHandler?.Invoke();
        }
    }

}
