#!/usr/bin/python
#coding=utf-8
import sys, csv , operator
import os
import glob

def removefile(filepath, suffix):
        print filepath
        for root, dirs, files in os.walk(filepath):
                for name in files:
                        if name.endswith(suffix):
                                os.remove(os.path.join(root, name))
                                print("Delete File: " + os.path.join(root, name))


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
