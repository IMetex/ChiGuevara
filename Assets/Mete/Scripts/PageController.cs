using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mete.Scripts
{
    public class PageController : MonoBehaviour
    {
        public Button _mainBnt;
        public Button _restarBtn;

        public GameObject _deadPage;

        public  Health.Health _health;
        
        private void Update()
        {
            if (_health.isDead)
            {
                _deadPage.SetActive(true);
            }
        }

        private void OnEnable()
        {
           _mainBnt.onClick.AddListener(MainButtonPressed);
           _restarBtn.onClick.AddListener(RestartButtonPressed);
        }

        private void RestartButtonPressed()
        {
            SceneManager.LoadScene(1);
        }

        private void MainButtonPressed()
        {
            SceneManager.LoadScene(0);
        }
    }
}