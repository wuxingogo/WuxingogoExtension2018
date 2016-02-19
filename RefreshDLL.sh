
echo "Build WuxingogoEditor Start by wuxingogo     ================"

xbuild WuxingogoEditor.sln

echo "Build WuxingogoEditor Success by wuxingogo ================"



#
RuntimeSrcDirctory="./WuxingogoRuntime/bin/Debug/"
#
EditorSrcDirctory="./WuxingogoEditor/bin/Debug/"
#
RuntimeDesDirctory="./WuxingogoExtension/Runtime/"
#
EditorDesDirctory="./WuxingogoExtension/Editor/"

#cp -r -f $RuntimeSrcDirctory/WuxingogoRuntime.dll $RuntimeDesDirctory

#echo "Wuxingogo:Copy Success"

#cp -r -f $EditorSrcDirctory/WuxingogoEditor.dll $EditorDesDirctory

#echo "Wuxingogo:Copy Success"

find .  -name "*.meta" -exec rm -rf {} \;

echo "Remove all .meta"
