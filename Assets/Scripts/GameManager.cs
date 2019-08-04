using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Codes.Linus.IntVectors
{
    public class GameManager : MonoBehaviour
    {

        public Board mBoard;

        public PieceManager mPieceManager;
        // Use this for initialization
        void Start()
        {
            mBoard.Create();

            mPieceManager.Setup(mBoard);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);            
        }

        public void Menu()
        {
            GameObject menu = Instantiate(GameObject.Find("MenuButton"), transform);
            menu.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => {
                Debug.Log("Click");
            });
        }
    }
}