
namespace Com.MyCompany.MyGame
{
    using System.Collections;
    using System.Collections.Generic;
    using Photon.Pun;
    using Photon.Realtime;
    using UnityEngine;

    public class NetworkControllableOwnershipManager : MonoBehaviour, IPunOwnershipCallbacks
    {
        public void OnOwnershipRequest(PhotonView targetView, Player requestingPlayer)
        {
            NetworkControllableObject requestedObject = targetView.gameObject.GetComponent<NetworkControllableObject>();
            if (requestedObject == null) return;
            //Debug.Log(requestedObject.isHeld);
            if (requestedObject.isHeld == false)
            {
                //Debug.Log("Request approved");
                requestedObject.isHeld = true;
                Debug.Log("isHeld = True (Request approved)");
                targetView.TransferOwnership(requestingPlayer);
            }
          

        }

        public void OnOwnershipTransfered(PhotonView targetView, Player previousOwner)
        {
            NetworkControllableObject targetInteractableObject = targetView.gameObject.GetComponent<NetworkControllableObject>();
            if (targetInteractableObject == null) return;
            if(targetView.OwnerActorNr == 0)
            {
                targetInteractableObject.isHeld = false;
            }
           
        }

        // Start is called before the first frame update
        void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        // Update is called once per frame
        void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        
    }
}

