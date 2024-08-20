using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Game.GunSystem
{
    [CreateAssetMenu(menuName = "Game/Modification", fileName = "Mod", order = 3)]
    public class Modification : ScriptableObject
    {
        // Name of the modification
        public string m_name;
        // An easy description of the features of the modification
        public string m_description;
        // Array of every weapon that can use this modification
        public Waeapon[] waeapons;

    }
}