using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kizilay.Core.HashService
{
    public interface IHashGeneratorService
    {
        string GenerateHash(string text);
        bool CompareHash(string hashedText, string text);
    }
}
