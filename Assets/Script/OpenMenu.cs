using System.Collections;
using UnityEngine;

public class OpenMenu : MonoBehaviour
{
    public Animator animations;  // Animator reference
    public GameObject menuObject;  // GameObject to disable

    public void Tutup()
    {
        // Play the animation
        animations.Play("MenuBack");
        
        // Start coroutine to wait for animation to finish
        StartCoroutine(WaitForAnimationToEnd());
    }

    private IEnumerator WaitForAnimationToEnd()
    {
        // Get the animation length
        float animationTime = animations.GetCurrentAnimatorStateInfo(0).length;
        
        // Wait until animation is done
        yield return new WaitForSeconds(animationTime);
        
        // Disable the GameObject
        if (menuObject != null)
        {
            menuObject.SetActive(false);
        }
    }
}
