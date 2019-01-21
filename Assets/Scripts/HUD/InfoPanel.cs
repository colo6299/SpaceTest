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

    public void Start()
    {
        border = transform.GetChild(0).GetComponent<Image>();

        title = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        slot = transform.GetChild(1).GetChild(1).GetComponent<Text>();

        itemClass = transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<Text>();
        damageType = transform.GetChild(2).GetChild(1).GetChild(1).GetComponent<Text>();
        dps = transform.GetChild(2).GetChild(2).GetChild(1).GetComponent<Text>();

        critChance = transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<Text>();
        critDamage = transform.GetChild(3).GetChild(1).GetChild(1).GetComponent<Text>();

        stat1Title = transform.GetChild(4).GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        stat1Value = transform.GetChild(4).GetChild(0).GetChild(0).GetChild(1).GetComponent<Text>();

        stat2Title = transform.GetChild(4).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
        stat2Value = transform.GetChild(4).GetChild(1).GetChild(0).GetChild(1).GetComponent<Text>();

        stat3Title = transform.GetChild(4).GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        stat3Value = transform.GetChild(4).GetChild(0).GetChild(1).GetChild(1).GetComponent<Text>();

        stat4Title = transform.GetChild(4).GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>();
        stat4Value = transform.GetChild(4).GetChild(1).GetChild(1).GetChild(1).GetComponent<Text>();

        stat5Title = transform.GetChild(4).GetChild(0).GetChild(2).GetChild(0).GetComponent<Text>();
        stat5Value = transform.GetChild(4).GetChild(0).GetChild(2).GetChild(1).GetComponent<Text>();

        stat6Title = transform.GetChild(4).GetChild(1).GetChild(2).GetChild(0).GetComponent<Text>();
        stat6Value = transform.GetChild(4).GetChild(1).GetChild(2).GetChild(1).GetComponent<Text>();
    }

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
