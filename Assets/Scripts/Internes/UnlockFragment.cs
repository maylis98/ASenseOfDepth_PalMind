using UnityEngine;
using UnityEngine.Playables;

public class UnlockFragment : MonoBehaviour
{
    public PlayableDirector timeline;

    private void OnTriggerEnter(Collider camera)
    {
        if (camera.gameObject.tag == "MainCamera")
        {
            timeline.Play();
            
        }
    }
}
