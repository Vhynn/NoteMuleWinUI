using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;

namespace XKCDPasswordGen
{
    //Class To generate PassPhrase style passwords
    public class XkcdPasswordGen
    {
        internal readonly string[] words;
        internal Random rnd;

        public XkcdPasswordGen()
        {
           words = GetWordsList();
           rnd = new Random();
        }

        //Generates the actual password
        internal string Generate(int numWords)
        {
            if (numWords < 2)
            {
                numWords= 2;
            }

            int max = words.Length;

            string password = words[rnd.Next(max)];
            
            for(int i = 1; i < numWords; ++i)
            {
                password += GenerateSeperator(rnd) + words[rnd.Next(max)];
            }
            return password;
        }

        //Gets an array of all words in WordList.txt
        private static string[] GetWordsList()
        {
            string[] words;

            Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);
            string Root = Directory.GetCurrentDirectory();

            string file = Root + "\\" + "WordList.txt";
            //Debug.WriteLine(file);
            string filecontents = File.ReadAllText(file);

            words = filecontents.Split(",");

            return words;
        }

        //Generates a random inner chunk to place between words with numbers and symbols to fill out pw requirements
        private static string GenerateSeperator(Random rnd)
        {
            String[] symbols = { "!", "@", "#", "$", "%", "&", "*" };
            String seperator = rnd.Next(100, 999).ToString() + symbols[rnd.Next(0, 6)];

            return seperator;
        }
    }
}
