using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BFTFLoan.Attributes
{
    public class LegalIDNumberAttribute : ValidationAttribute
    {
        private static readonly Dictionary<char, int> firstLetterMapping = new Dictionary<char, int>(26);
        private readonly int[] authMultiplier = new int[] { 1, 9, 8, 7, 6, 5, 4, 3, 2, 1, 1 };

        static LegalIDNumberAttribute()
        {
            #region 初始化
            // 初始化對照值
            Init();
            #endregion
        }

        private static void Init()
        {
            // 字元 ASCII十進位值, 身份證字號首碼對照值
            // A 65, 10 | H 72, 17 | O 79, 35 | V 86, 29 |
            // B 66, 11 | I 73, 34 | P 80, 23 | W 87, 32 |
            // C 67, 12 | J 74, 18 | Q 81, 24 | X 88, 30 |
            // D 68, 13 | K 75, 19 | R 82, 25 | Y 89, 31 |
            // E 69, 14 | L 76, 20 | S 83, 26 | Z 90, 33 | 
            // F 70, 15 | M 77, 21 | T 84, 27 | 
            // G 71, 16 | N 78, 22 | U 85, 28 | 

            // 身份證字號首碼對照值
            int mappingValue = 10;

            // A 的 ASCII 十進位值是 65
            // Z 的 ASCII 十進位值是 90
            // 所以 charValue 範圍是 65 ~ 90 共 26 個數字
            for (int charValue = 65; charValue <= 90; charValue++)
            {
                // 將十進位值轉為 char
                char numberToChar = Convert.ToChar(charValue);

                // 如果 numberToChar = I, O, W, Z
                // 則會指定對照值
                // continue 是為了不讓 mappingValue + 1
                // 因為這些特例之後的字母對照值是延續特例之前的值 + 1
                switch (numberToChar)
                {
                    case 'I':
                        firstLetterMapping.Add(numberToChar, 34);
                        continue;
                    case 'O':
                        firstLetterMapping.Add(numberToChar, 35);
                        continue;
                    case 'W':
                        firstLetterMapping.Add(numberToChar, 32);
                        continue;
                    case 'Z':
                        firstLetterMapping.Add(numberToChar, 33);
                        continue;
                }

                // 如果 numberToChar != I, O, W, Z
                // 則依照順序填入對照值
                firstLetterMapping.Add(numberToChar, mappingValue);

                // 對照值 + 1
                mappingValue++;
            }
        }
        public override bool IsValid(object value)
        {

            #region 將傳入的身分證字號轉成 string
            // 將要驗證的值轉成 string
            // 因為參數的型別是 object
            string idNumber = Convert.ToString(value);
            #endregion

            #region 正規表示式驗證
            // Regex Pattern
            Regex regex = new Regex("^[A-Z]{1}[0-9]{9}$");

            if (!regex.IsMatch(idNumber)) return false;
            #endregion

            #region 驗證長度是否為 10
            // 如果長度 != 10 則回傳 false
            if (idNumber.Length != 10) return false;
            #endregion

            #region 將身分證字號分割成 string array
            // 將使用者輸入的身分證字號 split 成 string array
            string[] splitedIdNumber = new string[10];
            char[] idNumberChars = idNumber.ToCharArray();

            for (int i = 0; i < idNumberChars.Length; i++)
            {
                splitedIdNumber[i] = Convert.ToString(idNumberChars[i]);
            }
            #endregion

            #region 驗證第 2 碼
            // 如果第 2 碼不是 1 也不是 2 則回傳 false
            if (splitedIdNumber[1] != "1" && splitedIdNumber[1] != "2") return false;
            #endregion

            #region 將身份證字號轉成對照值
            // 將身分證字號轉成對照值 + 後 9 碼
            // 共 11 碼
            int[] idNumberMappingValues = new int[11];

            // 將身份證字號首碼轉成 char
            char firstChar = Convert.ToChar(idNumber[0]);

            // 使用 LINQ 尋找和首碼相同的 Key 所對應的 Value
            int mappingValue = firstLetterMapping
                .Where(c => c.Key == firstChar)
                .FirstOrDefault()
                .Value;

            // 因為對照值是 int
            // 所以將對照值 / 10 取十位數
            // 將對照值 % 10 取個位數
            // 並加入 idNumberValue 陣列
            idNumberMappingValues[0] = mappingValue / 10;
            idNumberMappingValues[1] = mappingValue % 10;

            // 將身分證字號後 9 碼轉成 int 並依序填入 idNumberValue 陣列
            for (int i = 2; i < idNumberMappingValues.Length; i++)
            {
                idNumberMappingValues[i] = Convert.ToInt32(splitedIdNumber[i - 1]);
            }
            #endregion

            #region 將對照值加權
            // 加權後總和
            int weightedResult = 0;

            // 將對照值依序乘以 1, 9, 8, 7, 6, 5, 4, 3, 2, 1, 1
            for (int i = 0; i < idNumberMappingValues.Length; i++)
            {
                weightedResult += idNumberMappingValues[i] * authMultiplier[i];
            }
            #endregion

            // 將加權後總和 / 10
            // 如果整除 則為合法身分證字號
            return weightedResult % 10 == 0;
        }
    }
}