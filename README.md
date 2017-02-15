# WuxingogoExtension

## Intro
------


This repository includes full source code of the WuxingogoExtension libraries。


##Features
------

* Dynamic call method, change field and property object more easliy.
* Create AssetBundle extension (Unity 4.x && 5.x).
* Custom Attribute Collection(Inspector Drawer).
* Quick set unity prefs.
* Generated CSharpCode(CodeDom).
* Finite-State-Machine and Behaviour Tree Editor.
* Hierarchy Extension.
* Static and Stored GameManager: Control your game more easliy.(ScriptableObject)

##Usage

Copy WuxingogoExtension folder to Assets/Plugins.

Command Line environment:

Windows :　msbuild, python2.7(double click build.bat)

OSX : xbuild, python2.7(run build.sh)

-----------

####XAssetBundleWindow. (Unity Version 4.X)

![github](ScreenShot/AssetBundle.png "github") 

####XAssetBundleWindow. (Unity Version 5.X)

7Z Compress & Encrypt AssetsBundle

Incremental update in one file

![github](ScreenShot/AssetBundle.jpg "github") 

The project had been moved to https://github.com/wuxingogo/GameUpdaterTest

####XBehaviour Window (Finite-State-Machine)

Finite-State-Machine Window.

![github](ScreenShot/BehaviourFSM.jpg "github") 

The project had been moved to https://github.com/wuxingogo/Unity-BTFsm

####XCodeGenerateEditor:

`Save Code Template`

Generate `Namespace`,`Field`, `Method`, `Class`, `Comment`, `Attribute`, `Property`

TODO LIST: 

Compile Code From XReflectionWindow.

![github](ScreenShot/CodeGenerate.png "github") 

####XReflectionWindow:

![github](ScreenShot/Reflection.png "github") 


#### X-Attribute

Note: The target script must inherit from XMonoBehaviour.

```c#
    [X]     // Create button in the Inspector
    public void MethodHandleGo(GameObject go)
    {
        XLogger.Log("Test Method");
    }
    [X]     // Reflection this property in the Inspector
    public int Amount
    {
        get{
            return 0;
        }
    }
    private int amountChange = 0;
    [X]     
    public int AmountChange
    {
        get{
            return amountChange;
        }
        set{
            amountChange = value;
        }
    }
    [Disable]   // Disable change this SerializeField
    public int Count = 0;
    
    [X]
    public Dictionary<int, string> mapKeyDict = new Dictionary<int, string>(){
        {1,    "1"},
        {33,   "22"},
        {222,  "32131"}
    };
    [SerializeField]
    private List<string> list = new List<string>()
    {
        "stack1",
        "stack2",
        "stack3"
    };
    [X]
    public Queue<string> queue;
    [X]
    public Stack stack;

    void Reset()
    {
        stack = new Stack (list);
        queue = new Queue<string>(list);
    }

```
![github](ScreenShot/Inspector.png "github") 


#### Hierarchy Extension

Quick toggle and lock.

![github](ScreenShot/Hierachy.png "github") 

Etc.

##Copyright, License & Contributors
-----
MIT license

Contcat:52111314ly@gmail.com, 52111314ly@sina.com

To be continue!















