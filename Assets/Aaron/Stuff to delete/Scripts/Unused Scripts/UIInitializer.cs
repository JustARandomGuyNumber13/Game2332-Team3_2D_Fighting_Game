using UnityEngine;

public class UIInitializer : MonoBehaviour
{
    public GameObject sliderPrefab; // Prefab containing your sliders and toggles
    public Transform parentTransform; // Parent object to hold the instantiated prefab

    void Start()
    {
        if (sliderPrefab == null || parentTransform == null)
        {
            Debug.LogError("Slider prefab or parent transform not assigned!");
            return;
        }

        // Check if the necessary UI elements are already present
        if (parentTransform.Find("BGM_Slider") == null || parentTransform.Find("SFX_Slider") == null || parentTransform.Find("BGM_Toggle") == null || parentTransform.Find("SFX_Toggle") == null)
        {
            // Instantiate the prefab without a parent
            GameObject instantiatedUI = Instantiate(sliderPrefab);
            Debug.Log("Prefab instantiated");

            // Check if the instantiatedUI is valid
            if (instantiatedUI == null)
            {
                Debug.LogError("Failed to instantiate prefab!");
                return;
            }

            // Set the parent after instantiation
            instantiatedUI.transform.SetParent(parentTransform, false);

            // Assign the UI components to the Audio_Manager
            if (Audio_Manager.Instance != null)
            {
                Audio_Manager.Instance.AssignUIComponents(instantiatedUI);
                Debug.Log("AssignUIComponents called from UIInitializer");
            }
            else
            {
                Debug.LogError("Audio_Manager instance is null!");
            }
        }
        else
        {
            Debug.Log("UI elements already present. No need to instantiate new ones.");
        }
    }
}
