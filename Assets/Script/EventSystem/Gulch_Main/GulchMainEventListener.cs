using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gulch
{
    public class GulchMainEventListener
    {
        private static GulchMainEventListener instance = new GulchMainEventListener();
        public static GulchMainEventListener Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new GulchMainEventListener();
                }
                return instance;
            }
        }

        public GulchMainEventListener()
        {
            InitGulchMainEvent();
        }

        private void InitGulchMainEvent()
        {
            slowMutantSlain_trainingGround = 0;
        }

        // event slay two slow mutants at the west side of the Gulch
        public event Action Slay_SlowMutant_TrainingGround;
        private int slowMutantSlain_trainingGround;
        public void OnSlay_SlowMutant_TrainingGround()
        {
            slowMutantSlain_trainingGround++;
            if(slowMutantSlain_trainingGround >= 2)
            {
                Slay_SlowMutant_TrainingGround?.Invoke();
            }
        }
    }
}
