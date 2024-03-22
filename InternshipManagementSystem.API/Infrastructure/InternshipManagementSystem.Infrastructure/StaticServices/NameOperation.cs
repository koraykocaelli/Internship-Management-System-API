using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternshipManagementSystem.Infrastructure.StaticServices
{
    public static class NameOperation
    {
        public static string CharacterRegulator(string name)
        {

            return name.Trim().ToLower().Replace(" ", "-")
                .Replace("ç", "c").Replace("ğ", "g").Replace("ı", "i")
                .Replace("ö", "o").Replace("ş", "s").Replace("ü", "u")
                .Replace("â", "a").Replace("î", "i").Replace("û", "u")
                .Replace(" ", "-").Replace("?", "").Replace("!", "")
                .Replace(":", "").Replace(";", "").Replace("(", "")
                .Replace(")", "").Replace("[", "").Replace("]", "")
                .Replace("{", "").Replace("}", "").Replace("/", "")
                .Replace("\\", "").Replace("'", "").Replace("\"", "")
                .Replace("<", "").Replace(">", "").Replace(",", "")
                .Replace(".", "").Replace("–", "-").Replace("—", "-")
                .Replace("---", "-").Replace("--", "-").Replace("?", "")
                .Replace("!", "").Replace(":", "").Replace(";", "")
                .Replace("(", "").Replace(")", "").Replace("[", "")
                .Replace("]", "").Replace("{", "").Replace("}", "")
                .Replace("/", "").Replace("\\", "").Replace("'", "")
                .Replace("\"", "").Replace("<", "").Replace(">", "")
                .Replace(",", "").Replace(".", "").Replace("–", "-")
                .Replace("—", "-").Replace("---", "-").Replace("--", "-")
                .Replace("?", "").Replace("!", "").Replace(":", "")
                .Replace(";", "").Replace("(", "").Replace(")", "")
                .Replace("[", "").Replace("]", "").Replace("{", "")
                .Replace("}", "").Replace("/", "").Replace("\\",   "")
                .Replace("'", "").Replace("\"", "").Replace("<", "")
                .Replace(">", "").Replace(",", "").Replace(".", "")
                .Replace("–", "-").Replace("—", "-").Replace("---", "-")
                .Replace("--", "-").Replace("?", "").Replace("!", "")
                .Replace(":", "").Replace(";", "").Replace("(", "")
                .Replace(")", "").Replace("[", "").Replace("]", "")
                .Replace("{", "").Replace("}", "").Replace("/", "")
                .Replace("\\", "").Replace("'", "").Replace("\"", "")
                .Replace("<", "").Replace(">", "").Replace(",", "");

        }
    }
}
