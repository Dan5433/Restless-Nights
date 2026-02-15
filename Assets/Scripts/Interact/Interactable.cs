using EditorAttributes;
using Extensions;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] protected Color outlineColor = Color.white;
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] MaterialPropertyBlock materialProperties;
    [SerializeField] protected AudioSource audioSource;

    protected virtual bool CanInteract => true;
    public bool IsAudioPlaying => audioSource.isPlaying;

    const string OUTLINE_MATERIAL_NAME = "Sprite Lit Outline";
    const string OUTLINE_COLOR_PROPERTY_NAME = "_OutlineColor";
    const string OUTLINE_ENABLED_PROPERTY_NAME = "_OutlineEnabled";

    public abstract AudioClip InteractSFX { get; }

    protected virtual void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        InitalizeFields();
    }

    private void OnValidate()
    {
        InitalizeFields();
    }

    void InitalizeFields()
    {
        if (!spriteRenderer)
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        if (materialProperties == null)
        {
            materialProperties = new MaterialPropertyBlock();
            UpdateMaterialPropertyBlock();
        }

        if (!spriteRenderer.sharedMaterial.name.Equals(OUTLINE_MATERIAL_NAME))
            Debug.LogWarning("SpriteRenderer on Interactable does not have outline material selected!");

        materialProperties.SetColor(OUTLINE_COLOR_PROPERTY_NAME, outlineColor);

        spriteRenderer.SetPropertyBlock(materialProperties);
    }

    public void Interact()
    {
        if (!CanInteract)
            return;

        if (InteractInternal())
            PlayInteractSFX();
    }

    public void PlayInteractSFX()
    {
        audioSource.PlayOneShotWithRandomPitch(InteractSFX);
    }

    [Button(nameof(RaycastEnter), 36)]
    public void RaycastEnter()
    {
        if (!CanInteract)
            return;

        materialProperties.SetInteger(OUTLINE_ENABLED_PROPERTY_NAME, 1);
        spriteRenderer.SetPropertyBlock(materialProperties);
    }

    [Button(nameof(RaycastExit), 36)]
    public void RaycastExit()
    {
        if (!CanInteract)
            return;

        materialProperties.SetInteger(OUTLINE_ENABLED_PROPERTY_NAME, 0);
        spriteRenderer.SetPropertyBlock(materialProperties);
    }

    protected void UpdateMaterialPropertyBlock()
    {
        spriteRenderer.GetPropertyBlock(materialProperties);
    }

    public void PlaySFX(AudioClip clip)
    {
        audioSource.PlayOneShotWithRandomPitch(clip);
    }

    protected abstract bool InteractInternal();
}
