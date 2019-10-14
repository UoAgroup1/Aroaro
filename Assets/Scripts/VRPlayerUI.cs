using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Com.MyCompany.MyGame
{
    public class VRPlayerUI : MonoBehaviour
    {

        #region Private Fields
        [Tooltip("UI Text to display Player's name")]
        [SerializeField]
        private Text playerNameText;

        [Tooltip("UI Slider to display Player's Health")]
        [SerializeField]
        private Slider playerHealthSlider;

        // a reference to the PlayerManager of the player (the target) is needed for binding
        private VRPlayerManager target;

        // needed to follow the player
        float characterControllerHeight = 0f;
        Transform targetTransform;
        Renderer targetRenderer;
        CanvasGroup _canvasGroup;
        Vector3 targetPosition;
        #endregion

        #region Public Fields
        // needed to follow the player
        [Tooltip("Pixel offset from the player target")]
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f, 0f, 0f);
        #endregion


        #region MonoBehaviour Callbacks
        private void Awake()
        {
            //parenting to the UI canvas
            this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false); // I would do this a different way personally
        }
        void Update()
        {
            //needed to follow player
            _canvasGroup = this.GetComponent<CanvasGroup>();

            // reflect the Player Health on Slider to reflect Health value on player

            /*
              if (playerHealthSlider != null)
            {
                playerHealthSlider.value = target.Health;
            }
            */

            // Destroy itself if the target is null, is a fail safe for when Photon is destroying instances of a Player over the network
            // needed so we dont have many orphan (leftover) UI prefab instances all over the scene
            if (target == null)
            {
                Destroy(this.gameObject);
                return;
            }
        }

        // added for the Ui prefab
        void LateUpdate()
        {
            // Do not show the UI if we are not visible to the camera, thus avoid potential bugs with seeing the UI, but not the player itself.
            if (targetRenderer != null)
            {
                this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
            }


            // #Critical
            // Follow the Target GameObject on screen.
            if (targetTransform != null)
            {
                targetPosition = targetTransform.position;
                targetPosition.y += characterControllerHeight;
                this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
            }
        }
        #endregion

        #region Public Methods
        // needed for the UI to player binding
        public void SetTarget(VRPlayerManager _target)
        {
            if (_target == null)
            {
                Debug.LogError("Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
                return;
            }
            // Cache references for efficiency
            target = _target;
            // sets position and follows player
            targetTransform = this.target.GetComponent<Transform>();
            targetRenderer = this.target.GetComponent<Renderer>();
            CharacterController characterController = _target.GetComponent<CharacterController>();
            // Get data from the Player that won't change during the lifetime of this Component
            if (characterController != null)
            {
                characterControllerHeight = characterController.height;
            }

            // 
            if (playerNameText != null)
            {
                playerNameText.text = target.photonView.Owner.NickName;
            }
        }
        #endregion
    }
}

