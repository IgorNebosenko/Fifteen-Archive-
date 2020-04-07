using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Interfaces;
using System.Threading.Tasks;
using Game.IO;

namespace Game.UI
{
    /// <summary>
    /// Manager of global objects. Wait while all objects loads? and then - show UI
    /// </summary>
    public class GlobalObjectManager : MonoBehaviour
    {
        List<IGlobalObject> globalObjects;
        bool isInitilized = false;

        /// <summary>
        /// GameObject of UI main window
        /// </summary>
        public GameObject MainWindow;
        /// <summary>
        /// GameObject of UI settings
        /// </summary>
        public GameObject Settings;
        /// <summary>
        /// GameObject of UI first start screen
        /// </summary>
        public GameObject FirstStartScreen;


        void Start()
        {
            globalObjects = new List<IGlobalObject>();
            globalObjects.AddRange(gameObject.GetComponents<IGlobalObject>());
        }

        async void Update()
        {
            if (isInitilized)
            {
                await Task.Delay(100);
                return;
            }
            foreach (IGlobalObject i in globalObjects)
            {
                if (!i.IsLoaded)
                {
                    isInitilized = false;
                    break;
                }
                else
                    isInitilized = true;
            }
            if (isInitilized)
            {
                Initilize();
            }
            await Task.Delay(10);

        }

        void Initilize()
        {
            SettingsCore sc = gameObject.GetComponent<SettingsCore>();
            if (sc.ShowMessageAboutCust)
            {
                Settings.SetActive(true);
                FirstStartScreen.SetActive(true);
            }
            else
                MainWindow.SetActive(true);
        }
    }
}