#!/usr/bin/python
#coding=utf-8
import xml.dom.minidom
import os
#myName = input("What is your name?")
#myVar = 'Hello'
#print(myName)
#print(myVar)
#打开xml文档
 dom = xml.dom.minidom.parse('WuxingogoEditor/WuxingogoEditor.csproj')
 #得到文档元素对象
 root = dom.documentElement
 print root.nodeName
 # print root.nodeValue
 # print root.nodeType
 # print root.ELEMENT_NODE

 print "Build WuxingogoRuntime Start by wuxingogo================"
 os.system("xbuild WuxingogoRuntime.sln")
 print "Build WuxingogoRuntime Ended by wuxingogo================"

 print "Build WuxingogoEditor  Start by wuxingogo================"
 os.system("xbuild WuxingogoEditor.sln")
 print "Build WuxingogoEditor  Ended by wuxingogo================"