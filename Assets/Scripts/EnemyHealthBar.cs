using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform fill;
    public Color fullColor = Color.green;
    public Color midColor = Color.yellow;
    public Color lowColor = Color.red;
    public Vector3 worldOffset = new Vector3(0, 2f, 0);
    
    private Camera targetCamera;
    private Vector3 fillBaseScale;
    private Renderer fillRenderer;
    private Transform parentEnemy;

    void Awake()
    {
        if (fill != null)
        {
            fillBaseScale = fill.localScale;
            fillRenderer = fill.GetComponent<Renderer>();
        }
        
        parentEnemy = transform.parent;
        
        if (parentEnemy != null)
        {
            transform.SetParent(null);
        }
    }

    void Start()
    {
        targetCamera = Camera.main;
    }

    void LateUpdate()
    {
        if (parentEnemy == null)
        {
            Destroy(gameObject);
            return;
        }
        
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
            if (targetCamera == null) return;
        }
        
        transform.position = parentEnemy.position + worldOffset;
        transform.rotation = targetCamera.transform.rotation;
    }

    public void SetFill(float ratio)
    {
        if (fill == null) return;
        ratio = Mathf.Clamp01(ratio);
        Vector3 s = fillBaseScale;
        s.x = fillBaseScale.x * ratio;
        fill.localScale = s;
        if (fillRenderer != null)
        {
            Color target = ratio > 0.5f
                ? Color.Lerp(midColor, fullColor, (ratio - 0.5f) * 2f)
                : Color.Lerp(lowColor, midColor, ratio * 2f);
            fillRenderer.material.color = target;
        }
    }
}