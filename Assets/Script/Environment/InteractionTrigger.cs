using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        transform.parent.SendMessage("EnterInteractionArea");
    }

    private void OnTriggerExit2D(Collider2D other) {
        transform.parent.SendMessage("ExitInteractionArea");
    }
}
