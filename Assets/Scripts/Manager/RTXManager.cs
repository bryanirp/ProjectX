using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
namespace Game
{
    public enum RenderingType
    {
        DR,
        RTX
    }
    [RequireComponent(typeof(LightManager))]
    public class RTXManager : MonoBehaviour
    {
        public RenderingType renderingType;
        //public static RenderingType renderingType;
        public GameObject[] RTX;
        public GameObject[] DR;

        Light Sun;
        public static RTXManager instance;
        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            Sun = LightManager.instance._sun;
            Sun.GetComponent<HDAdditionalLightData>().useScreenSpaceShadows = true;
            SetRendering();
        }

        public RTXManager SetRenderingType(RenderingType renderingType)
        {
            instance.renderingType = renderingType;
            return instance;
        }
        public RTXManager UpdateRendering()
        {
            if (instance.renderingType == RenderingType.DR)
            {
                foreach (GameObject go in DR)
                {
                    go.SetActive(true);
                }
                foreach (GameObject go in RTX)
                {
                    go.SetActive(false);
                }
            }
            if (instance.renderingType == RenderingType.RTX)
            {
                foreach (GameObject go in DR)
                {
                    go.SetActive(false);
                }
                foreach (GameObject go in RTX)
                {
                    go.SetActive(true);
                }
            }
            return instance;
        }
        public RTXManager GetRenderingType(RenderingType renderingType)
        {
            renderingType = instance.renderingType;
            return instance;
        }
        void Update()
        {

        }
        void SetRendering()
        {
            if (renderingType == RenderingType.DR)
            {
                foreach (GameObject go in DR)
                {
                    go.SetActive(true);
                }
                foreach (GameObject go in RTX)
                {
                    go.SetActive(false);
                }
                Sun.GetComponent<HDAdditionalLightData>().useRayTracedShadows = false;
            }
            if (renderingType == RenderingType.RTX)
            {
                foreach (GameObject go in DR)
                {
                    go.SetActive(false);
                }
                foreach (GameObject go in RTX)
                {
                    go.SetActive(true);
                }
                Sun.GetComponent<HDAdditionalLightData>().useRayTracedShadows = true;
            }

        }
    }

}