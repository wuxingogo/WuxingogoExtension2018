#!/usr/bin/python
#coding=utf-8
import sys, csv , operator
import os
import glob
import shutil
import platform

def removefile(filepath, suffix):
    print ("Root Path : " + filepath)
    for root, dirs, files in os.walk(filepath):
        for name in files:
            if name.endswith(suffix):
                os.remove(os.path.join(root, name))
                print("Delete File: " + os.path.join(root, name))
                

def UsePlatform():
  sysstr = platform.system()
  if(sysstr =="Windows"):
    DoWindowsCommand();
  else:
    DoUnixCommand();
  print ("Environment : " + sysstr)


def DoWindowsCommand():
    os.system("msbuild WuxingogoRuntime.sln")
    os.system("msbuild WuxingogoEditor.sln")

def DoUnixCommand():
    os.system("xbuild WuxingogoRuntime.sln")
    os.system("xbuild WuxingogoEditor.sln")


def copyfile(filepath, despath):
    try:
        shutil.copyfile(filepath,despath)
        print(filepath + " or " + despath + " was success !")
    except Exception: 
        print(filepath + " or " + despath + " was error !")
    pass
	

def abspath(filepath):
    return os.path.abspath

def currPath():
    return os.path.abspath(os.curdir)

def CopyToProject(despath):
    copyfile( currPath()+"/" + editorSourceFile,despath + "Editor/WuxingogoEditor.dll")
    copyfile( currPath()+"/" + runtimeSourceFile,despath + "Plugins/WuxingogoRuntime.dll");
    copyfile( currPath()+"/" + editorDllPdb,despath + "Editor/WuxingogoEditor.pdb")
    copyfile( currPath()+"/" + editorDllMdb,despath + "Editor/WuxingogoEditor.dll.mdb")
    copyfile( currPath()+"/" + runtimeDllPdb,despath + "Plugins/WuxingogoRuntime.pdb")
    copyfile( currPath()+"/" + runtimeDllMdb,despath + "Plugins/WuxingogoRuntime.dll.mdb")
    
    


print "remove all meta"
path = os.getcwd()
removefile("WuxingogoEditor", ".meta")
removefile("WuxingogoRuntime", ".meta")
print "remove all meta success"

UsePlatform()

os.chdir(currPath())

isSecure = 0
editorSourceFile = "OutPutDll/WuxingogoEditor.dll"
runtimeSourceFile = "OutPutDll/WuxingogoRuntime.dll"
editorDllPdb = "OutPutDll/WuxingogoEditor.pdb"
editorDllMdb = "OutPutDll/WuxingogoEditor.dll.mdb"
runtimeDllPdb = "OutPutDll/WuxingogoRuntime.pdb"
runtimeDllMdb = "OutPutDll/WuxingogoRuntime.dll.mdb"

copyfile(editorSourceFile, "WuxingogoExtension/Editor/WuxingogoEditor.dll")
copyfile(runtimeSourceFile, "WuxingogoExtension/Plugins/WuxingogoRuntime.dll")

if(isSecure == 1):
    os.system("Reactor.lnk -file " + currPath()+"\\" + editorSourceFile + " -mono 1 -unprintable_characters 1")
    os.system("Reactor.lnk -file " + currPath()+"\\" + editorSourceFile + " -mono 1 -unprintable_characters 1")

    editorSourceFile = "OutPutDll\WuxingogoEditor_Secure\WuxingogoEditor.dll"
    runtimeSourceFile = "OutPutDll\WuxingogoRuntime_Secure\WuxingogoRuntime.dll"





