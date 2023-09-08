using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIObjectInstantiator : MonoBehaviour
{
    public void InstantiateUIObject(UITemplate template)
    {
        //For Canvas...
        GameObject canvasObject = new GameObject(template.name + "Canvas");
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();
        canvas.transform.position = Vector3.zero ;
        canvas.transform.rotation = Quaternion.identity;
        canvas.transform.localScale = Vector3.one;

        //For BackGround Image....
        GameObject backGroundImage = new GameObject("Back Ground Image");
        backGroundImage.transform.SetParent(canvas.transform);
        Image image = backGroundImage.AddComponent<Image>();
        image.sprite = template.imageConfiguration.backGroundImageSprite;
        RectTransform imageRectTransform = image.GetComponent<RectTransform>();
        imageRectTransform.sizeDelta = new Vector2(template.backGroundImage.width, template.backGroundImage.height);
        #region ForColor not using now.....
        //image.color = new Color(
        //    template.color.r / 255f,
        //    template.color.g / 255f,
        //    template.color.b / 255f,
        //    template.color.a / 255f
        //);
        #endregion
        backGroundImage.transform.localPosition = template.backGroundImage.position;
        backGroundImage.transform.localRotation = Quaternion.Euler(template.backGroundImage.rotation);
        backGroundImage.transform.localScale = template.backGroundImage.scale;

        //For AdIcon....
        GameObject adIcon = new GameObject("AD Icon");
        adIcon.transform.SetParent(canvas.transform);
        Image adIconImg = adIcon.AddComponent<Image>();
        adIconImg.sprite = template.imageConfiguration.adIconSprite;
        imageRectTransform = adIconImg.GetComponent<RectTransform>();
        imageRectTransform.sizeDelta = new Vector2(template.adIcon.width, template.adIcon.height);
        adIcon.transform.localPosition = template.adIcon.position;
        adIcon.transform.localRotation = Quaternion.Euler(template.adIcon.rotation);
        adIcon.transform.localScale = template.adIcon.scale;

        //For AdHeading.....
        GameObject textObject = new GameObject("AD Title");
        textObject.transform.SetParent(canvas.transform);
        TextMeshProUGUI text = textObject.AddComponent<TextMeshProUGUI>();
        text.text = template.adName.textArea;
        textObject.transform.localPosition = template.adName.position;
        textObject.transform.localRotation = Quaternion.Euler(template.adName.rotation);
        textObject.transform.localScale = template.adName.scale;

        //For AdDiscription....
        GameObject adDiscription = new GameObject("AD Discription");
        adDiscription.transform.SetParent(canvas.transform);
        TextMeshProUGUI adDiscriptionTxt = adDiscription.AddComponent<TextMeshProUGUI>();
        adDiscriptionTxt.text = template.adDiscription.textArea;
        //adDiscriptionTxt.text = template.adDiscription;
        adDiscription.transform.localPosition = template.adDiscription.position;
        adDiscription.transform.localRotation = Quaternion.Euler(template.adDiscription.rotation);
        adDiscription.transform.localScale = template.adDiscription.scale;
        RectTransform rectTransform = adDiscription.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(template.adDiscription.width, template.adDiscription.height);

        //For AdInstalButton.....
        GameObject installButton = new GameObject("Install Button");
        installButton.transform.SetParent(canvas.transform);
        Button buttonComponent = installButton.AddComponent<Button>();
        Image buttonImage = installButton.AddComponent<Image>();
        buttonComponent.targetGraphic = buttonImage;


        GameObject buttonTxt = new GameObject("TextMeshPro");
        buttonTxt.AddComponent<TextMeshProUGUI>().text = "Install";
        buttonTxt.GetComponent<TextMeshProUGUI>().color = Color.black;
        buttonTxt.transform.SetParent(installButton.transform);
        buttonTxt.transform.localScale= template.installButton.scale;
        buttonTxt.GetComponent<TextMeshProUGUI>().alignment = TextAlignmentOptions.Center;
        installButton.transform.localPosition = template.installButton.position;
        installButton.transform.localRotation = Quaternion.Euler(template.installButton.rotation);
        installButton.transform.localScale = template.installButton.scale;
        RectTransform buttonRect = installButton.GetComponent<RectTransform>();
        buttonRect.sizeDelta = new Vector2(template.installButton.width, template.installButton.height);


        canvas.transform.SetParent(this.transform);


    }
}
