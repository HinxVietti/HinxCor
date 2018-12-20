using UnityEngine;
using UnityEngine.Events;

namespace HinxCor.Unity.UI
{

    [RequireComponent(typeof(UserButton))]
    public class ButtonStatusGroup : MonoBehaviour
    {
        [Header("Enable on Not Disable")]
        [SerializeField] private GameObject[] Normal;
        [Header("Enable on Disable")]
        [SerializeField] private GameObject[] Disable;
        [SerializeField] private UnityButtonStateChangeEvent OnInteractChanged;
        [SerializeField] private UnityButtonStateChangeEvent OnStateChanged;
        [SerializeField] private ButtonStates btnStates;
        private UserButton _btn;
        private bool interact = false;

        private void Start()
        {
            _btn = gameObject.RawComponent<UserButton>();
        }

        private void Update()
        {
            if (_btn.buttonState != btnStates)
            {
                btnStates = _btn.buttonState;
                OnStateChanged.TryInvoke(btnStates);
            }
            if (_btn.interactable && !interact)
            {
                interact = true;
                OnInteractChange();
            }
            else if (!_btn.interactable && interact)
            {
                interact = false;
                OnInteractChange();
            }
        }

        private void OnInteractChange()
        {
            for (int i = 0; i < Normal.Length; i++)
            {
                Normal[i].SetActive(interact);
            }
            for (int i = 0; i < Disable.Length; i++)
            {
                Disable[i].SetActive(!interact);
            }
            OnInteractChanged.TryInvoke(interact ? ButtonStates.Normal : ButtonStates.Disabled);
        }

        [System.Serializable]
        private class UnityButtonStateChangeEvent : UnityEvent<ButtonStates>
        {

        }

    }
}