sysstr=platform.system()
if(sysstr =="Windows"):
    copyfile(currPath()+"\\" + editorSourceFile,"E:/Work/Xingyu/SunSongSunshine/Assets/Plugins/WuxingogoExtension/Editor/WuxingogoEditor.dll")
    copyfile(currPath()+"\\" + runtimeSourceFile,"E:\Work\Xingyu\SunSongSunshine\Assets\Plugins\WuxingogoExtension\Plugins\WuxingogoRuntime.dll")
    copyfile(currPath()+"\\" + editorSourceFile, "E:/Work/UnityProject/New Unity Project/Assets/WuxingogoExtension/Editor/WuxingogoEditor.dll")
    copyfile( currPath()+"\\" + runtimeSourceFile, "E:\Work\UnityProject\New Unity Project\Assets\WuxingogoExtension\Plugins\WuxingogoRuntime.dll")

    copyfile( currPath()+"\\" + editorSourceFile, "E:\Work\CastingWorkBase\OneSideWar\Assets\Plugins\WuxingogoExtension\Editor\WuxingogoEditor.dll")
    copyfile( currPath()+"\\" + runtimeSourceFile, "E:\Work\CastingWorkBase\OneSideWar\Assets\Plugins\WuxingogoExtension\Plugins\WuxingogoRuntime.dll")

    copyfile( currPath()+"\\" + editorSourceFile, "E:\Work\UnityProject\PublishWuxingogo\Assets\Plugins\WuxingogoExtension\Editor\WuxingogoEditor.dll")
    copyfile( currPath()+"\\" + runtimeSourceFile, "E:\Work\UnityProject\PublishWuxingogo\Assets\Plugins\WuxingogoExtension\Plugins\WuxingogoRuntime.dll")
    copyfile( currPath()+"\\" + editorSourceFile, "E:\Work\NewSunSongSunshine\Assets\Plugins\WuxingogoExtension\Editor\WuxingogoEditor.dll")
    copyfile( currPath()+"\\" + runtimeSourceFile, "E:\Work\NewSunSongSunshine\Assets\Plugins\WuxingogoExtension\Plugins\WuxingogoRuntime.dll")

    copyfile( currPath()+"\\" + editorSourceFile, "E:\Work\UnityProject\PhysicsDemo\Assets\Plugins\WuxingogoExtension\Editor\WuxingogoEditor.dll")
    copyfile( currPath()+"\\" + runtimeSourceFile, "E:\Work\UnityProject\PhysicsDemo\Assets\Plugins\WuxingogoExtension\Plugins\WuxingogoRuntime.dll")
else:
    # copyfile(currPath()+"/" + editorSourceFile,"/Users/Wuxingogo/Documents/UnityProject/Casting/OneSideWar/Assets/Plugins/WuxingogoExtension/Editor/WuxingogoEditor.dll")
    # copyfile(currPath()+"/" + runtimeSourceFile,"/Users/Wuxingogo/Documents/UnityProject/Casting/OneSideWar/Assets/Plugins/WuxingogoExtension/Plugins/WuxingogoRuntime.dll")

    copyfile(currPath()+"/" + editorSourceFile,"/Users/wuxingogo/Documents/UnityProject/MyProject/Wuliao/Assets/Plugins/WuxingogoExtension/Editor/WuxingogoEditor.dll")
    copyfile(currPath()+"/" + runtimeSourceFile,"/Users/wuxingogo/Documents/UnityProject/MyProject/Wuliao/Assets/Plugins/WuxingogoExtension/Plugins/WuxingogoRuntime.dll")
    CopyToProject("/Users/Wuxingogo/Documents/UnityProject/Casting/OneSideWar/Assets/Plugins/WuxingogoExtension/")


#print currPath()  + "OutPutDll\WuxingogoEditor.dll"



#copyfile( currPath()  + "\OutPutDll\WuxingogoEditor.dll", "E:/Work/UnityProject/New Unity Project/Assets/WuxingogoExtension/Editor/WuxingogoEditor.dll")
#copyfile( currPath()  + "\OutPutDll\WuxingogoRuntime.dll", "E:\Work\UnityProject\New Unity Project\Assets\WuxingogoExtension\Runtime\WuxingogoRuntime.dll")


