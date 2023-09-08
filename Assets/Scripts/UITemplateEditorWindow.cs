using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class UITemplateEditorWindow : EditorWindow
{
    private string jsonFilePath = "Assets/UITemplates.json";
    private UITemplateData templateData;
    private Vector2 scrollPosition;
    private UITemplate selectedTemplate;

    private bool showBackgroundImageSection = true;
    private bool showAdIconSection = true;
    private bool showAdNameSection = true;
    private bool showAdDescriptionSection = true;
    private bool showInstallButtonSection = true;

    [MenuItem("Window/UITemplate Editor")]
    public static void ShowWindow()
    {
        GetWindow<UITemplateEditorWindow>("UITemplate Editor");
    }
    private void OnGUI()
    {
        GUILayout.Label("UI Template Editor", EditorStyles.boldLabel);

        if (GUILayout.Button("Load Templates"))
        {
            LoadTemplates();
        }

        if (GUILayout.Button("Create New Template"))
        {
            CreateNewTemplate();
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

        if (templateData != null && templateData.templates != null)
        {
            for (int i = 0; i < templateData.templates.Count; i++)
            {
                UITemplate template = templateData.templates[i];
                EditorGUILayout.LabelField("Template Name", EditorStyles.boldLabel);
                template.name = EditorGUILayout.TextField(template.name);

                showBackgroundImageSection = EditorGUILayout.BeginFoldoutHeaderGroup(showBackgroundImageSection, "Back Ground Image");
                if (showBackgroundImageSection)
                {
                    DisplayUIProperties(template.backGroundImage, "Back Ground Image",true);
                }
                EditorGUILayout.EndFoldoutHeaderGroup();

                showAdIconSection = EditorGUILayout.BeginFoldoutHeaderGroup(showAdIconSection, "AD Icon");
                if (showAdIconSection)
                {
                    DisplayUIProperties(template.adIcon, "AD Icon", true);
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
                showAdNameSection = EditorGUILayout.BeginFoldoutHeaderGroup(showAdNameSection, "AD Name");
                if (showAdNameSection)
                {
                    DisplayUIProperties(template.adIcon, "AD Name", true,true);
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
                showAdDescriptionSection = EditorGUILayout.BeginFoldoutHeaderGroup(showAdDescriptionSection, "AD Discription");
                if (showAdDescriptionSection)
                {
                    DisplayUIProperties(template.adIcon, "AD Discription", true,true);
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
                showInstallButtonSection = EditorGUILayout.BeginFoldoutHeaderGroup(showInstallButtonSection, "Install Button");
                if (showInstallButtonSection)
                {
                    DisplayUIProperties(template.adIcon, "Install Button", true);
                }
                EditorGUILayout.EndFoldoutHeaderGroup();
                template.imageConfiguration = (ImageConfiguration)EditorGUILayout.ObjectField("Image Configuration", template.imageConfiguration, typeof(ImageConfiguration), false);
                if (GUILayout.Button("Instantiate"))
                {
                    InstantiateUITemplate(template);
                }

                if (GUILayout.Button("Delete"))
                {
                    DeleteTemplate(i); 
                    break;
                }

                EditorGUILayout.Space();
            }

        }

        EditorGUILayout.EndScrollView();

        if (GUILayout.Button("Save Templates"/*, GUILayout.Width(180)*/))
        {
            SaveTemplates();
        }
        //if (GUILayout.Button("Instantiate", GUILayout.Width(90),Guila)
        //{

        //}

    }

    private void DisplayUIProperties(UIProperties properties, string label, bool includeSize = false,bool showText=false)
    {
        //EditorGUILayout.LabelField(label, EditorStyles.boldLabel);
        properties.position = EditorGUILayout.Vector3Field("Position", properties.position);
        properties.rotation = EditorGUILayout.Vector3Field("Rotation", properties.rotation);
        properties.scale = EditorGUILayout.Vector3Field("Scale", properties.scale);

        if (includeSize)
        {
            properties.width = EditorGUILayout.FloatField("Width", properties.width);
            properties.height = EditorGUILayout.FloatField("Height", properties.height);
        }
        if (showText)
        {
            properties.textArea = EditorGUILayout.TextField(label, properties.textArea);
        }
        if (!string.IsNullOrEmpty(properties.textArea))
        {
            properties.textArea = EditorGUILayout.TextField("Description", properties.textArea);
        }
    }

    private void LoadTemplates()
    {
        if (File.Exists(jsonFilePath))
        {
            string jsonData = File.ReadAllText(jsonFilePath);
            templateData = JsonUtility.FromJson<UITemplateData>(jsonData);
        }
        else
        {
            templateData = new UITemplateData();
            templateData.templates = new List<UITemplate>();
        }
    }

    private void InstantiateUITemplate(UITemplate template)
    {
        UIObjectInstantiator instantiator = FindObjectOfType<UIObjectInstantiator>();

        if (instantiator != null)
        {
            instantiator.InstantiateUIObject(template);
        }
        else
        {
            Debug.LogError("UIObjectInstantiator script not found in the scene.");
        }
    }

    private void DeleteTemplate(int indexToDelete)
    {
        if (templateData != null && templateData.templates != null && indexToDelete >= 0 && indexToDelete < templateData.templates.Count)
        {
            templateData.templates.RemoveAt(indexToDelete);
        }
    }

    private void CreateNewTemplate()
    {
        if (templateData == null)
        {
            templateData = new UITemplateData();
            templateData.templates = new List<UITemplate>();
        }

        UITemplate newTemplate = new UITemplate();
        templateData.templates.Add(newTemplate);
    }

    private void SaveTemplates()
    {
        if (templateData != null)
        {
            string updatedJsonData = JsonUtility.ToJson(templateData, true);
            File.WriteAllText(jsonFilePath, updatedJsonData);
            AssetDatabase.Refresh();
        }
    }
}

[System.Serializable]
public class UITemplate
{
    public string name;

    public ImageConfiguration imageConfiguration;

    public UIProperties backGroundImage = new UIProperties();
    public UIProperties adIcon = new UIProperties();
    public UIProperties adName = new UIProperties();
    public UIProperties adDiscription = new UIProperties();
    public UIProperties installButton = new UIProperties();

}

[System.Serializable]
public class UIProperties
{
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    public float height;
    public float width;

    public string textArea;
    public Image image;
    public Button button;

}
[System.Serializable]
public class UITemplateData
{
    public List<UITemplate> templates;
}
