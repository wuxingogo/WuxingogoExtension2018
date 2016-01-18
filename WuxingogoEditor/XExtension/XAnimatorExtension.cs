using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditorInternal;
using UnityEditor.Animations;

public class XAnimatorExtension : XBaseWindow
{
    RuntimeAnimatorController _controller = null;
    AnimatorType _animatorType = AnimatorType.AnimatorController;
    GameObject _fbxModel = null;
    Object[] objects = null;
    AnimationCurve curve = null;

    Animator _animator = null;
    [MenuItem("Wuxingogo/Wuxingogo XAnimatorExtension ")]
    static void init()
    {
        InitWindow<XAnimatorExtension>();
    }

    public override void OnXGUI()
    {

        _animatorType = (AnimatorType)CreateEnumSelectable("AnimatorType", _animatorType);
        switch (_animatorType)
        {
            case AnimatorType.AnimatorController:
                ShowAnimatorController();
                break;
            case AnimatorType.AnimatorModel:
                ShowAnimatorModel();
                break;
            case AnimatorType.Animator:
                ShowAnimatorInfo();
                break;
        }
    }

    void ShowAnimatorController()
    {
        _controller = (RuntimeAnimatorController)CreateObjectField("animator", _controller, typeof(RuntimeAnimatorController));

        if (null != _controller)
        {


            EditorGUILayout.BeginHorizontal();
            CreateLabel("clip");
            CreateLabel("duration");
            CreateLabel("isloop");
            EditorGUILayout.EndHorizontal();

            if (_controller.GetType() == typeof(UnityEditor.Animations.AnimatorController))
            {
                UnityEditor.Animations.AnimatorController controller = _controller as UnityEditor.Animations.AnimatorController;
#if UNITY_4_6
                 ShowAnimatorControllerLayer(controller.GetLayer(0));
#endif
#if UNITY_5
                ShowAnimatorControllerLayer(controller.layers[0]);
#endif

            }
            else if (_controller.GetType() == typeof(AnimatorOverrideController))
            {
                AnimatorOverrideController overrideController = _controller as AnimatorOverrideController;
                //				AnimationClipPair[] clips = (AnimationClipPair[])_controller.GetType().BaseType.GetProperty("clips").GetValue(_controller, null);
                //ShowAnimatorOverrideControllerClips(overrideController.clips);
            }

        }

    }

    void ShowAnimatorModel()
    {
        _fbxModel = (GameObject)CreateObjectField("model", _fbxModel, typeof(GameObject));

        string path = AssetDatabase.GetAssetPath(_fbxModel);

        ModelImporter modelIm = (ModelImporter)ModelImporter.GetAtPath(path);
        //		AnimationClip newClip = AssetDatabase.LoadAssetAtPath(path, typeof(AnimationClip)) as AnimationClip;

        EditorGUILayout.BeginHorizontal();
        CreateLabel("name");
        CreateLabel("start");
        CreateLabel("end");
        CreateLabel("isLooping");
        EditorGUILayout.EndHorizontal();

        if (modelIm == null)
            return;
        ModelImporterClipAnimation[] animations = modelIm.clipAnimations;
        for (int pos = 0; pos < animations.Length; pos++)
        {
            EditorGUILayout.BeginHorizontal();
            animations[pos].name = CreateStringField(animations[pos].name);
            animations[pos].firstFrame = CreateFloatField(animations[pos].firstFrame);
            animations[pos].lastFrame = CreateFloatField(animations[pos].lastFrame);
            animations[pos].loop = CreateCheckBox(animations[pos].loop);
            //			animations[pos].
            CreateLabel(animations[pos].loop.ToString());
            EditorGUILayout.EndHorizontal();
        }
        if (CreateSpaceButton("Insert"))
        {
            ModelImporterClipAnimation[] newAnim = new ModelImporterClipAnimation[animations.Length + 1];
            animations.CopyTo(newAnim, 0);
            animations = newAnim;
            animations[animations.Length - 1] = animations[0];

        }
        if (CreateSpaceButton("Sub"))
        {
            ModelImporterClipAnimation[] newAnim = new ModelImporterClipAnimation[animations.Length - 1];
            System.Array.Copy(animations, newAnim, newAnim.Length);
            animations = newAnim;
        }
        if (GUI.changed)
        {
            modelIm.clipAnimations = animations;

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }

    void ShowAnimatorControllerLayer(UnityEditor.Animations.AnimatorControllerLayer layer)
    {
#if UNITY_4_6
        StateMachine stateMachine = layer.stateMachine;

        for (int pos = 0; pos < stateMachine.stateCount; pos++)
        {
            Motion motion = stateMachine.GetState(pos).GetMotion();
            if (null != motion)
            {
                EditorGUILayout.BeginHorizontal();
                CreateLabel(stateMachine.GetState(pos).name);
                CreateLabel(motion.averageDuration.ToString());
                CreateLabel(motion.isLooping.ToString());
                EditorGUILayout.EndHorizontal();
            }

        }
#elif UNITY_5
        AnimatorStateMachine stateMachine = layer.stateMachine;
        ChildAnimatorState[] states = layer.stateMachine.states;


        for (int pos = 0; pos < states.Length; pos++)
        {
            Motion motion = states[pos].state.motion;
            if (null != motion)
            {
                EditorGUILayout.BeginHorizontal();
                CreateLabel(motion.name);
                CreateLabel(motion.averageDuration.ToString());
                CreateLabel(motion.isLooping.ToString());
                EditorGUILayout.EndHorizontal();
            }

        }
#endif
    }

    void ShowAnimatorInfo()
    {
        _animator = CreateObjectField("animator", _animator, typeof(Animator)) as Animator;
        if( null != _animator)
        {
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
            for (int i = 0; i < clips.Length; i++)
            {
                BeginHorizontal();
                CreateLabel(clips[i].name);
                CreateLabel(clips[i].averageDuration.ToString());
                CreateLabel(clips[i].isLooping.ToString());
                EndHorizontal();
            }
        }
    }

    void ShowAnimatorOverrideControllerClips(AnimationClipPair[] clips)
    {
        //for (int pos = 0; pos < clips.Length; pos++)
        //{
        //    EditorGUILayout.BeginHorizontal();
        //    CreateLabel(clips[pos].originalClip.name);
        //    CreateLabel(clips[pos].overrideClip.averageDuration.ToString());
        //    CreateLabel(clips[pos].overrideClip.isLooping.ToString());
        //    EditorGUILayout.EndHorizontal();
        //}
    }

    void OnSelectionChange()
    {
        //TODO List
        //		if(Selection.objects.Length > 0)
        //			Debug.Log("obj is " + Selection.objects[0].GetType());
    }

    public static Animation GetAnimation(Animator animator)
    {
        //		Animation anim = animator.animation;
        //		int count = anim.GetClipCount();
        // Assembly ass = typeof(Editor).Assembly;
        // System.Type refType = ass.GetType("UnityEditor.AnimationEditor");

        // PropertyInfo property = refType.GetProperty("target");

        // object target = property.GetValue(refType,null);

        // Animation anim = (Animation)target;

        return null;
    }

    internal enum AnimatorType
    {
        AnimatorController,
        AnimatorModel,
        Animator
    }

}