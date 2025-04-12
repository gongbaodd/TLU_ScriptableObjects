using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    [SerializeField] List<CharacterConfig> characterConfigs; // List of character configurations
    [SerializeField] VisualTreeAsset selectorTree;
    UIDocument uiDocument; // Reference to the UI document
    List<Button> selectors;

    TemplateContainer disabledSelector;

    IEnumerator PlaySprite(VisualElement portrait, Sprite[] sprites)
    {
        int index = 0;
        while (true)
        {
            portrait.style.backgroundImage = new StyleBackground(sprites[index]);
            index = (index + 1) % sprites.Length;
            yield return new WaitForSeconds(0.1f);
        }
    }

    TemplateContainer CreateSelector(CharacterConfig config)
    {
        var selector = selectorTree.CloneTree();
        selector.AddToClassList("item-template");

        var nameLabel = selector.Q<Label>("name");
        var descriptionLabel = selector.Q<Label>("description");
        var strengthLabel = selector.Q<Label>("strength");
        var charismaLabel = selector.Q<Label>("charisma");
        var intelligenceLabel = selector.Q<Label>("intelligence");
        var knowledgeLabel = selector.Q<Label>("knowledge");
        var portrait = selector.Q<VisualElement>("portrait");

        nameLabel.text = config.name;
        descriptionLabel.text = config.description;
        strengthLabel.text = config.strength.ToString();
        charismaLabel.text = config.charisma.ToString();
        intelligenceLabel.text = config.intelligence.ToString();
        knowledgeLabel.text = config.knowledge.ToString();
        portrait.style.backgroundImage = new StyleBackground(config.Portrait[0]);

        StartCoroutine(PlaySprite(portrait, config.Portrait));

        return selector;
    }

    void InitSelectors()
    {
        var selectionPanel = uiDocument.rootVisualElement.Q<VisualElement>("left");

        foreach (var config in characterConfigs)
        {
            var selector = CreateSelector(config);
            selectionPanel.Add(selector);

            selector.RegisterCallback<ClickEvent>(evt =>
            {
                disabledSelector?.SetEnabled(true);
                selector.SetEnabled(false);
                disabledSelector = selector;

                var characterPanel = uiDocument.rootVisualElement.Q<VisualElement>("right");
                characterPanel.Clear();
                characterPanel.Add(CreateSelector(config));
            });
        }
    }

    void Awake()
    {
        uiDocument = GetComponent<UIDocument>();

        InitSelectors();
    }


}
