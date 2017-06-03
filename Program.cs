using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using java.io;
using opennlp.tools.tokenize;
using System.Text.RegularExpressions;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] file = Directory.GetFiles(@"C:\10311209\lab-6-opennlp-ju-zi-qie-fen-XiuXuanLiu\Dataset", "*.html");
            StreamWriter sw = new StreamWriter(@"ReadByTokens.txt");
            foreach (string filename in file)
                using (StreamReader sr = new StreamReader(filename))
                {

                    while (sr.Peek() != -1)
                    {
                        string line = sr.ReadLine();
                        line = Regex.Replace(line, "<P[^>]*>", ""); //[^>] 所有不是 > 的字
                        line = Regex.Replace(line, @"&nbsp;", "");
                        line = Regex.Replace(line, "<DIV[^>]*>", "");
                        line = Regex.Replace(line, "<BR[^>]*>", "");
                        line = Regex.Replace(line, "<img[^>]*title=\"(?'titleName'.*?)\"[^>]*>", "${titleName}");
                        line = Regex.Replace(line, "<[^>]*href.*>(?'Name'.*?)<[^>]*>", "${Name}");
                        line = Regex.Replace(line, "<[^>]*>", "");

                        string[] tokens;
                        InputStream modelIn = new FileInputStream(@"C:\10311209\lab-6-opennlp-ju-zi-qie-fen-XiuXuanLiu\en-token.bin");
                        TokenizerModel model = new TokenizerModel(modelIn);
                        TokenizerME enTokenizer = new TokenizerME(model);
                        tokens = enTokenizer.tokenize(line);
                        for (int i = 0; i < tokens.Length; i++)
                        {
                            sw.Write(tokens[i] + " ");
                            if (tokens[i].Equals("."))
                            {
                                sw.Write("\n");
                            }
                        }
                    }
                }
            sw.Close();
        }
    }
}
