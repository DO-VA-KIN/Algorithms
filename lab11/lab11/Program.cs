using System.Xml.Serialization;
using System;

namespace lab11
{
    public class Program
    {
        static void Main()
        {
            string fileName = Environment.CurrentDirectory + @"\config.xml";
            Config? config = new();

            if (File.Exists(fileName))
            {
                try
                {
                    XmlSerializer xmlSerializer = new(typeof(Config));
                    using (FileStream fs = new(fileName, FileMode.Open))
                    { config = xmlSerializer.Deserialize(fs) as Config; }
                }
                catch { File.Delete(fileName); }
            }
            else
            {
                try
                {
                    XmlSerializer xmlSerializer = new(typeof(Config));
                    using (FileStream fs = new("config.xml", FileMode.OpenOrCreate))
                    { xmlSerializer.Serialize(fs, config); }
                }
                catch {};
            }


            Generator gen = new();
            char[,] maze = gen.Generate(45, 5, true, true);

            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                    Console.Write(maze[i, j]);
                Console.Write("\n");
            }
            Console.ReadKey();
        }


        [Serializable]
        public class Config
        {
            public int Size = 45;
            public short Width = 5;
            public bool Galery = true;
            public bool Squares = true;
        }          
    }
}