using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Product_Classification
{
    public partial class Form1 : Form
    {
        ClassificationData Data = new ClassificationData();
        ClassificationData TestData = new ClassificationData();
        NaiveBayesClassifier NBClassifer = new NaiveBayesClassifier();
        public Form1()
        {
            InitializeComponent();
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("Please wait for 50 seconds", "Working", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
            Data.InputTrainingData();
            //TestData.InputTestingData(Data);
            for (int i=0;i<100;i++)
            {
                dataGridView1.Rows.Add(Data.Array1[i], Data.Array2[i], Data.Array3[i], Data.Array4[i], Data.Array5[i]);
            }
            MessageBox.Show("Data Loaded Successfully", "Succeeded", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NBClassifer.TrainClassifier(Data);
            richTextBox1.Text = Data.Result;
            MessageBox.Show("Classifer is trained successfully", "Succeeded", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            /*//int[] PredictedValues=NBClassifer.TestClassifier(TestData);
            String CorrectPredictions = "";
            String IncorrectPredictions = "";
            double CorrectPrediction = 0;
            double IncorrectPrediction = 0;
            for(int i=0;i<PredictedValues.Length;i++)
            {
                if (PredictedValues[i]!=TestData.OutputData[i])
                {
                    CorrectPrediction++;
                    CorrectPredictions += PredictedValues[i] + " = " + TestData.OutputData[i] + "\n";
                }
                else
                {
                    IncorrectPrediction++;
                    IncorrectPredictions += PredictedValues[i] + " = " + TestData.OutputData[i] + "\n";
                }
            }
            String Final = "";
            Final += "Correctly Predicted Values:\n\n"+CorrectPredictions+ "Incorrectly Predicted Values:\n\n"+IncorrectPredictions;
            File.WriteAllText("Result.txt",Final);
            double Accuracy = (CorrectPrediction / (CorrectPrediction+IncorrectPrediction) ) * 100;
            Accuracy = Math.Round(Accuracy, 2);
            richTextBox1.Text = "Accuracy : "+Accuracy+"%"+"\n\n"+"Correct Predictons : "+CorrectPrediction+"\nIncorrect Predictions : "+IncorrectPrediction;*/
        }
    }
}