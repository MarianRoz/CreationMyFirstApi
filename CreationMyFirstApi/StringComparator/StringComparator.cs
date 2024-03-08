using CreationMyFirstApi.Models;
using CreationMyFirstApi.Services;
using System.Collections;
using System.Text.RegularExpressions;

namespace CreationMyFirstApi
{
    public class StringComparator
    {

        private static List<IEnumerable> WordLetterPairs(string str)
        {
            List<IEnumerable> AllPairs = new();

            string[] Words = Regex.Split(str, @"\s");

            for (int w = 0; w < Words.Length; w++)
            {
                if (!string.IsNullOrEmpty(Words[w]))
                {
                    String[] PairsInWord = LetterPairs(Words[w]);

                    for (int p = 0; p < PairsInWord.Length; p++)
                    {
                        AllPairs.Add(PairsInWord[p]);
                    }
                }
            }
            return AllPairs;
        }
        private static string[] LetterPairs(string str)
        {
            int numPairs = str.Length - 1;
            string[] pairs = new string[numPairs];

            for (int i = 0; i < numPairs; i++)
            {
                pairs[i] = str.Substring(i, 2);
            }
            return pairs;
        }
        public static double CompareStrings(string str1, string str2)
        {
            List<IEnumerable> pairs1 = WordLetterPairs(str1.ToUpper());
            List<IEnumerable> pairs2 = WordLetterPairs(str2.ToUpper());

            int intersection = 0;
            int union = pairs1.Count + pairs2.Count;

            for (int i = 0; i < pairs1.Count; i++)
            {
                for (int j = 0; j < pairs2.Count; j++)
                {
                    if (pairs1[i].ToString() == pairs2[j].ToString())
                    {
                        intersection++;
                        pairs2.RemoveAt(j);
                        break;
                    }
                }
            }
            return (2.0 * intersection * 100) / union;
        }
        public int Key { get; set; }
        public double Similarity { get; set; }
        public AnnouncementService Merge(AnnouncementEntity p)
        {
            return new AnnouncementService { Key = this.Key, Similarity = this.Similarity };
        }
    }
}
