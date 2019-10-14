using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

namespace Com.MyCompany.MyGame
{
    /// <summary>
    ///  Player name input field. Let the user input his name, will appear ABOVE the player in the game.
    /// </summary>
    [RequireComponent(typeof(InputField))] //force necessity of InputField component, reduces risk of runtime and compile time errors
    public class VRPlayerNameInputField : MonoBehaviour
    {
        #region Private Constants

        // stores the PlayerPref Key to avoid typos ???
        const string playerNamePrefKey = "PlayerName";

        #endregion

        #region MonoBehaviour Callbacks
        /// <summary>
        ///  MonoBehaviour method called on GameObject by Unity, during the initialisation phase
        /// </summary>
        void Start()
        {
            string defaultName = string.Empty; //local field
            InputField _inputField = this.GetComponent<InputField>();
            if (_inputField != null)
            {
                if (PlayerPrefs.HasKey(playerNamePrefKey))
                {
                    defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                    _inputField.text = defaultName;
                }
            }

            PhotonNetwork.NickName = defaultName;
        }

        #endregion

        #region Public Methods
        public void SetPlayerName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                Debug.LogError("Player Name is null or empty");
                return; //end logic if nothing is in input
            }
            PhotonNetwork.NickName = value;

            PlayerPrefs.SetString(playerNamePrefKey, value);
        }
        #endregion
    }
}

