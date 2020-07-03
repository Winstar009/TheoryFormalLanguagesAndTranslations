using System;
using System.Collections.Generic;
using System.IO;

namespace CodeAnalyzer
{
    class GenerateAutomatonKWText
    {
        public static void Start()
        {
            List<Tuple<List<int>, char, List<int>>> letters = new List<Tuple<List<int>, char, List<int>>>();

            List<string> kws = new List<string>();
            using (StreamReader reader = new StreamReader("../../Resources/Keywords.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string s = reader.ReadLine().Trim();
                    if (s != "")
                    {
                        kws.Add(s);
                    }
                }
            }

            List<int> F = new List<int>();
            int q = 1;
            kws.ForEach((kw) =>
            {
                for (int i = 0; i < kw.Length; i++)
                {
                    int index = letters.FindIndex(l => l.Item2 == kw[i]);
                    if (index >= 0)
                    {
                        List<int> s = letters[index].Item1;
                        s.Add(q);
                        List<int> f = letters[index].Item3;
                        f.Add(++q);
                        letters[index] = new Tuple<List<int>, char, List<int>>(s, kw[i], f);
                    }
                    else
                    {
                        letters.Add(new Tuple<List<int>, char, List<int>>(new List<int> { 1 }, kw[i], new List<int> { ++q }));
                    }
                }
                F.Add(q);
            });

            using (StreamWriter writer = new StreamWriter("../../Resources/KeywordsFunc.txt"))
            {
                writer.WriteLine("#\\Delta");
                letters.ForEach((letter) =>
                {
                    string startState = "";
                    letter.Item1.ForEach((item) =>
                    {
                        startState += "Q" + item + " ";
                    });
                    startState = startState.Trim();

                    string finalState = "";
                    letter.Item3.ForEach((item) =>
                    {
                        finalState += "Q" + item + " ";
                    });
                    finalState = finalState.Trim();

                    writer.WriteLine("{" + startState + "}{\\" + letter.Item2 + "}{" + finalState + "}");
                });
                writer.WriteLine("#end\n");

                writer.WriteLine("#S");
                writer.WriteLine("Q1");
                writer.WriteLine("#end\n");

                writer.WriteLine("#F");
                string f = "";
                F.ForEach((item) =>
                {
                    f += "Q" + item + " ";
                });
                f = f.Trim();
                writer.WriteLine(f);
                writer.WriteLine("#end");
            }
        }
    }
}
