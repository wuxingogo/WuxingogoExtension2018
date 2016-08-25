#!/usr/bin/python
#coding=utf-8
import sys, csv , operator
import os
import glob
import shutil
import platform

# remove all .meta file
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


print "remove all meta"
path = os.getcwd()
removefile(path,".meta")
print "remove all meta success"

UsePlatform()


copyfile("OutPutDll/WuxingogoEditor.dll", "WuxingogoExtension/Editor/WuxingogoEditor.dll")
copyfile("OutPutDll/WuxingogoRuntime.dll", "WuxingogoExtension/Runtime/WuxingogoRuntime.dll")

#print currPath()  + "OutPutDll\WuxingogoEditor.dll"
copyfile(currPath()+"\OutPutDll\WuxingogoEditor.dll","E:/Work/Xingyu/SunSongSunshine/Assets/WuxingogoExtension/Editor/WuxingogoEditor.dll")
copyfile(currPath()+"\OutPutDll\WuxingogoRuntime.dll","E:\Work\Xingyu\SunSongSunshine\Assets\WuxingogoExtension\Runtime\WuxingogoRuntime.dll")
copyfile( currPath()  + "\OutPutDll\WuxingogoEditor.dll", "E:/Work/UnityProject/New Unity Project/Assets/WuxingogoExtension/Editor/WuxingogoEditor.dll")
copyfile( currPath()  + "\OutPutDll\WuxingogoRuntime.dll", "E:\Work\UnityProject\New Unity Project\Assets\WuxingogoExtension\Runtime\WuxingogoRuntime.dll")

