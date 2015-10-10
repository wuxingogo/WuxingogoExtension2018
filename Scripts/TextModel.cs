using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if TMP
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
#endif
public class TextModel : MonoBehaviour
{
#if TMP
	public TextMeshProUGUI modelText = null;
#elif !TMP
    public Text modelText = null;
#endif
    public string xmlCmd = "";
	public void OnEnable(){
		if( null != modelText && xmlCmd != "" ){
			modelText.text = xmlCmd;
		}
	}
}

[System.Serializable]
public class TextModelItem{
 #if TMP
	public TextMeshProUGUI modelText = null;
#elif !TMP
    public Text modelText = null;
 #endif
	public string xmlCmd = "";

	public void GetTextFromXml(){
#if TINYTIME
		modelText.text = Address.AddressData.GetText(xmlCmd);
#endif
	}
}

