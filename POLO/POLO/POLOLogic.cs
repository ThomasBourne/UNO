using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POLO
{
    class POLOLogic
    {
        public bool IsPolo(string path)
        {
            if (!File.Exists(path))
            {
                Errors.InvalidPath(path);
                return false;
            }
            bool extensionName = false;
            string extensionType = "";
            for (int i = 0; i < path.Length; i++)
            {
                if (!extensionName)
                {
                    if (path[i] == '.')
                    {
                        extensionName = !extensionName;
                    }
                }
                else
                {
                    extensionType += path[i];
                }
            }
            for (int i = 0; i < Store.allowedTypes.Length; i++)
            {
                if (Store.allowedTypes[i] == extensionType)
                    return true;
            }
            Errors.InvalidFileExtension(path);
            return false;
        }
    }
}
