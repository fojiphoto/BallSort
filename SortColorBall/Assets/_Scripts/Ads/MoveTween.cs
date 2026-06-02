using UnityEngine;
using DG.Tweening;

public class MoveTween : MonoBehaviour
{
    public bool shouldResetStartPos, reverse, ignoreTimeScale = true;
    public float time, waitForNextLoop;
    public Vector3 StartPos, EndPos;
    public Ease EaseType = Ease.OutBounce; // Default equivalent ease type
    public Ease EaseTypeReverse = Ease.OutBounce; // Default equivalent ease type
    public LoopType loopType = LoopType.Restart;
    public int LoopValues=0;

    public virtual void OnEnable()
    {
        // Reset the tween on enable to avoid conflicts if this script is enabled/disabled multiple times
        transform.DOKill();

        if (reverse)
        {
            transform.localPosition = EndPos;
            transform.DOLocalMove(StartPos, time)
                .SetEase(EaseTypeReverse)
                .SetDelay(waitForNextLoop)
                .SetLoops(-1, loopType)
                .SetUpdate(ignoreTimeScale);
        }
        else
        {
            if (shouldResetStartPos)
                transform.localPosition = StartPos;

            transform.DOLocalMove(EndPos, time)
                .SetEase(EaseType)
                .SetDelay(waitForNextLoop)
                .SetLoops(LoopValues, loopType)
                .SetUpdate(ignoreTimeScale);
        }
    }
}
