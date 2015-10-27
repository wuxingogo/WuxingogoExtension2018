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
=======
RuntimeDesDirctory="./ReleaseDLL/Runtime/"
#
EditorDesDirctory="./ReleaseDLL/Editor/"

cp $RuntimeSrcDirctory/WuxingogoRuntime.dll $RuntimeDesDirctory

echo "Wuxingogo:Copy Success"

cp $EditorSrcDirctory/WuxingogoEditor.dll $EditorDesDirctory
>>>>>>> a2f18d844b4456f0146cc1ec8a025bc8d05e9dd2

echo "Wuxingogo:Copy Success"
