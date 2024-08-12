using UnityEditor;
using UnityEngine; // Ensure UnityEngine is included for GameObject references

[CustomEditor(typeof(Interactable), true)] // Use Interactable instead of InteractableEditor
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target; // Cast to Interactable, not InteractableEditor
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.promptMessage = EditorGUILayout.TextField("Prompt Message", interactable.promptMessage);
            EditorGUILayout.HelpBox("EventOnlyInteractable can ONLY use UnityEvents.", MessageType.Info);
            if (interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else
        {
            base.OnInspectorGUI();
            if (interactable.useEvents)
            {
                if (interactable.GetComponent<InteractionEvent>() == null)
                {
                    interactable.gameObject.AddComponent<InteractionEvent>();
                }
            }
            else
            {
                InteractionEvent interactionEvent = interactable.GetComponent<InteractionEvent>();
                if (interactionEvent != null)
                {
                    DestroyImmediate(interactionEvent);
                }
            }
        }
    }
}
