using System.Globalization;
namespace DemoMVC.Models.Xulychuoi{
    public class Xulychuoi{

         private static readonly string[] VietnameseSigns = new string[]
            {
            "aAeEoOuUiIdDyY",
            "áàạảãâấầậẩẫăắằặẳẵ",
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",
            "éèẹẻẽêếềệểễ",
            "ÉÈẸẺẼÊẾỀỆỂỄ",
            "óòọỏõôốồộổỗơớờợởỡ",
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",
            "úùụủũưứừựửữ",
            "ÚÙỤỦŨƯỨỪỰỬỮ",
            "íìịỉĩ",
            "ÍÌỊỈĨ",
            "đ",
            "Đ",
            "ýỳỵỷỹ",
            "ÝỲỴỶỸ"
            };
        public static string removeSpaceInString(string stringInput)
            {
                string stringProcessed = stringInput.Trim();
                int index = stringProcessed.IndexOf("  ");
                while(index > 0)
                {
                    stringProcessed = stringProcessed.Replace("  ", " ");
                }
                return stringProcessed;
            }

            public static string toUpperString(string stringInput)
            {
                string stringProcessed = stringInput.ToUpper();
                return stringProcessed;
            }

            public static string toLowerString(string stringInput)
            {
                string stringProcessed = stringInput.ToLower();
                return stringProcessed;
            }

            public static string CapitalizeOneFirstCharacter(string stringInput)
            {
                string stringProcessed = stringInput.Trim();
                string firstCharacter = stringProcessed.Substring(0,1).ToUpper() + stringProcessed.Substring(1);
                
                
                return firstCharacter;
            }

            public static string CapitalizeFirstCharacter(string stringInput)
            {
                
                string stringProcessed = "";
                TextInfo cultInfo = new CultureInfo("en-US", false).TextInfo;
                stringProcessed = cultInfo.ToTitleCase(stringInput);
                return stringProcessed;
            }

            public static string RemoveVietnameseAccents(string strInput)
                {
                string strResult = "";
                for (int i = 1; i < VietnameseSigns.Length; i++)
                {
                    for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    strInput = strInput.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
                }
                strResult = strInput;
                return strResult;
                }
    }

}
