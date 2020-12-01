using CodeAnalyzer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace UnitTestTheoryFormalLanguagesAndTranslations
{
    [TestClass]
    public class LexicalAnalysisTests
    {
        private void LoadFiniteStateAutomatons(ref LexicalAnalysis lexicalAnalysis)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "../../../CodeAnalyzer/FiniteStateAutomatons");
            lexicalAnalysis.LoadFiniteStateAutomatons(Directory.GetFiles(path));
        }

        [TestMethod]
        public void TestFiniteStateAutomatonCHAR()
        {
            LexicalAnalysis lexicalAnalysis = new LexicalAnalysis();
            string path = Path.Combine(Environment.CurrentDirectory, "../../../CodeAnalyzer/FiniteStateAutomatons/CHAR.txt");
            lexicalAnalysis.LoadFiniteStateAutomatons(new string[] { path });
            Assert.AreEqual("<\"CHAR\",\'5\'>", lexicalAnalysis.Lexer("\'5\'", true));
            Assert.AreEqual("<\"CHAR\",\'а\'>", lexicalAnalysis.Lexer("\'а\'", true));
            Assert.AreEqual("<\"CHAR\",\'\\\\\'>", lexicalAnalysis.Lexer("\'\\\\\'", true));
            Assert.AreEqual("<\"CHAR\",\'\\u0000\'>", lexicalAnalysis.Lexer("\'\\u0000\'", true));
            Assert.AreEqual("<\"CHAR\",\'\\uFFFF\'>", lexicalAnalysis.Lexer("\'\\uFFFF\'", true));
            Assert.AreEqual("<\"CHAR\",\'\\u1234\'>", lexicalAnalysis.Lexer("\'\\u1234\'", true));
            Assert.AreEqual("<\"CHAR\",\'\\xabcd\'>", lexicalAnalysis.Lexer("\'\\xabcd\'", true));
            Assert.AreEqual("<\"CHAR\",\'\\U12345678\'>", lexicalAnalysis.Lexer("\'\\U12345678\'", true));

            Assert.AreEqual("error", lexicalAnalysis.Lexer("\'\'", true));
            Assert.AreEqual("error", lexicalAnalysis.Lexer("\'123\'", true));
            Assert.AreEqual("error", lexicalAnalysis.Lexer("\'\\xABCH\'", true));
            Assert.AreEqual("error", lexicalAnalysis.Lexer("\'\\U1\'", true));
        }

        [TestMethod]
        public void TestFiniteStateAutomatonCOMMENT()
        {
            LexicalAnalysis lexicalAnalysis = new LexicalAnalysis();
            string path = Path.Combine(Environment.CurrentDirectory, "../../../CodeAnalyzer/FiniteStateAutomatons/COMMENT.txt");
            lexicalAnalysis.LoadFiniteStateAutomatons(new string[] { path });
            Assert.AreEqual("<\"COMMENT\",// comment>", lexicalAnalysis.Lexer("// comment", true));
            Assert.AreEqual("<\"COMMENT\",//comment\n>", lexicalAnalysis.Lexer("//comment\n", true));
            Assert.AreEqual("<\"COMMENT\",//comment///*/*/\n>", lexicalAnalysis.Lexer("//comment///*/*/\n", true));
            Assert.AreEqual("<\"COMMENT\",/*comment///**/>", lexicalAnalysis.Lexer("/*comment///**/", true));

            Assert.AreEqual("error", lexicalAnalysis.Lexer("// comment\n no comment", true));
            Assert.AreEqual("error", lexicalAnalysis.Lexer("/*comment///*/*/", true));
        }

        [TestMethod]
        public void TestFiniteStateAutomatonID()
        {
            LexicalAnalysis lexicalAnalysis = new LexicalAnalysis();
            string path = Path.Combine(Environment.CurrentDirectory, "../../../CodeAnalyzer/FiniteStateAutomatons/ID.txt");
            lexicalAnalysis.LoadFiniteStateAutomatons(new string[] { path });
            Assert.AreEqual("<\"ID\",test>", lexicalAnalysis.Lexer("test", true));
            Assert.AreEqual("<\"ID\",test1>", lexicalAnalysis.Lexer("test1", true));
            Assert.AreEqual("<\"ID\",_test>", lexicalAnalysis.Lexer("_test", true));
            Assert.AreEqual("<\"ID\",_1test>", lexicalAnalysis.Lexer("_1test", true));

            Assert.AreEqual("error", lexicalAnalysis.Lexer("1test", true));
        }

        [TestMethod]
        public void TestFiniteStateAutomatonINTEGER()
        {
            LexicalAnalysis lexicalAnalysis = new LexicalAnalysis();
            string path = Path.Combine(Environment.CurrentDirectory, "../../../CodeAnalyzer/FiniteStateAutomatons/INTEGER.txt");
            lexicalAnalysis.LoadFiniteStateAutomatons(new string[] { path });
            Assert.AreEqual("<\"INTEGER\",100>", lexicalAnalysis.Lexer("100", true));
            Assert.AreEqual("<\"INTEGER\",-100>", lexicalAnalysis.Lexer("-100", true));

            Assert.AreEqual("error", lexicalAnalysis.Lexer("-10.1", true));
        }

        [TestMethod]
        public void TestFiniteStateAutomatonKW()
        {
            LexicalAnalysis lexicalAnalysis = new LexicalAnalysis();
            string path = Path.Combine(Environment.CurrentDirectory, "../../../CodeAnalyzer/FiniteStateAutomatons/KW.txt");
            lexicalAnalysis.LoadFiniteStateAutomatons(new string[] { path });
            Assert.AreEqual("<\"KW\",while>", lexicalAnalysis.Lexer("while", true));
            Assert.AreEqual("<\"KW\",if>", lexicalAnalysis.Lexer("if", true));
        }

        [TestMethod]
        public void TestFiniteStateAutomatonNL()
        {
            LexicalAnalysis lexicalAnalysis = new LexicalAnalysis();
            string path = Path.Combine(Environment.CurrentDirectory, "../../../CodeAnalyzer/FiniteStateAutomatons/NL.txt");
            lexicalAnalysis.LoadFiniteStateAutomatons(new string[] { path });
            Assert.AreEqual("<\"NL\",line feed>\n", lexicalAnalysis.Lexer("\n", true));
            Assert.AreEqual("<\"NL\",next line>\u0085", lexicalAnalysis.Lexer("\u0085", true));
        }

        [TestMethod]
        public void TestFiniteStateAutomatonOP()
        {
            LexicalAnalysis lexicalAnalysis = new LexicalAnalysis();
            string path = Path.Combine(Environment.CurrentDirectory, "../../../CodeAnalyzer/FiniteStateAutomatons/OP.txt");
            lexicalAnalysis.LoadFiniteStateAutomatons(new string[] { path });
            Assert.AreEqual("<\"OP\",{>", lexicalAnalysis.Lexer("{", true));
            Assert.AreEqual("<\"OP\",==>", lexicalAnalysis.Lexer("==", true));
            Assert.AreEqual("<\"OP\",>>>", lexicalAnalysis.Lexer(">>", true));
        }

        [TestMethod]
        public void TestFiniteStateAutomatonREAL()
        {
            LexicalAnalysis lexicalAnalysis = new LexicalAnalysis();
            string path = Path.Combine(Environment.CurrentDirectory, "../../../CodeAnalyzer/FiniteStateAutomatons/REAL.txt");
            lexicalAnalysis.LoadFiniteStateAutomatons(new string[] { path });
            Assert.AreEqual("<\"REAL\",.100>", lexicalAnalysis.Lexer(".100", true));
            Assert.AreEqual("<\"REAL\",-.100>", lexicalAnalysis.Lexer("-.100", true));
            Assert.AreEqual("<\"REAL\",-10.100>", lexicalAnalysis.Lexer("-10.100", true));

            Assert.AreEqual("error", lexicalAnalysis.Lexer("-10,1", true));
        }

        [TestMethod]
        public void TestFiniteStateAutomatonSTRING()
        {
            LexicalAnalysis lexicalAnalysis = new LexicalAnalysis();
            string path = Path.Combine(Environment.CurrentDirectory, "../../../CodeAnalyzer/FiniteStateAutomatons/STRING.txt");
            lexicalAnalysis.LoadFiniteStateAutomatons(new string[] { path });
            Assert.AreEqual("<\"STRING\",\"abc\">", lexicalAnalysis.Lexer("\"abc\"", true));
            Assert.AreEqual("<\"STRING\",\"\">", lexicalAnalysis.Lexer("\"\"", true));
            Assert.AreEqual("<\"STRING\",@\"абв\">", lexicalAnalysis.Lexer("@\"абв\"", true));

            Assert.AreEqual("error", lexicalAnalysis.Lexer("\"123", true));
        }

        [TestMethod]
        public void TestFiniteStateAutomatonWS()
        {
            LexicalAnalysis lexicalAnalysis = new LexicalAnalysis();
            string path = Path.Combine(Environment.CurrentDirectory, "../../../CodeAnalyzer/FiniteStateAutomatons/WS.txt");
            lexicalAnalysis.LoadFiniteStateAutomatons(new string[] { path });
            Assert.AreEqual("<\"WS\",spase>", lexicalAnalysis.Lexer(" ", true));
            Assert.AreEqual("<\"WS\",horizontal tab>", lexicalAnalysis.Lexer("\t", true));
            Assert.AreEqual("<\"WS\",vertical tab>", lexicalAnalysis.Lexer("\v", true));
        }

        [TestMethod]
        public void TestFiniteStateAutomatons()
        {
            LexicalAnalysis lexicalAnalysis = new LexicalAnalysis();
            LoadFiniteStateAutomatons(ref lexicalAnalysis);
            Directory.GetFiles(Path.Combine(Environment.CurrentDirectory, "..", "..", "TestingData", "Source")).ToList().ForEach((f) =>
            {
                string source, resultLexicalAnalysis, result;
                using (FileStream stream = new FileStream(f, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        source = reader.ReadToEnd();
                    }
                }
                resultLexicalAnalysis = lexicalAnalysis.Lexer(source);

                string resultPath = Path.Combine(f, "..", "..", "Result", f.Substring(f.LastIndexOf("\\") + 1));
                if (File.Exists(resultPath))
                {
                    using (FileStream stream = new FileStream(resultPath, FileMode.Open))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                    Assert.AreEqual(result, resultLexicalAnalysis);
                }
                else
                {
                    using (FileStream stream = new FileStream(resultPath, FileMode.Create))
                    {
                        using (StreamWriter writer = new StreamWriter(stream))
                        {
                            writer.Write(resultLexicalAnalysis);
                        }
                    }
                    System.Diagnostics.Process.Start(resultPath);
                }
            });
        }
    }
}
