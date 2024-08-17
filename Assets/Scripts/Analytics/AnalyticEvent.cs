namespace Analytic
{
    using System.Collections;
    using System.Collections.Generic;
    using Core.AnalyticServices.Data;
    using UnityEngine;

    public abstract class BaseEvent : IEvent
    {
        public string StepId;
        public int    LevelCompleted;
        public float  SessionCount;
        public double UserPlayTime;
        public int    CoinCollected;
        public int    AdCount;
    }
}