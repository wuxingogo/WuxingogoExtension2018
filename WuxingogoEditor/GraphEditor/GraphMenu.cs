using UnityEngine;
using UnityEditor;
using System.Collections;

namespace GraphEditor
{
    public class GraphMenu
    {
        public GraphMenu(int menuType)
        {
            GenericMenu menu = new GenericMenu();
            if (menuType == -1)
            {
                menu.AddItem(new GUIContent("Add Base Node"), false, NoneCallback, "AddBaseNode");
                menu.AddItem(new GUIContent("Add Element Node"), false, NoneCallback, "AddElement");
                menu.AddItem(new GUIContent("Add Behaviour Node"), false, NoneCallback, "AddBehaviour");
                menu.AddItem(new GUIContent("Add Condition Node"), false, NoneCallback, "AddCondition");
                menu.AddItem(new GUIContent("Add Model Node/Int"), false, NoneCallback, "AddIntModel");
                menu.AddItem(new GUIContent("Add Model Node/Float"), false, NoneCallback, "AddFloatModel");
                menu.AddItem(new GUIContent("Add Model Node/String"), false, NoneCallback, "AddStringModel");
            }
            else
            {
                menu.AddItem(new GUIContent("Make Transition"), false, SelectedCallback, "MakeTransition");
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Delete Node"), false, SelectedCallback, "DeleteNode");
            }

            menu.ShowAsContext();

        }

        void NoneCallback(object handle)
        {
            BaseNode baseNode = null;
            switch (handle.ToString())
            {
                case "AddBaseNode":
                    baseNode = new BaseNode();
                    break;
                case "AddElement":
                    baseNode = new ElementNode();
                    break;
                case "AddBehaviour":
                    baseNode = new BehaviourNode();
                    break;
                case "AddCondition":
                    baseNode = new ConditionNode();
                    break;
                case "AddStringModel":
                    baseNode = new ModelNode<string>("");
                    break;
                case "AddIntModel":
                    baseNode = new ModelNode<int>(0);
                    break;
                case "AddFloatModel":
                    baseNode = new ModelNode<float>(0.0f);
                    break;
            }
            GraphWindow.GetInstance().AddNode(baseNode);
        }
        void SelectedCallback(object handle)
        {
            switch (handle.ToString())
            {
                case "MakeTransition":
                    GraphWindow.GetInstance().SetCurrentTransition();
                    break;
                case "DeleteNode":
                    GraphWindow.GetInstance().DeletedNode();
                    break;
            }
        }

    }
}