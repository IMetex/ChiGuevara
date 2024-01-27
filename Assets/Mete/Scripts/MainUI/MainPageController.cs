using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Mete.Scripts.MainUI
{
    public class MainPageController : MonoBehaviour
    {
        [Header("Button References")]
        [SerializeField] private Button _plaBtn;
        [SerializeField] private Button _creditsBtn;
        [SerializeField] private Button _controllerBtn;
        [SerializeField] private Button _exitBtn;
        [SerializeField] private Button[] _backBtn;

        [SerializeField] private GameObject _mainPage;
        [SerializeField] private GameObject _creditsPage;
        [SerializeField] private GameObject _controllerPage;
        
        void Start()
        {
            InitPage();
        }

        private void OnEnable()
        {
            _plaBtn.onClick.AddListener(PlayButtonPressed);
            _creditsBtn.onClick.AddListener(CreditsButtonPressed);
            _controllerBtn.onClick.AddListener(ControllerButtonPressed);
            _exitBtn.onClick.AddListener(ExitButtonPressed);

            foreach (var btn  in _backBtn)
            {
                btn.onClick.AddListener(BackButtonPressed);
            }
        }

        private void ExitButtonPressed()
        {
           Application.Quit();
        }

        private void OnDisable()
        {
            _plaBtn.onClick.RemoveListener(PlayButtonPressed);
            _creditsBtn.onClick.RemoveListener(CreditsButtonPressed);
            _controllerBtn.onClick.RemoveListener(ControllerButtonPressed);
            _exitBtn.onClick.RemoveListener(ExitButtonPressed);
            
            foreach (var btn  in _backBtn)
            {
                btn.onClick.RemoveListener(BackButtonPressed);
            }
        
        }

        private void BackButtonPressed()
        {
            InitPage();
        }

        private void ControllerButtonPressed()
        {
            _mainPage.SetActive(false);
            _controllerPage.SetActive(true);
        }

        private void CreditsButtonPressed()
        {
           _mainPage.SetActive(false);
           _creditsPage.SetActive(true);
        }

        private void PlayButtonPressed()
        {
            SceneManager.LoadScene(1);
        }

        private void InitPage()
        {
            _mainPage.SetActive(true);
            _creditsPage.SetActive(false);
            _controllerPage.SetActive(false);
        }
    }
}
