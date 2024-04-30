using ABKaspo.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABKaspo.Game
{
    public class LightManager : MonoBehaviour
    {
        public Light[] _sceneLights;
        public Light[] _cellingLights;
        public Light[] _decorativeLights;
        public Light _sun;
        public static LightManager instance;

        public static Light sun;

        private void Awake()
        {
            instance = this;
            //sun = RTXManager.sun
        }
        void Start()
        {

        }
        void Update()
        {

        }
        public LightManager ChangeDayCycle(bool usingDayCycle, bool isDay)
        {
            if (usingDayCycle == true)
            {
                isDay = false;


            }
            return instance;
        }
    }
}