using System;
using System.IO;
using UnityEditor;
using UnityEngine;

/************************************************************************************
 * 
 *							            Shortcuts
 *							  
 *							      Sidebar Editor Window
 *			
 *			                        Script written by: 
 *			        Jonathan Carter (https://jonathan.carter.games)
 *			        
 *									Version: 1.0.1
 *						   Last Updated: 09/10/2020 (d/m/y)					
 * 
*************************************************************************************/

namespace CarterGames.Assets.Shortcuts
{
    public class ShortcutsEditorWindow : EditorWindow
    {
        // Editor Versions
        private string[] legacy = new string[2] { "2018", "2017" };

        // Colours used in this editor
        private Color32 pinkCol = new Color32(255, 143, 248, 255);
        private Color32 yelCol = new Color32(242, 255, 143, 255);
        private Color32 ornCol = new Color32(255, 205, 143, 255);
        private Color32 bluCol = new Color32(143, 206, 255, 255);
        private Color32 grnCol = new Color32(143, 255, 143, 255);
        private Color32 redCol = new Color32(255, 143, 143, 255);

        // CG - check for scripts, if they are in the project, they will show up, if not it won't cause any issues with the script and just won't show the secion.
        Type checkSM = Type.GetType("CarterGames.Assets.SaveManager.SaveDataEditor");
        Type checkAMP = Type.GetType("CarterGames.Assets.AudioManagerPlus.AudioManagerPlusEditor");


        public bool useNamespace;
        public bool shouldClearFields;
        public string fileName;
        public string fileNamespace;

        // Tools for the delecting of panels
        public Rect DeselectWindow;

        // Variable used in scroll view for the sidebar editor
        Vector2 ScrollPos;


        /// <summary>
        /// Unity Method | OnEnable, sets up the editor perfs for to save/load script creation options...
        /// </summary>
        private void OnEnable()
        {
            shouldClearFields = true;

            useNamespace = EditorPrefs.GetBool("CarterGames-Shortcuts-UseNamespace");
            fileName = EditorPrefs.GetString("CarterGames-Shortcuts-FileName");
            fileNamespace = EditorPrefs.GetString("CarterGames-Shortcuts-FileNamespace");
            shouldClearFields = EditorPrefs.GetBool("CarterGames-Shortcuts-ShouldClearFields");
        }


        /// <summary>
        /// The menu item that opens the sidebar editor that this script is mostly about.
        ///   SHORTCUT: Shift + Alt + S
        /// </summary>
        [MenuItem("Carter Games/Shortcuts/Sidebar #&s", priority = 5)]
        public static void ShowWindow()
        {
            GetWindow<ShortcutsEditorWindow>("Shortcuts Sidebar");
        }


        /// <summary>
        /// A menu item that resets the transform component on any selected gameobjects in the scene.
        ///   SHORTCUT: Shift + Alt + R
        /// </summary>
        [MenuItem("Carter Games/Shortcuts/Reset Transform #&r", priority = 5)]
        public static void ResetTransform()
        {
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                Selection.gameObjects[i].transform.localPosition = Vector3.zero;
                Selection.gameObjects[i].transform.rotation = Quaternion.identity;
                Selection.gameObjects[i].transform.localScale = Vector3.one;
            }
        }


        /// <summary>
        /// A menu item that opens the Unity package manager.
        ///   SHORTCUT: Shift + Alt + P
        /// </summary>
        [MenuItem("Carter Games/Shortcuts/Open Package Manager #&p", priority = 5)]
        public static void OpenPM()
        {
            EditorApplication.ExecuteMenuItem("Window/Package Manager");
        }


        /// <summary>
        /// A menu item that opens the project settings window.
        ///   SHORTCUT: Shift + Alt + E
        /// </summary>
        [MenuItem("Carter Games/Shortcuts/Open Project Settings #&e", priority = 5)]
        public static void OpenProj()
        {
            SettingsService.OpenProjectSettings("Project");
        }


        /// <summary>
        /// A menu item that opens the editor preferences.
        ///   SHORTCUT: Shift + Alt + D
        /// </summary>
        [MenuItem("Carter Games/Shortcuts/Open Preferences #&d", priority = 5)]
        public static void OpenPref()
        {
            SettingsService.OpenUserPreferences("Preferences");
        }


