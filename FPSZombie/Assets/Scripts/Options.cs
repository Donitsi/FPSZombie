using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    public Toggle invertMouseToggle;
    public Slider volumeSlider;

    private static int TRUE = 1;
    private static int FALSE = 0;
    private static string INVERT_MOUSE_KEY = "invertMouse";
    private static string VOLUME_KEY = "volume";

    public void Start() {
        invertMouseToggle.isOn = GetInvertMouse();
        volumeSlider.value = GetVolume();
    }

    public void Save() {
        if(invertMouseToggle.isOn) PlayerPrefs.SetInt(INVERT_MOUSE_KEY, TRUE);
        else PlayerPrefs.SetInt(INVERT_MOUSE_KEY, FALSE);
        PlayerPrefs.SetFloat(VOLUME_KEY, volumeSlider.normalizedValue);

        //PlayerPrefs.Save();
    }

// Näitä metodeja kutsutaan scripteissä, joissa halutaan käyttää asetuksiin tallennettuja arvoja ->

    // Palauttaa volumen 0.0f -> 1.0f väliltä. Default = 1.0f
    public static float GetVolume() {
        return PlayerPrefs.GetFloat(Options.VOLUME_KEY, 1f);
    }

    // Palauttaa true jos invertMouse päällä, false jos pois päältä. Default = false
    public static bool GetInvertMouse() {
        int invertPref = PlayerPrefs.GetInt(INVERT_MOUSE_KEY, -1);

        if (invertPref == TRUE) return true;
        else if (invertPref == FALSE) return false;
        
        return false;
    }
}
