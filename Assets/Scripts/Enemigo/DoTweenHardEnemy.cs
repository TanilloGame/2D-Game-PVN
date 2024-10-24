using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DoTweenHardEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform rightXPosition;
    [SerializeField]
    private Transform leftXPosition;

    [SerializeField]
    private Ease moveCurve;

    private Sequence moveEnemy;


    // Start is called before the first frame update
    void Start()
    {
        moveEnemy = DOTween.Sequence();
       

        moveEnemy.Append(transform.DOMoveX(rightXPosition.position.x, 3f).SetEase(moveCurve));
        moveEnemy.Append(transform.DOMoveX(leftXPosition.position.x, 3f).SetEase(moveCurve));

        moveEnemy.SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void movementStop() 
    
    {
        moveEnemy.Kill();
    
    
    
    }
}
