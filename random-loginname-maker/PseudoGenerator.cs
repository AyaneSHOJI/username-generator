using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

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

        public class WordsList
        {
            public int _id;
            public string word;
        }

        public static void ParseFile(string fileName)
        {
            MongoClient client = new MongoClient();
            client.DropDatabase("pseudo");
            var db = client.GetDatabase("pseudo");
            var table = db.GetCollection<WordsList>("wordsList");
            
            StreamReader file = new StreamReader(fileName);
            string line;

            int bufferSize = 1000;
            List<WordsList> buffer = new List<WordsList>();

            //For increment integer ID instead of using ObjectId 
            int idx = 0;

            while ((line = file.ReadLine()) != null)
            {
                buffer.Add(new WordsList() { word = line, _id = idx++ });

                if(buffer.Count > bufferSize)
                {
                    table.InsertMany(buffer);
                    buffer.Clear();
                }
            }
            
            table.InsertMany(buffer);
        }

         //Pseudo is composed of "first word(from Frech dico)" "second word(from Mots positifs)" "numbers"
        public static string GetPseudo()
        {
            MongoClient client = new MongoClient();
            var db = client.GetDatabase("pseudo");
            var table = db.GetCollection<WordsList>("wordsList");

            Random randWord = new Random();
            Random randNb = new Random();

            PseudoGenerator p = new PseudoGenerator();

            //French dico id between 0-22741
            int r1 = randWord.Next(0, 22741);
            //mots positif id between 22742-22851
            int r2 = randWord.Next(22742, 22852);
            
            BsonArray nbs = new BsonArray { r1, r2 };
            var r = table.Find(Builders<WordsList>.Filter.In(_ => _._id, nbs)).ToList();
            string firstWord = r[0].word;
            string secondWord = r[1].word;

            //Capitalize first letter
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;

            //Remove white space
            string trimmedFirstWord = Regex.Replace(firstWord, @"s", "");

            //Random numbers between 0-1000
            p.nb = randNb.Next(1001);

            p.result = ti.ToTitleCase(trimmedFirstWord) + ti.ToTitleCase(secondWord) + p.nb.ToString();
            return p.result;
        }

        //------------ With files-------------------
        //public static List<string> ParseFile(string link)
        //{
        //    List<string> parsedFile = new List<string>();
        //    WebClient client = new WebClient();
        //    Stream stream = client.OpenRead(link);
        //    StreamReader reader = new StreamReader(stream);            
        //    string line;
        //    while((line = reader.ReadLine()) != null)
        //    {
        //        parsedFile.Add(line);
        //    }
        //    stream.Close();
        //    return parsedFile;                      
        //}
        //public static string GetPseudo(List<string> list1, List<string> list2)
        //{
        //    Random randL1 = new Random();
        //    Random randL2 = new Random();
        //    Random randNb = new Random();

        //    PseudoGenerator p = new PseudoGenerator();
        //    int r = randL1.Next(list1.Count);
        //    p.word1 = list1[r];
        //    int ra = randL2.Next(list2.Count);
        //    p.word2 = list2[ra];
        //    p.nb = randNb.Next(1001);
        //    p.result = p.word1 + p.word2 + p.nb.ToString();
        //    return p.result;      

        //}



    }
}
