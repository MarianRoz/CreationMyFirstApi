using CreationMyFirstApi.Models;
using CreationMyFirstApi.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Text.RegularExpressions;

namespace CreationMyFirstApi.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository announcementRepository;
        public AnnouncementService(IAnnouncementRepository context)
        {
            announcementRepository = context ??
                throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<AnnouncementEntity>> Get()
        {
            using (DbContext connection = new AnnouncementDbContext())
            {
                return await announcementRepository.Get();
            }
        }
        public async Task<AnnouncementEntity> GetAnnouncementById(int iD)
        {
            return await announcementRepository.GetAnnouncementById(iD);
        }
        public async Task<AnnouncementEntity> Create(AnnouncementEntity announcement)
        {
            return await announcementRepository.Create(announcement);
        }

        public async Task<AnnouncementEntity> Update(AnnouncementEntity announcement)
        {
            return await announcementRepository.Update(announcement);
        }

        public bool Delete(int id)
        {
            return announcementRepository.Delete(id);
        }
        public async Task<IEnumerable<AnnouncementDetails>> GetSelectedAnnouncementDetails(int id)

        {
            IList<AnnouncementRepository> array = new List<AnnouncementRepository>();
            IList<AnnouncementRepository> array2 = new List<AnnouncementRepository>();

            foreach (AnnouncementEntity item in await Get())
            {
                array.Add(new AnnouncementRepository { Key = item.Id, Similarity = CompareStrings(item.Title, GetAnnouncementById(id).Result.Title) });
            }
            AnnouncementRepository[] res = array.OrderByDescending(x => x.Similarity).Take(3).ToArray();

            // add code

            return await announcementRepository.GetSelectedAnnouncementDetails(id); //add code
        }
        private List<IEnumerable> WordLetterPairs(string str)
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
        private string[] LetterPairs(string str)
        {
            int numPairs = str.Length - 1;
            string[] pairs = new string[numPairs];

            for (int i = 0; i < numPairs; i++)
            {
                pairs[i] = str.Substring(i, 2);
            }
            return pairs;
        }
        public double CompareStrings(string str1, string str2)
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
    }
}
