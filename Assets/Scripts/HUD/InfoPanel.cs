using UnityEngine;
using UnityEngine.UI;

public class InfoPanel : MonoBehaviour
{
    public Image border;

    public Text title;
    public Text slot;

    public Text itemClass;
    public Text damageType;

    public Text dps;
    public Text critChance;
    public Text critDamage;

    public Text stat1Title;
    public Text stat1Value;

    public Text stat2Title;
    public Text stat2Value;

    public Text stat3Title;
    public Text stat3Value;

    public Text stat4Title;
    public Text stat4Value;

    public Text stat5Title;
    public Text stat5Value;

    public Text stat6Title;
    public Text stat6Value;

    public void ResetFields()
    {
        border.color = Color.black;

        title.color = Color.white;
        slot.color = Color.white;

        dps.color = Color.white;
        critChance.color = Color.white;
        critDamage.color = Color.white;

        stat1Value.color = Color.white;
        stat2Value.color = Color.white;
        stat3Value.color = Color.white;
        stat4Value.color = Color.white;
        stat5Value.color = Color.white;
        stat6Value.color = Color.white;

        stat1Title.transform.parent.gameObject.SetActive(false);
        stat2Title.transform.parent.gameObject.SetActive(false);
        stat3Title.transform.parent.gameObject.SetActive(false);
        stat4Title.transform.parent.gameObject.SetActive(false);
        stat5Title.transform.parent.gameObject.SetActive(false);
        stat6Title.transform.parent.gameObject.SetActive(false);
    }

}
