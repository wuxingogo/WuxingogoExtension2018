#!/usr/bin/python
#coding=utf-8
import sys, csv , operator
import os
import glob
import shutil

# remove all .meta file
def removefile(filepath, suffix):
    print filepath
    for root, dirs, files in os.walk(filepath):
        for name in files:
            if name.endswith(suffix):
                os.remove(os.path.join(root, name))
                print("Delete File: " + os.path.join(root, name))


def copyfile(filepath, despath):
	#print filepath
	#print despath
	shutil.copyfile(filepath,despath)


print "remove all meta"
path = os.getcwd()
removefile(path,".meta")
print "remove all meta success"

print "Build WuxingogoRuntime Start by wuxingogo================"
os.system("msbuild WuxingogoRuntime.sln")
print "Build WuxingogoRuntime Ended by wuxingogo================"

print "Build WuxingogoEditor  Start by wuxingogo================"
os.system("msbuild WuxingogoEditor.sln")
print "Build WuxingogoEditor  Ended by wuxingogo================"


#copyfile("WuxingogoExtension\Editor\WuxingogoEditor.dll", "E:\Work\Xingyu\SunSongSunshine\Assets\WuxingogoExtension\Editor\WuxingogoEditor.dll")
#copyfile("WuxingogoExtension\Runtime\WuxingogoRuntime.dll", "E:\Work\Xingyu\SunSongSunshine\Assets\WuxingogoExtension\Runtime\WuxingogoRuntime.dll")

#copyfile("WuxingogoExtension\Editor\WuxingogoEditor.dll", "E:\Work\CastingWorkBase\Downfall\Assets\WuxingogoExtension\Editor\WuxingogoEditor.dll")
#copyfile("WuxingogoExtension\Runtime\WuxingogoRuntime.dll", "E:\Work\CastingWorkBase\Downfall\Assets\WuxingogoExtension\Runtime\WuxingogoRuntime.dll")

print("Copy Success!")