        /// <summary>
        /// Unity Merthod | OnGUI - Creates the sidebar window.
        /// </summary>
        public void OnGUI()
        {
            // Creates a massive button to deselect when an element that is not a button of field is pressed.
            DeselectWindow = new Rect(0, 0, position.width, position.height);

            // Calls a method to define the min/max sixes for this editor window when undocked.
            SetMinMaxWindowSize();

            // The Header GUI - Logo, Title & Docs/Discord buttons 
            HeaderLogo();
            HeaderText();
            HeaderVersionAndButtons();

            GUILayout.Space(2.5f);

            // Starting the scroll view
            ScrollPos = EditorGUILayout.BeginScrollView(ScrollPos, GUILayout.Width(position.width), GUILayout.ExpandHeight(true));

            // Calling all the methods for each section of buttons, put into method to clean up the OnGUI method.
            ScriptTemplates();
            CheckForChanges();
            GeneralButtons();
            PreferencesButtons();
            ProjectSettingsButtons();
            BuildSettingsButtons();

            // Shows the Carter Games Buttons if one of the assets this window supports is in the project.
            if (checkAMP != null || checkSM != null)
            {
                CarterGamesButtons();
            }

            // Ends the scroll view
            EditorGUILayout.EndScrollView();

            // Makes it so you can deselect elements in the window by adding a button the size of the window that you can't see under everything
            //make sure the following code is at the very end of OnGUI Function
            if (GUI.Button(DeselectWindow, "", GUIStyle.none))
            {
                GUI.FocusControl(null);
            }
        }


        /// <summary>
        /// Defines the min and max size for the editor window
        /// </summary>
        private void SetMinMaxWindowSize()
        {
            EditorWindow editorWindow = this;
            editorWindow.minSize = new Vector2(300f, 500f);
            editorWindow.maxSize = new Vector2(800f, 900f);
        }


        /// <summary>
        /// GUI | Shows the Shortcuts logo or an alt text if it was not imported with the package.
        /// </summary>
        private void HeaderLogo()
        {
            GUILayout.Space(10f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            // Shows either the Carter Games Logo or an alternative for if the icon is deleted when you import the package.
            if (Resources.Load<Texture2D>("Carter Games/Shortcuts/LogoSC"))
            {
                if (GUILayout.Button(Resources.Load<Texture2D>("Carter Games/Shortcuts/LogoSC"), GUIStyle.none, GUILayout.Width(50), GUILayout.Height(50)))
                {
                    GUI.FocusControl(null);
                }
            }
            else
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();
                EditorGUILayout.LabelField("Shortcuts", EditorStyles.boldLabel, GUILayout.Width(75f));
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10f);
        }


