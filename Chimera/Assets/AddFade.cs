using UnityEngine;

public class AddFade : StateMachineBehaviour
{
    public float fadeDuration = 1.0f;
    public Color startColor = Color.white;
    public Color endColor = Color.yellow;
    private SpriteRenderer spriteRenderer;
    private float elapsedTime = 0f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get the SpriteRenderer component from the GameObject with the Animator
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = startColor;
        }
        elapsedTime = 0f;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (spriteRenderer != null)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.PingPong(elapsedTime / fadeDuration, 1f);
            if (elapsedTime > fadeDuration / 2)
            {
                t = Mathf.PingPong(fadeDuration - elapsedTime / fadeDuration, 1f);
            }
            spriteRenderer.color = Color.Lerp(startColor, endColor, t);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = startColor;
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
