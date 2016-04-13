using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class Perlin
    {
        List<List<double>> PerlinNoise_;

        public Perlin(int width, int height, int octaveCount)
        {
            List<List<double>> baseNoise = GenerateWhiteNoise(width, height);

            PerlinNoise_ = GeneratePerlinNoise(baseNoise, octaveCount);
        }

        public List<List<double>> GenerateWhiteNoise(int width, int height)
        {
            List<List<double>> tempNoise = new List<List<double>>();

            Random r = new Random();

            for (int i = 0; i < height; i++)
            {
                List<double> tempRow = new List<double>();
                for (int j = 0; j < width; j++)
                {
                    tempRow.Add(r.NextDouble());
                }
                tempNoise.Add(tempRow);
            }

            return tempNoise;
        }

        public double Interpolate(double x0, double x1, double alpha)
        {
            return x0 * (1 - alpha) + alpha * x1;
        }

        public List<List<double>> GenerateSmoothNoise(List<List<double>> baseNoise, int octave)
        {
            int width = baseNoise[0].Count;
            int height = baseNoise.Count;

            List<List<double>> smoothNoise = new List<List<double>>();     

            int samplePeriod = (int)Math.Pow(2.0, octave);
            double sampleFrequency = 1.0f / samplePeriod;

            for (int i = 0; i < height; i++)
            {
                int sample_i0 = (i / samplePeriod) * samplePeriod;
                int sample_i1 = (sample_i0 + samplePeriod) % width;
                double horizontal_blend = (i - sample_i0) * sampleFrequency;
                List<double> tempRow = new List<double>();

                for (int j = 0; j < width; j++)
                {
                    int sample_j0 = (j / samplePeriod) * samplePeriod;
                    int sample_j1 = (sample_j0 + samplePeriod) % height;
                    double verticle_blend = (j - sample_j0) * sampleFrequency;

                    double top = Interpolate(baseNoise[sample_i0][sample_j0],
                                            baseNoise[sample_i1][sample_j0], horizontal_blend);

                    double bottom = Interpolate(baseNoise[sample_i0][sample_j1],
                                               baseNoise[sample_i1][sample_j1], horizontal_blend);

                    tempRow.Add(Interpolate(top, bottom, verticle_blend));
                }
                smoothNoise.Add(tempRow);
            }

            return smoothNoise;
        }

        public List<List<double>> GeneratePerlinNoise(List<List<double>> baseNoise, int octaveCount)
        {
            int width = baseNoise[0].Count;
            int height = baseNoise.Count;

            double persistance = 0.3f;

            List<List<List<double>>> smoothNoise = new List<List<List<double>>>();  

            for (int i = 0; i < octaveCount; i++)
            {
                smoothNoise.Add(GenerateSmoothNoise(baseNoise, i));
            }

            List<List<double>> perlinNoise = new List<List<double>>();

            double amplitude = 1.0f;
            double totalAmplitude = 0.0f;

            for (int i = 0; i < height; i++)
            {
                List<double> tempRow = new List<double>();
                for (int j = 0; j < width; j++)
                {
                    tempRow.Add(0);
                }
                perlinNoise.Add(tempRow);
            }

            for (int octave = octaveCount - 1; octave >= 0; octave--)
            {
                amplitude *= persistance;
                totalAmplitude += amplitude;

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        perlinNoise[i][j] += smoothNoise[octave][i][j] * amplitude;
                    }
                }
            }

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    perlinNoise[i][j] /= totalAmplitude;
                }
            }

            return perlinNoise;
        }

        public List<List<double>> GetPerlinNoise()
        {
            return PerlinNoise_;
        }

    }
}