        /// <summary>
        /// GUI | Shows the text title for the window
        /// </summary>
        private void HeaderText()
        {
            // Label that shows the name of the script / tool & the Version number for reference sake.
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Sidebar", EditorStyles.boldLabel, GUILayout.Width(60f));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }


        /// <summary>
        /// GUI | shows the documetation / discord buttons
        /// </summary>
        private void HeaderVersionAndButtons()
        {
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Version: 1.0.1", GUILayout.Width(87.5f));
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(2.5f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Documentation", GUILayout.Width(110f)))
            {
                Application.OpenURL("https://carter.games/shortcuts.html");
            }
            GUI.backgroundColor = Color.cyan;
            if (GUILayout.Button("Discord", GUILayout.Width(65f)))
            {
                Application.OpenURL("https://carter.games/discord");
            }
            GUI.backgroundColor = Color.white;
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }


        /// <summary>
        /// Script Template Buttons
        /// </summary>
        private void ScriptTemplates()
        {
            GUILayout.Space(10);


            EditorGUILayout.LabelField("Script Creation", EditorStyles.boldLabel, GUILayout.Width(120));

            EditorGUILayout.BeginVertical("Box");

            GUILayout.Space(2.5f);

            EditorGUILayout.LabelField("Controls", EditorStyles.boldLabel, GUILayout.Width(120));

            EditorGUILayout.BeginHorizontal();
            useNamespace = EditorGUILayout.Toggle("Inc Namespace: ", useNamespace);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            fileName = EditorGUILayout.TextField("File Name: ", fileName);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            shouldClearFields = EditorGUILayout.Toggle("Keep File Name?: ", shouldClearFields);
            EditorGUILayout.EndHorizontal();

            if (useNamespace)
            {
                EditorGUILayout.BeginHorizontal();
                fileNamespace = EditorGUILayout.TextField("Namespace: ", fileNamespace);
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(3.5f);

            if (fileName.Length > 0)
            {
                EditorGUILayout.LabelField("Options", EditorStyles.boldLabel, GUILayout.Width(120));
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                GUI.backgroundColor = pinkCol;
                if (GUILayout.Button("New Monobehaviour", GUILayout.MaxWidth(145), GUILayout.Height(30)))
                {
                    CreateScript(0);
                }

                if (GUILayout.Button("New Scriptable Object", GUILayout.MaxWidth(155), GUILayout.Height(30)))
                {
                    CreateScript(1);
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("New C# Class", GUILayout.MaxWidth(115), GUILayout.Height(30)))
                {
                    CreateScript(2);
                }

                if (GUILayout.Button("New Interface", GUILayout.MaxWidth(115), GUILayout.Height(30)))
                {
                    CreateScript(3);
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("New Custom Inspector", GUILayout.MaxWidth(155), GUILayout.Height(30)))
                {
                    CreateScript(4);
                }

                if (GUILayout.Button("New Editor Window", GUILayout.MaxWidth(140), GUILayout.Height(30)))
                {
                    CreateScript(5);
                }

                GUI.backgroundColor = Color.white;

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }
            GUILayout.Space(2.5f);

            EditorGUILayout.EndVertical();
            
        }

        /// <summary>
        /// Checks for changes in the editor and saves the editor perfs accordingly
        /// </summary>
        private void CheckForChanges()
        {
            if (useNamespace != EditorPrefs.GetBool("CarterGames-Shortcuts-UseNamespace"))
            {
                EditorPrefs.SetBool("CarterGames-Shortcuts-UseNamespace", useNamespace);
            }

            if (fileName != EditorPrefs.GetString("CarterGames-Shortcuts-FileName") && GUIUtility.hotControl.Equals(0))
            {
                EditorPrefs.SetString("CarterGames-Shortcuts-FileName", fileName);
            }

            if (fileNamespace != EditorPrefs.GetString("CarterGames-Shortcuts-FileNamespace") && GUIUtility.hotControl.Equals(0))
            {
                EditorPrefs.SetString("CarterGames-Shortcuts-FileNamespace", fileNamespace);
            }

            if (shouldClearFields != EditorPrefs.GetBool("CarterGames-Shortcuts-ShouldClearFields"))
            {
                EditorPrefs.SetBool("CarterGames-Shortcuts-ShouldClearFields", shouldClearFields);
            }
        }


        /// <summary>
        /// General Buttons
        /// </summary>
        private void GeneralButtons()
        {
            GUILayout.Space(10);

            EditorGUILayout.LabelField("General", EditorStyles.boldLabel, GUILayout.Width(100));


            EditorGUILayout.BeginVertical("Box");

            GUILayout.Space(2.5f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUI.backgroundColor = Color.grey;

            if (GUILayout.Button("Package Manager", GUILayout.Width(125), GUILayout.Height(30)))
            {
                EditorApplication.ExecuteMenuItem("Window/Package Manager");
            }

            if (GUILayout.Button("AI Navigation", GUILayout.Width(110), GUILayout.Height(30)))
            {
                EditorApplication.ExecuteMenuItem("Window/AI/Navigation");
            }

            GUI.backgroundColor = Color.white;
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(2.5f);

            EditorGUILayout.EndVertical();
        }



        /// <summary>
        /// Preferences Buttons
        /// </summary>
        private void PreferencesButtons()
        {
            GUILayout.Space(10);

            EditorGUILayout.LabelField("Preferences", EditorStyles.boldLabel, GUILayout.Width(100));


            EditorGUILayout.BeginVertical("Box");

            GUILayout.Space(2.5f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUI.backgroundColor = yelCol;
            if (GUILayout.Button("Editor Colours", GUILayout.Width(110), GUILayout.Height(30)))
            {
                SettingsService.OpenUserPreferences("Preferences/Colors");
            }

            if (GUILayout.Button("External Tools", GUILayout.Width(110), GUILayout.Height(30)))
            {
                SettingsService.OpenUserPreferences("Preferences/External Tools");
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            if (Application.unityVersion.Contains("2018"))
            {
                if (GUILayout.Button("Editor Key Bindings", GUILayout.Width(135), GUILayout.Height(30)))
                {
                    SettingsService.OpenUserPreferences("Preferences/Keys");
                }
            }

            if (GUILayout.Button("GI Cache", GUILayout.Width(95), GUILayout.Height(30)))
            {
                SettingsService.OpenUserPreferences("Preferences/GI Cache");
            }

            GUI.backgroundColor = Color.white;
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(2.5f);

            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Player Settings Buttons 
        /// </summary>
        private void ProjectSettingsButtons()
        {
            GUILayout.Space(10);

            EditorGUILayout.LabelField("Project Settings", EditorStyles.boldLabel, GUILayout.Width(110));


            EditorGUILayout.BeginVertical("Box");

            GUILayout.Space(2.5f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUI.backgroundColor = ornCol;

            if (GUILayout.Button("Audio", GUILayout.Width(80), GUILayout.Height(30)))
            {
                SettingsService.OpenProjectSettings("Project/Audio");
            }

            if (GUILayout.Button("Editor", GUILayout.Width(80), GUILayout.Height(30)))
            {
                SettingsService.OpenProjectSettings("Project/Editor");
            }

            if (GUILayout.Button("Graphics", GUILayout.Width(80), GUILayout.Height(30)))
            {
                SettingsService.OpenProjectSettings("Project/Graphics");
            }


            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();


            if (GUILayout.Button("Input", GUILayout.Width(80), GUILayout.Height(30)))
            {
                SettingsService.OpenProjectSettings("Project/Input");
            }

            if (GUILayout.Button("Physics", GUILayout.Width(80), GUILayout.Height(30)))
            {
                SettingsService.OpenProjectSettings("Project/Physics");
            }

            if (GUILayout.Button("Physics 2D", GUILayout.Width(80), GUILayout.Height(30)))
            {
                SettingsService.OpenProjectSettings("Project/Physics 2D");
            }


            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();


            if (GUILayout.Button("Player", GUILayout.Width(80), GUILayout.Height(30)))
            {
                SettingsService.OpenProjectSettings("Project/Player");
            }

            if (GUILayout.Button("Quality", GUILayout.Width(80), GUILayout.Height(30)))
            {
                SettingsService.OpenProjectSettings("Project/Quality");
            }

            if (GUILayout.Button("VFX", GUILayout.Width(80), GUILayout.Height(30)))
            {
                SettingsService.OpenProjectSettings("Project/VFX");
            }

            GUI.backgroundColor = Color.white;
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(2.5f);

            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Build Settings Buttons 
        /// </summary>
        private void BuildSettingsButtons()
        {
            GUILayout.Space(10);

            EditorGUILayout.LabelField("Build Settings", EditorStyles.boldLabel, GUILayout.Width(110));


            EditorGUILayout.BeginVertical("Box");

            GUILayout.Space(2.5f);

            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();

            GUI.backgroundColor = grnCol;

            if (GUILayout.Button("Open Build Settings", GUILayout.Width(145), GUILayout.Height(30)))
            {
                 GetWindow(Type.GetType("UnityEditor.BuildPlayerWindow,UnityEditor"));
            }


            GUI.backgroundColor = Color.white;
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(2.5f);

            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Shows buttons for Carter Games Assets that are in the unity project.
        /// This only shows values if one of our other assets are in your project somewhere. 
        /// Otherwise nothing will show...
        /// </summary>
        private void CarterGamesButtons()
        {
            GUILayout.Space(10);

            EditorGUILayout.LabelField("Carter Games", EditorStyles.boldLabel, GUILayout.Width(110));


            EditorGUILayout.BeginVertical("Box");

            GUILayout.Space(5f);


            if (checkAMP != null)
            {
                GUI.backgroundColor = redCol;
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (Resources.Load<Texture2D>("Carter Games/Audio Manager +/LogoAMP"))
                {
                    if (GUILayout.Button(Resources.Load<Texture2D>("Carter Games/Audio Manager +/LogoAMP"), GUIStyle.none, GUILayout.Width(40), GUILayout.Height(35)))
                    {
                        GUI.FocusControl(null);
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("Audio Manager +", EditorStyles.boldLabel, GUILayout.Width(115));
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Advanced Editor", GUILayout.Width(125), GUILayout.Height(30)))
                {
                    Type.GetType("CarterGames.Assets.AudioManagerPlus.AdvancedAudioEditorWindow").GetMethod("ShowWindow").Invoke(this, null);
                }

                if (GUILayout.Button("File Convertor", GUILayout.Width(125), GUILayout.Height(30)))
                {
                    Type.GetType("CarterGames.Assets.AudioManagerPlus.AMF2AMPF").GetMethod("ShowWindow").Invoke(this, null);
                }

                GUI.backgroundColor = Color.white;
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.Space(5);


            if (checkSM != null)
            {
                GUI.backgroundColor = bluCol;
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (Resources.Load<Texture2D>("Carter Games/Save Manager/LogoSM"))
                {
                    if (GUILayout.Button(Resources.Load<Texture2D>("Carter Games/Save Manager/LogoSM"), GUIStyle.none, GUILayout.Width(40), GUILayout.Height(35)))
                    {
                        GUI.FocusControl(null);
                    }
                }
                else
                {
                    EditorGUILayout.LabelField("Save Manager", EditorStyles.boldLabel, GUILayout.Width(95));
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.FlexibleSpace();

                if (GUILayout.Button("Save Data Editor", GUILayout.Width(125), GUILayout.Height(30)))
                {
                    checkSM.GetMethod("ShowWindow").Invoke(this, null);
                }

                GUI.backgroundColor = Color.white;
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndHorizontal();
            }



            GUILayout.Space(5f);

            EditorGUILayout.EndVertical();
        }


        /// <summary>
        /// Creates a new script based on the type inputted.
        /// </summary>
        /// <param name="type">int for button order (0=Mono, 1=SO, 2=Class, 3=Inter, 4=EditorIns, 5=EditorWin</param>
        private void CreateScript(int type = 0)
        {
            string path = AssetDatabase.GetAssetPath(Selection.activeObject);


            if (!type.Equals(4) && !type.Equals(5))
            {
                if (Directory.Exists(Application.dataPath + "/Scripts"))
                {
                    path = "Assets/Scripts/" + fileName + ".cs";
                }
                else
                {
                    AssetDatabase.CreateFolder("Assets", "Scripts");
                    path = "Assets/Scripts/" + fileName + ".cs";
                }
            }
            else
            {
                if (Directory.Exists(Application.dataPath + "/Editor"))
                {
                    path = "Assets/Editor/" + fileName + ".cs";
                }
                else
                {
                    AssetDatabase.CreateFolder("Assets", "Editor");
                    path = "Assets/Editor/" + fileName + ".cs";
                }
            }
            

            if (!File.Exists(path))
            {
                // makes a monobehaviour
                using (StreamWriter outfile =
                    new StreamWriter(path))
                {
                    // adds the editor namespace if needed
                    if (type.Equals(4) || type.Equals(5))
                    {
                        outfile.WriteLine("using UnityEditor;");
                    }

                    // adds unity engine if needed
                    if (type.Equals(0) || type.Equals(1) || type.Equals(4) || type.Equals(5))
                    {
                        outfile.WriteLine("using UnityEngine;");
                    }

                    outfile.WriteLine("using System.Collections.Generic;");
                    outfile.WriteLine("");

                    if (useNamespace)
                    {
                        outfile.WriteLine("namespace " + fileNamespace);
                        outfile.WriteLine("{");

                        // Goes through and sets the correct header for the script type
                        switch (type)
                        {
                            case 0:
                                outfile.WriteLine("    public class " + fileName + " : MonoBehaviour");
                                break;
                            case 1:
                                outfile.WriteLine("    public class " + fileName + " : ScriptableObject");
                                break;
                            case 2:
                                outfile.WriteLine("    public class " + fileName);
                                break;
                            case 3:
                                outfile.WriteLine("    public interface " + fileName);
                                break;
                            case 4:
                                outfile.WriteLine("    /*[CustomEditor(typeof(###))] Edit to put your class into the hastags uncomment*/");
                                outfile.WriteLine("    public class " + fileName + " : Editor");
                                break;
                            case 5:
                                outfile.WriteLine("    public class " + fileName + " : EditorWindow");
                                break;
                            default:
                                break;
                        }

                        outfile.WriteLine("    {");

                        // Goes through and sorts the file content if needed...
                        switch (type)
                        {
                            case 0:
                                outfile.WriteLine("        private void Start()");
                                outfile.WriteLine("        {");
                                outfile.WriteLine("        ");
                                outfile.WriteLine("        }");
                                outfile.WriteLine("");
                                outfile.WriteLine("        private void Update()");
                                outfile.WriteLine("        {");
                                outfile.WriteLine("        ");
                                outfile.WriteLine("        }");
                                break;
                            case 4:
                                outfile.WriteLine("        public override void OnInspectorGUI()");
                                outfile.WriteLine("        {");
                                outfile.WriteLine("            /*### script = (###)target Edit to put your class into the hastags uncomment*/");
                                outfile.WriteLine("        ");
                                outfile.WriteLine("            base.OnInspectorGUI();");
                                outfile.WriteLine("        }");
                                break;
                            case 5:
                                outfile.WriteLine("        [MenuItem(\"Tools/###\")]");
                                outfile.WriteLine("        public static void ShowWindow()");
                                outfile.WriteLine("        {");
                                outfile.WriteLine("            GetWindow<" + fileName + ">();");
                                outfile.WriteLine("        }");
                                outfile.WriteLine("        ");
                                outfile.WriteLine("        public void OnGUI()");
                                outfile.WriteLine("        {");
                                outfile.WriteLine("        ");
                                outfile.WriteLine("        }");
                                break;
                            default:
                                outfile.WriteLine("");
                                break;
                        }

                        outfile.WriteLine("    }");
                        outfile.WriteLine("}");
                    }
                    else
                    {
                        // Goes though and sets the correct header for the script type
                        switch (type)
                        {
                            case 0:
                                outfile.WriteLine("public class " + fileName + " : MonoBehaviour");
                                break;
                            case 1:
                                outfile.WriteLine("public class " + fileName + " : ScriptableObject");
                                break;
                            case 2:
                                outfile.WriteLine("public class " + fileName);
                                break;
                            case 3:
                                outfile.WriteLine("public interface " + fileName);
                                break;
                            case 4:
                                outfile.WriteLine("/*[CustomEditor(typeof(###))] Edit to put your class into the hastags uncomment*/");
                                outfile.WriteLine("public class " + fileName + " : Editor");
                                break;
                            case 5:
                                outfile.WriteLine("public class " + fileName + " : EditorWindow");
                                break;
                            default:
                                break;
                        }

                        outfile.WriteLine("{");

                        // Goes through and sorts the file content if needed...
                        switch (type)
                        {
                            case 0:
                                outfile.WriteLine("    private void Start()");
                                outfile.WriteLine("    {");
                                outfile.WriteLine("    ");
                                outfile.WriteLine("    }");
                                outfile.WriteLine("    ");
                                outfile.WriteLine("    private void Update()");
                                outfile.WriteLine("    {");
                                outfile.WriteLine("    ");
                                outfile.WriteLine("    }");
                                break;
                            case 4:
                                outfile.WriteLine("    public override void OnInspectorGUI()");
                                outfile.WriteLine("    {");
                                outfile.WriteLine("        /*### script = (###)target Edit to put your class into the hastags uncomment*/");
                                outfile.WriteLine("        ");
                                outfile.WriteLine("        base.OnInspectorGUI();");
                                outfile.WriteLine("    }");
                                break;
                            case 5:
                                outfile.WriteLine("    [MenuItem(\"Tools/###\")]");
                                outfile.WriteLine("    public static void ShowWindow()");
                                outfile.WriteLine("    {");
                                outfile.WriteLine("        GetWindow<" + fileName + ">();");
                                outfile.WriteLine("    }");
                                outfile.WriteLine("    ");
                                outfile.WriteLine("    public void OnGUI()");
                                outfile.WriteLine("    {");
                                outfile.WriteLine("    ");
                                outfile.WriteLine("    }");
                                break;
                            default:
                                outfile.WriteLine("");
                                break;
                        }

                        outfile.WriteLine("}");
                    }
                }

                // Clears the file name after a script has been created.
                if (!shouldClearFields)
                {
                    fileName = "";
                }

                AssetDatabase.Refresh();
            }
        }
    }
}