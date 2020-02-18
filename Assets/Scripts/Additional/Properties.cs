using System;
using System.IO;
using System.Runtime.Serialization.Json;
using UnityEngine;
using UnityEditor;

namespace Game.Additional
{
    /// <summary>
    /// Class of properties game
    /// </summary>
    public static class Properties
    {
        /// <summary>
        /// Container of properties
        /// </summary>
        [Serializable]
        class PropContainer
        {
            /// <summary>
            /// Game version of this save
            /// </summary>
            public string gameVersion;
            /// <summary>
            /// Current language of game
            /// </summary>
            public SystemLanguage currLang;
        }

        /// <summary>
        /// Instance of container
        /// </summary>
        static PropContainer container;
        /// <summary>
        /// Instance of serializer
        /// </summary>
        static DataContractJsonSerializer serializer;

        /// <summary>
        /// String with path file of props
        /// </summary>
        static string PathToProps
        {
            get
            {
                return Path.Combine(Application.persistentDataPath, "properties.json");
            }
        }
        /// <summary>
        /// Property, which provides get and set current language of game
        /// </summary>
        public static SystemLanguage CurrLang
        {
            get
            {
                return container.currLang;
            }
            set
            {
                container.currLang = value;
            }
        }

        /// <summary>
        /// CTOR
        /// </summary>
        static Properties()
        {
            serializer = new DataContractJsonSerializer(typeof(PropContainer));
            container = new PropContainer();
            TestExiting();
        }

        /// <summary>
        /// Method of testing is file properties.json exists
        /// </summary>
        static void TestExiting()
        {
            if (File.Exists(PathToProps))
            {
                using (FileStream fs = new FileStream(PathToProps, FileMode.Open, FileAccess.Read))
                {
                    container = serializer.ReadObject(fs) as PropContainer;
                }
            }
            else
                CreatePropFile();
        }
        /// <summary>
        /// Method of creating default prop file. 
        /// If fields in container added - ad them in this method
        /// </summary>
        static void CreatePropFile()
        {
            container.gameVersion = Application.version;

            if (Application.systemLanguage == SystemLanguage.Russian ||
                Application.systemLanguage == SystemLanguage.Ukrainian)
            {
                container.currLang = SystemLanguage.Russian;
            }
            else
                container.currLang = SystemLanguage.English;
            
            WriteProps();
        }

        /// <summary>
        /// Method of writing properties to file
        /// </summary>
        public static void WriteProps()
        {
            using (FileStream fs = new FileStream(PathToProps, FileMode.Create, FileAccess.Write))
            {
                serializer.WriteObject(fs, container);
            }
        }
        /// <summary>
        /// Method, which refreshes properties from file in real time
        /// </summary>
        public static void RefreshProps()
        {
            using (FileStream fs = new FileStream(PathToProps, FileMode.Open, FileAccess.Read))
            {
                container = serializer.ReadObject(fs) as PropContainer;
            }
        }
    }
}