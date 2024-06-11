﻿using System.Collections;
using System;

namespace _Project.Scripts.Reusable
{

    public interface IState
    {
        // automatically gets called in the State machine. Allows you to delay flow if desired
        void Enter();
      public  virtual void AfterEnter() { }
        // allows simulation of Update() method without a MonoBehaviour attached
        void Tick();
        // allows simulatin of FixedUpdate() method without a MonoBehaviour attached
        void FixedTick();
        // automatically gets called in the State machine. Allows you to delay flow if desired
        void Exit();
    }

}