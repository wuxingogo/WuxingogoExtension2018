#
RuntimeSrcDirctory="./WuxingogoRuntime/bin/Debug/"
#
EditorSrcDirctory="./WuxingogoEditor/bin/Debug/"
#
RuntimeDesDirctory="./ReleaseDLL/Runtime/"
#
EditorDesDirctory="./ReleaseDLL/Editor/"

cp $RuntimeSrcDirctory/WuxingogoRuntime.dll $RuntimeDesDirctory

echo "Wuxingogo:Copy Success"

cp $EditorSrcDirctory/WuxingogoEditor.dll $EditorDesDirctory

echo "Wuxingogo:Copy Success"
