using UnityEngine;
using System.Collections;

namespace wuxingogo.Code
{
    public interface ICodeMember
    {
        System.CodeDom.CodeTypeMember Compile();
    }
}
