namespace Com.MyCompany.MyGame
{
    using Photon.Pun;
    using UnityEngine;
    
    public class NetworkControllableObject : MonoBehaviour, IPunObservable
    {
        private PhotonView photonView;
        public bool isHeld = false;
        private int originalOwnerNumber;

        public void DestroyObject()
        {
            if (photonView != null)
            {
                photonView.RPC("DestroyNetworkedObject", RpcTarget.All);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        [PunRPC]
        private void DestroyNetworkedObject()
        {
            if (photonView.IsMine) PhotonNetwork.Destroy(gameObject);
        }

        public void AttemptInteraction()
        {

            Debug.Log("isHeld = " + isHeld);
            if (photonView == null) return;
            if (isHeld == true) return;
            photonView.RequestOwnership();
        }

        public void AbortInteraction()
        {
            if (photonView == null) return;
            if (isHeld == true && photonView.OwnerActorNr == PhotonNetwork.LocalPlayer.ActorNumber)
            {
                if (photonView.OwnerActorNr == 0 ){
                    photonView.TransferOwnership(0);
                }else{
                    photonView.TransferOwnership(originalOwnerNumber); //transfer ownership back to original owner if not scene ObjectS
                }
            }
            
            
                
        }

        
        private void Awake()
        {
            photonView = gameObject.GetComponent<PhotonView>();
        }

        // Start is called before the first frame update
        void Start()
        {
            originalOwnerNumber = photonView.OwnerActorNr;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(isHeld);
            }
            else
            {
                this.isHeld = (bool)stream.ReceiveNext();
            }
        }
    }
}
