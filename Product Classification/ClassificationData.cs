using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Data;
using CsvHelper;
using Accord.Math;
using Accord.Statistics.Filters;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Accord.MachineLearning.Bayes;
namespace Product_Classification
{
    class ClassificationData
    {
        public String Result = "";
        public String[] Array1;
        public String[] Array2;
        public String[] Array3;
        public String[] Array4;
        public String[] Array5;
        public DataTable ExtractedData;
        public DataTable Symbols;
        public int[][] InputData;
        public int[] OutputData;
        public Codification CodeBook;
        public ClassificationData()
        {
            Array1 = new String[36283];
            Array2 = new String[36283];
            Array3 = new String[36283];
            Array4 = new String[36283];
            Array5 = new String[36283];
            ExtractedData = new DataTable();
        }
        public void ReadTrainingData()
        {
            ExtractedData.Columns.Add("Product Code", typeof(string));
            ExtractedData.Columns.Add("Product Title", typeof(string));
            ExtractedData.Columns.Add("Price", typeof(string));
            ExtractedData.Columns.Add("Warranty Type", typeof(string));
            ExtractedData.Columns.Add("Category", typeof(string));


            using (var reader = new StreamReader(@"TrainView.csv"))
            {
                int Count = 0;
                //while (!reader.EndOfStream)
                while (Count < 100)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    ExtractedData.Rows.Add(values[0],values[1],values[2],values[3],values[4]);
                    Array1[Count] = values[0];
                    Array2[Count] = values[1];
                    Array3[Count] = values[2];
                    Array4[Count] = values[3];
                    Array5[Count] = values[4];
                    Count++;
                }
            }

            /*
            for (int i = 0; i < 100; i++)
            {
                ExtractedData.Rows.Add(Array1[i], Array2[i], Array3[i], Array4[i], Array5[i]);

            }*/
            //WriteArrays();
        }
        public void ReadTestingData()
        {
            using (var reader = new StreamReader(@"TestView.csv"))
            {
                int Count = 0;
                //while (!reader.EndOfStream)
                while (Count < 11838)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    //ExtractedDataset.Rows.Add(values[0],values[1],values[2],values[3],values[4]);
                    Array1[Count] = values[0];
                    Array2[Count] = values[1];
                    Array3[Count] = values[2];
                    Array4[Count] = values[3];
                    Array5[Count] = values[4];
                    Count++;
                }
            }
            ExtractedData.Columns.Add("Product Code", typeof(string));
            ExtractedData.Columns.Add("Product Title", typeof(string));
            ExtractedData.Columns.Add("Price", typeof(string));
            ExtractedData.Columns.Add("Warranty Type", typeof(string));
            ExtractedData.Columns.Add("Category", typeof(string));

