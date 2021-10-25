using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace random_loginname_maker
{
    public class PseudoGenerator
    {
        public string word1;
        public string word2;
        public int nb;
        public string result;

        public List<string> list1;
        public List<string> list2;

        public static List<string> ParseFile(string link)
        {
            List<string> parsedFile = new List<string>();
            WebClient client = new WebClient();
            Stream stream = client.OpenRead(link);
            StreamReader reader = new StreamReader(stream);
            
            string line;
            while((line = reader.ReadLine()) != null)
            {
                parsedFile.Add(line);
            }
            stream.Close();
            return parsedFile;
                      
        }
        public static string GetPseudo(List<string> list1, List<string> list2)
        {
            Random randL1 = new Random();
            Random randL2 = new Random();
            Random randNb = new Random();
            
            PseudoGenerator p = new PseudoGenerator();
            int r = randL1.Next(list1.Count);
            p.word1 = list1[r];
            int ra = randL2.Next(list2.Count);
            p.word2 = list2[ra];
            p.nb = randNb.Next(1001);
            p.result = p.word1 + p.word2 + p.nb.ToString();
            return p.result;                 
        }

        //public void Validate()
        //{
        //    pseudo = word1 + word2 + nb.ToString();
        //}

    }
}
