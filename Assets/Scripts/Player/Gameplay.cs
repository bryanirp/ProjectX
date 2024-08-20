using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public enum GameState
    {
        loading,
        inGame,
        pause
    }
    public class Gameplay : MonoBehaviour
    {
        public static GameState gameState = GameState.inGame;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Game: GameState =" + gameState.GetType().ToString());
            }
        }
    }
}