using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private enum LadderPart {
        Top, Bottom, Complete
    }

    [SerializeField] private LadderPart part = LadderPart.Complete;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            switch(part)
            {
                case LadderPart.Complete :
                    playerController.ChangeCanClimb(true);
                    playerController.SetLadder(this);
                    break;
                case LadderPart.Top :
                    playerController.ChangeTopLadder(true);
                    break;
                case LadderPart.Bottom :
                    playerController.ChangeBottomLadder(true);
                    break;
                default :
                    break;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            switch(part)
            {
                case LadderPart.Complete :
                    playerController.ChangeCanClimb(false);
                    break;
                case LadderPart.Top :
                    playerController.ChangeTopLadder(false);
                    break;
                case LadderPart.Bottom :
                    playerController.ChangeBottomLadder(false);
                    break;
                default :
                    break;
            }
        }
    }
}