            for (int i = 0; i < 11838; i++)
            {
                ExtractedData.Rows.Add(Array1[i], Array2[i], Array3[i], Array4[i], Array5[i]);
            }
            //WriteTestArrays();
        }
        public void InputTrainingData()
        {
            ReadTrainingData();
            /*
            foreach (DataRow dataRow in ExtractedData.Rows)
            {
                foreach (var item in dataRow.ItemArray)
                {
                    Result += item + "\n";
                }
            }*/

            //ReadingArrays();
            String[] C = { "Product Code", "Product Title", "Price", "Warranty Type", "Category" };
            CodeBook = new Codification(ExtractedData,C);

            // Extract input and output pairs to train

            Symbols = CodeBook.Apply(ExtractedData);
            int[][] Inputs = Symbols.ToJagged<int>("Product Code","Product Title", "Price","Warranty Type");
            int[] Outputs = Symbols.ToArray<int>("Category");
            
            InputData = Inputs;
            OutputData = Outputs;

            var Learner = new NaiveBayesLearning();
            NaiveBayes NB = Learner.Learn(InputData, OutputData);
            //String[] A= { "AP564ELASSTWANMY", "Apple MacBook Pro MGXC2ZP / A 16GB i7 15.4 - inch Retina Display Laptop", "12550", "local"};
            String[] A = { "AD674FAASTLXANMY", "Adana Gallery Suri Square Hijab â€“ Light Pink", "49", "local" };
            //Fashion

            int[] instance = CodeBook.Transform(A);
            //double[] x = NB.Score(TrainingData.InputData);
            int c = NB.Decide(instance); // answer will be 0
            // Now let us convert the numeric output to an actual "Yes" or "No" answer
            Result = CodeBook.Revert("Category", c);
        }
        /*
        public void InputTestingData(ClassificationData TrainData)
        {
            //ReadTestingData();
            ReadingTestArrays();
            Symbols = TrainData.CodeBook.Apply(ExtractedData);
            double[][] Inputs = Symbols.ToArray<double>("Product Code", "Product Title", "Price", "Warranty Type");
            int[] Outputs = Symbols.ToArray<int>("Category");

            InputData = Inputs;
            OutputData = Outputs;
        }*/
        public void WriteArrays()
        {
            BinaryFormatter formatter;
            formatter = new BinaryFormatter();
            try
            {
                FileStream writerFileStream = new FileStream("Arrays.dat", FileMode.Create, FileAccess.Write);
                formatter.Serialize(writerFileStream, Array1);
                formatter.Serialize(writerFileStream, Array2);
                formatter.Serialize(writerFileStream, Array3);
                formatter.Serialize(writerFileStream, Array4);
                formatter.Serialize(writerFileStream, Array5);
                formatter.Serialize(writerFileStream, ExtractedData);
                writerFileStream.Close();
            }
            catch (Exception)
            {
                String Temp = "Unable";
            }
        }/*
        public void WriteTestArrays()
        {
            BinaryFormatter formatter;
            formatter = new BinaryFormatter();
            try
            {
                FileStream writerFileStream = new FileStream("TestArrays.dat", FileMode.Create, FileAccess.Write);
                formatter.Serialize(writerFileStream, Array1);
                formatter.Serialize(writerFileStream, Array2);
                formatter.Serialize(writerFileStream, Array3);
                formatter.Serialize(writerFileStream, Array4);
                formatter.Serialize(writerFileStream, Array5);
                formatter.Serialize(writerFileStream, ExtractedData);
                writerFileStream.Close();
            }
            catch (Exception)
            {
                String Temp = "Unable";
            }
        }*/
        public void ReadingArrays()
        {
            if (File.Exists("Arrays.dat"))
            {
                BinaryFormatter formatter;
                formatter = new BinaryFormatter();
                try
                {
                    FileStream readerFileStream = new FileStream("Arrays.dat", FileMode.Open, FileAccess.Read);
                    Array1 = (String[])formatter.Deserialize(readerFileStream);
                    Array2 = (String[])formatter.Deserialize(readerFileStream);
                    Array3 = (String[])formatter.Deserialize(readerFileStream);
                    Array4 = (String[])formatter.Deserialize(readerFileStream);
                    Array5 = (String[])formatter.Deserialize(readerFileStream);
                    ExtractedData = (DataTable)formatter.Deserialize(readerFileStream);
                    readerFileStream.Close();
                }
                catch (Exception)
                {
                    String Temp = "Unable";
                }
            }
        }/*
        public void ReadingTestArrays()
        {
            if (File.Exists("TestArrays.dat"))
            {
                BinaryFormatter formatter;
                formatter = new BinaryFormatter();
                try
                {
                    FileStream readerFileStream = new FileStream("TestArrays.dat", FileMode.Open, FileAccess.Read);
                    Array1 = (String[])formatter.Deserialize(readerFileStream);
                    Array2 = (String[])formatter.Deserialize(readerFileStream);
                    Array3 = (String[])formatter.Deserialize(readerFileStream);
                    Array4 = (String[])formatter.Deserialize(readerFileStream);
                    Array5 = (String[])formatter.Deserialize(readerFileStream);
                    ExtractedData = (DataTable)formatter.Deserialize(readerFileStream);
                    readerFileStream.Close();
                }
                catch (Exception)
                {
                    String Temp = "Unable";
                }
            }
        }*/
    }
}