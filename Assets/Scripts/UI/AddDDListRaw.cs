using UnityEngine;
using Game.Additional;
using TMPro;

namespace Game.UI
{
    /// <summary>
    /// Class which adds to dropdown items. All text values are raw
    /// </summary>
    public class AddDDListRaw : MonoBehaviour
    {
        /// <summary>
        /// List of text strings
        /// </summary>
        public string[] ListText;

        void Start()
        {
            TMP_Dropdown dd = gameObject.GetComponent<TMP_Dropdown>();

            for (int i = 0; i < ListText.Length; ++i)
            {
                dd.options.Add(new TMP_Dropdown.OptionData(
                    MultiLang.core.GetText(ListText[i]))) ;
            }
        }
    }
}