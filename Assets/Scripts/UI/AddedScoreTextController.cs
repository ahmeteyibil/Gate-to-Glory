using TMPro;
using UnityEngine;

public class AddedScoreTextController : MonoBehaviour
{
    TextMeshProUGUI tmpComponent;
    Animator m_Animator;
    Vector2 startPos;
    CanvasGroup cg;
    [SerializeField] float textLiveTime = 0.5f;
    private void Awake()
    {
        startPos = transform.position;
        cg = GetComponent<CanvasGroup>();
        m_Animator = GetComponent<Animator>();
        tmpComponent = GetComponent<TextMeshProUGUI>();
    }
    public void UpdateAddedScoreText(int addedScore)
    {
        ResetAnimation();
        string addedScoreStr;
        if (addedScore >= 0)
        {
            addedScoreStr = "+" + addedScore.ToString();
            tmpComponent.color = Color.green;
        }
        else
        {
            addedScoreStr = addedScore.ToString();
            tmpComponent.color = Color.red;
        }
        tmpComponent.text = addedScoreStr;
        Invoke(nameof(Disappear), textLiveTime);
    }
    void ResetAnimation()
    {
        //print("Reset trigged");
        m_Animator.SetTrigger(AnimatorVariables.AST_RESET);
        this.transform.position = startPos;
        cg.alpha = 1f;
    }
    void Disappear()
    {
        m_Animator.SetTrigger(AnimatorVariables.AST_DISAPPEAR);
        //print("Disappear trigged");
    }
}
