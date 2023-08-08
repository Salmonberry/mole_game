using UnityEngine;

public class GameStartAnimationController : MonoBehaviour
{
    public void OnGameReady()
    {
        gameObject.SetActive(false);
        GameStartReadyEvent.Trigger();
    }
}