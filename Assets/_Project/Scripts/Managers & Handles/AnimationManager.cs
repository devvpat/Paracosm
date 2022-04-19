using UnityEngine;

namespace ACoolTeam
{
    public static class AnimationManager
    {
        public static string CurrentState;

        public static void ChangeAnimState(Animator animator, AnimationClip newState)
        {
            if (CurrentState == newState.name) return;

            animator.Play(newState.name);
            CurrentState = newState.name;
        }
    }
}
