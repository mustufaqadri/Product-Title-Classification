using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Accord.MachineLearning.Bayes;
using Accord.Math;
using Accord.Statistics.Distributions.Fitting;
using Accord.Statistics.Distributions.Univariate;
using System.Data;
using CsvHelper;
using Accord.Statistics.Filters;

namespace Product_Classification
{
    class NaiveBayesClassifier 
    {

        public NaiveBayes<NormalDistribution> BayesianModel { get; private set; }
        public double Accuracy;
        public String Result = "";
        public List<int> Results;

        public NaiveBayesClassifier()
        {
            
        }
        
        public void TrainClassifier(ClassificationData TrainingData)
        {
            var Learner = new NaiveBayesLearning();
            NaiveBayes NB = Learner.Learn(TrainingData.InputData,TrainingData.OutputData);
            //String[] A= { "AP564ELASSTWANMY", "Apple MacBook Pro MGXC2ZP / A 16GB i7 15.4 - inch Retina Display Laptop", "12550", "local"};
            String[] A = { "AD674FAASTLXANMY", "Adana Gallery Suri Square Hijab â€“ Light Pink", "49", "local" };
            //Fashion

            int[] instance = TrainingData.CodeBook.Transform(A);
            //double[] x = NB.Score(TrainingData.InputData);
            int c = NB.Decide(instance); // answer will be 0
            // Now let us convert the numeric output to an actual "Yes" or "No" answer
            Result = TrainingData.CodeBook.Revert("Category", c);
            //Result = x[1].ToString();
        }/*
        public void TrainClassifier(ClassificationData TrainingData)
        {
            // Create a new Naive Bayes classifier.
            BayesianModel = new NaiveBayes<NormalDistribution>(9,4,NormalDistribution.Standard);
            
            // Compute the Naive Bayes model.
            Accuracy = 100-BayesianModel.Estimate(TrainingData.InputData,TrainingData.OutputData,true,new NormalOptions { Regularization = 1e-5 });
        }
        public int[] TestClassifier(ClassificationData TestingData)
        {
            Results = new List<int>();
            // Predict the results for a series of inputs.
            foreach (double[] input in TestingData.InputData)
            {
                Results.Add(BayesianModel.Compute(input));
            }
            return Results.ToArray();
        }
        
        public void ComputeResult(ClassificationData TestingData)
        {
            // Predict the result for a single input.
            int result = BayesianModel.Compute(TestingData.InputData);
            return result;
        }*/
    }
}
