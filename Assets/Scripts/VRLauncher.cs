using UnityEngine;
using Photon.Pun;
using Photon.Realtime; //added to use MonoBehaviourPunCallbacks
using UnityEngine.UI;

namespace Com.MyCompany.MyGame
{
    public class VRLauncher : MonoBehaviourPunCallbacks
    {
        #region Public Fields

        #endregion
        #region Private Serializable Fields
        [Tooltip("The max number of players per room. When a room is full, it can't be joined by new players, and so a new room will be created.")]
        [SerializeField]
        private byte maxPlayersPerRoom = 4; //initial value that can be changed in the Unity inspector

        //these are used for control panel and progresslabel status
        [Tooltip("The UI panel to let the user enter the name, connect and play.")]
        [SerializeField]
        private GameObject controlPanel;

        [Tooltip("The UI label to inform the user that the connection is in progress")]
        [SerializeField]
        private GameObject progressLabel;

        //added to toggle to VR version
        [SerializeField]
        private bool IsVRVersion;

        [SerializeField]
        private GameObject PhotonGameManager;
        #endregion

        #region Private Fields

        // Client's version number -- we separate users from each other bty gameVersion based on "gamebreaking" version features
        string gameVersion = "1"; //re: see Semantic Versioning for versioning choice and connotations
        bool isConnecting;
        #endregion

        #region MonoBehaviourPunCallbacks Callbacks
        //we are going to override the OnConnectedToMaster() and OnDisconnected() PUN callbacks here
        public override void OnConnectedToMaster()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnConnectedToMaster() was called by PUN");
            if (isConnecting) //we only progress to joining a random room when we have intention to connect
            {
                PhotonNetwork.JoinRandomRoom(); //we then implement OnJoinRandomRoomFailed() PUN callback if no room exists so we can create one using PhotonNetwork.CreateRoom()
            }
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            // logic for controlpanel and progress message for disconnecting
            //progressLabel.SetActive(false);
            //controlPanel.SetActive(true);
            Debug.LogWarningFormat("PUN Basics Tutorial/Launcher: OnDisconnected() was called by PUN with reason {0}", cause); //super helpful since we are exposing the given error message!
        }

        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinRandomFailed() was called by PUN. No random room available, so a new room will be created.\nCalling: PhotonNetwork.CreateRoom");
            PhotonNetwork.CreateRoom(null, new RoomOptions{ MaxPlayers = maxPlayersPerRoom }); //? might be accepting json format property vals

        }

        public override void OnJoinedRoom()
        {
            Debug.Log("PUN Basics Tutorial/Launcher: OnJoinedRoom() called by PUN. Now this client is in a room.");

            if (PhotonNetwork.CurrentRoom.PlayerCount <= maxPlayersPerRoom)
            {
                Debug.Log("We load the 'Room has space' ");

                // load the room level
                PhotonGameManager.GetComponent<VRGameManagerScript>().enabled = true;
                
                /*
                if (IsVRVersion)
                {
                    PhotonNetwork.LoadLevel("VR Room for 1");
                }
                else
                {
                    PhotonNetwork.LoadLevel("Room for 1");
                }
                */

            }
            else
            {
                Debug.Log("PUN Basics Tutorial/Launcher: OnJoinRandomFailed() was called by PUN. No space in exisiting room available, so a new room will be created.\nCalling: PhotonNetwork.CreateRoom");
                PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
            }
        }
        #endregion

        #region MonoBehaviour Callbacks




        void Awake()
        {
            // Critical - makes sure we can use the PhotoenNetwork.LoadLevel() on the master client and that all clients in the same room sync their level automatically
            PhotonNetwork.AutomaticallySyncScene = true;
        }



        // Start is called before the first frame update
        void Start()
        {
            Connect(); // we dont need this if we are using a button to start this

            // logic for controlpanel and progress message
            //progressLabel.SetActive(false);
            //controlPanel.SetActive(true);
        }
        #endregion



        #region Public methods

        public void Connect()
        {
            isConnecting = true; //intention to connect, will be false when exiting
            // logic for controlpanel and progress message when attempting connection
            //progressLabel.SetActive(true);
            //controlPanel.SetActive(false); //hides control panel

            if (PhotonNetwork.IsConnected) //check if connected or not, join random room if we are, else we attempt connect to the Photon Online Server
            {
                PhotonNetwork.JoinRandomRoom(); //if this fails Photon will call OnJoinRandomFailed() RPC, if we implement this we can choose how to handle the response
            }
            else
            {
                // have to connect to Photon Online Server first
                PhotonNetwork.GameVersion = gameVersion;
                PhotonNetwork.ConnectUsingSettings(); // this method is the starting point to connect to Photon Cloud
            }
        }

        #endregion
    }
}

