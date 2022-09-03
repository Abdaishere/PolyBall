using System.Collections;
using UnityEngine;
// ReSharper disable All

namespace UI
{
    public class MenuManager : MonoBehaviour
    {
        public static bool GameIsPaused;

        public GameObject pauseMenuUI;
        private void Start()
        {
            pauseMenuUI.SetActive(false);
        }

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.Escape)) return;
            PauseMenu();
        }

        public void PauseMenu()
        {
            if (GameIsPaused)
                Resume();
            else
                Pause();
        }

        public void Resume()
        {
            StartCoroutine(ResumeRoutine());
        }
        private IEnumerator ResumeRoutine()
        {
            pauseMenuUI.SetActive(false);
            
            Time.timeScale = 0.2f;
            yield return new WaitForSeconds(0.06f);
            Time.timeScale = 1;
            
            GameIsPaused = false;
        }

        public void Pause()
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0f;
            GameIsPaused = true;
        }

        public void QuitGame()
        {
            Debug.Log("Quiting");
            Application.Quit();
        }
    }
}
