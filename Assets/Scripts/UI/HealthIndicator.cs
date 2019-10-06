using UnityEngine;
using UnityEngine.UI;

public class HealthIndicator : MonoBehaviour
{

    public RectTransform healthTransform;
    public Text healthText;
    public Entity trackingEntity;

    private float initialWidth;    

    // Start is called before the first frame update
    void Start()
    {
        RectTransform transform = GetComponent<RectTransform>();
        initialWidth = transform.rect.width;        
    }

    // Update is called once per frame
    void Update()
    {
        float width = trackingEntity.Health / trackingEntity.Stats.MaxHealth * initialWidth;
        healthTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        healthText.text = string.Format("{0}/{1}", trackingEntity.Health, trackingEntity.Stats.MaxHealth);
    }
}
