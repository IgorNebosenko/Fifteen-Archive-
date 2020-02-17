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

        static PropContainer container;
        static DataContractJsonSerializer serializer;

        public static string PathToProps
        {
            get
            {
                return Path.Combine(Application.persistentDataPath, "properties.json");
            }
        }
    
        /// <summary>
        /// CTOR
        /// </summary>
        static Properties()
        {
                serializer = new DataContractJsonSerializer(typeof(PropContainer));
                TestExiting();
        }

            /// <summary>
            /// Method of testing is file props.json exists
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



        static void CreatePropFile()
        {
            container.gameVersion = PlayerSettings.bundleVersion;

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
    }
}