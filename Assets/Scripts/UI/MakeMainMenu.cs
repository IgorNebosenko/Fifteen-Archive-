using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Additional;

namespace Game.UI
{
    /// <summary>
    /// Class which makes main menu buttons from the selected prefab
    /// </summary>
    public class MakeMainMenu : MonoBehaviour
    {
        /// <summary>
        /// Prefab of instance button
        /// </summary>
        public GameObject buttonPrefab;

        /// <summary>
        /// Constant, which defines count of buttons
        /// </summary>
        const int COUNT_BTN = 7;
        /// <summary>
        /// All keywords for buttons
        /// </summary>
        static readonly string[] valueTextButton =
        { 
            "newGame", "continueGame", "settings",
            "stat", "creator", "supportProject", "exit"
        };

        /// <summary>
        /// Method which calls with starting execute of this class
        /// </summary>
        void Start()
        {
            for (int i = 0; i < COUNT_BTN; ++i)
            {
                buttonPrefab.GetComponentInChildren<MultiLang>().idText =
                    valueTextButton[i];
                //Don't foget to add events OnClick()
                
                Instantiate(buttonPrefab, this.transform);
            }
            Debug.LogWarning("MakeMainMenu is not complete! Add event OnClick() for buttons!");
        }
    }
}