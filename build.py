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
	print filepath
	print despath
	shutil.copyfile(filepath,despath)
def abspath(filepath):
    return os.path.abspath


print "remove all meta"
path = os.getcwd()
removefile(path,".meta")
print "remove all meta success"

UsePlatform()


copyfile("OutPutDll/WuxingogoEditor.dll", "WuxingogoExtension/Editor/WuxingogoEditor.dll")
copyfile("OutPutDll/WuxingogoRuntime.dll", "WuxingogoExtension/Runtime/WuxingogoRuntime.dll")

#copyfile("WuxingogoExtension/Editor/WuxingogoEditor.dll", "/Users\ly-account\Documents\work\WuxingogoExtension\WuxingogoExtension\Editor\WuxingogoEditor.dll")
#copyfile("WuxingogoExtension/Runtime/WuxingogoRuntime.dll", "/Users\ly-account\Documents\work\WuxingogoExtension\WuxingogoExtension\Runtime\WuxingogoRuntime.dll")

print("Copy Success!")

