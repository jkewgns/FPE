using UnityEngine;
using TMPro;
using System.IO;
using Unity.Mathematics;

public class StylePoints : MonoBehaviour
{
    [Header("Settings")]
    public bool IsCar = true;

    [Header("UI")]
    public TMP_Text DriftPointsUI;
    public TMP_Text MultiplierUI;

    private float points;
    private float totalPoints;
    private float pointsCooldown;
    private float multiplierEligibility;
    private float multiplier = 1;

    private string savePath;

    void Start()
    {
        savePath = Path.Combine(Application.persistentDataPath, "style_points.json");
        LoadPoints();
        DriftPointsUI.text = "Drift Points: " + Mathf.Floor(totalPoints).ToString();
    }

    void Update()
    {
        if (IsCar)
        {
            CalculateDriftPoints();
        }
    }

    void CalculateDriftPoints()
    {
        Vector3 forward = transform.forward;
        Vector3 right = transform.right * GetComponent<Rigidbody>().angularVelocity.magnitude;
        float angle = Vector3.SignedAngle(right, forward, Vector3.up);
        float gain = Mathf.Abs(angle * Time.deltaTime) * multiplier;
        totalPoints += gain;

        if (Mathf.Abs(angle * Time.deltaTime) * 50 > 0.1f)
        {
            points += gain;
            multiplierEligibility += (10 / (multiplier * 0.35f)) * Time.deltaTime;
            if (multiplierEligibility > 100)
            {
                multiplierEligibility = 0;
                multiplier += 1;
            }
            pointsCooldown = Time.time + 0.5f;
        }

        if (pointsCooldown < Time.time)
        {
            points = math.max(points - 1, 0);
            multiplier = 1;
        }

        DriftPointsUI.text = Mathf.Floor(points).ToString();
        MultiplierUI.text = "x" + Mathf.Floor(multiplier).ToString();

        SavePoints();
    }

    void SavePoints()
    {
        StylePointsData data = new StylePointsData();
        data.totalPoints = totalPoints;
        File.WriteAllText(savePath, JsonUtility.ToJson(data));
    }

    void LoadPoints()
    {
        if (!File.Exists(savePath)) return;
        StylePointsData data = JsonUtility.FromJson<StylePointsData>(File.ReadAllText(savePath));
        totalPoints = data.totalPoints;
    }

    void OnApplicationQuit()
    {
        SavePoints();
    }
}
