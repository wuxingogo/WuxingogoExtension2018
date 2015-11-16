#
RuntimeSrcDirctory="./WuxingogoRuntime/bin/Debug/"
#
EditorSrcDirctory="./WuxingogoEditor/bin/Debug/"
#
RuntimeDesDirctory="./WuxingogoExtension/Runtime/"
#
EditorDesDirctory="./WuxingogoExtension/Editor/"

cp -r -f $RuntimeSrcDirctory/WuxingogoRuntime.dll $RuntimeDesDirctory

echo "Wuxingogo:Copy Success"

cp -r -f $EditorSrcDirctory/WuxingogoEditor.dll $EditorDesDirctory

echo "Wuxingogo:Copy Success"