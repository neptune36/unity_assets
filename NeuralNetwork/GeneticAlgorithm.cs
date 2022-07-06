using System;
using UnityEngine;

public class GeneticAlgorithm : MonoBehaviour
{

    public static System.Random rdm = new System.Random();

    public int generation_nb;
    public int population_size;

    public float rank_rate;
    public float crossover_rate;
    public float mutation_rate;

    public bool elitism;


    public static string FloatToBinary(float value)
    {
        string s = value.ToString();
        string[] t = s.Split('.');

        string v = t.Length > 1 ?  t[1]: t[0];

        int i = int.Parse(v);
        string result = Convert.ToString(i, 2);

        while (result.Length < 10)
        {
            result = "0" + result;
        }

        return result;

    }

    public static float BinaryToFloat(string binary)
    {
        int i = Convert.ToInt32(binary, 2);
        string s = i.ToString();
        while (s.Length < 3)
        {
            s = "0" + s;
        }
        s = "0." + s;
        float r = float.Parse(s);
        return r;
    }

    public void LaunchTraining()
    {
        int data_row_nb = population_size * generation_nb + 1;
        string[,] data = new string[data_row_nb, 4];

        data[0, 0] = "Generation";
        data[0, 1] = "Id joueur";
        data[0, 2] = "Score";
        data[0, 3] = "Brain";


        HardIAPlayer[] population = new HardIAPlayer[population_size];
        for (int i = 0; i < population_size; i++)
        {
            population[i] = new HardIAPlayer();
        }

        for (int generation = 0; generation < generation_nb; generation++)
        {

            population = PlayPopulation(population);

            for (int player = 0; player < population.Length; player++)
            {
                int index = population.Length * generation + player + 1;

                data[index, 0] = generation.ToString();
                data[index, 1] = player.ToString();
                data[index, 2] = population[player].Score.ToString();
                data[index, 3] = population[player].Brain.getGene();

            }
            //DateTime start_gen = DateTime.Now;
            population = GetNextGeneration(population);
            //DateTime end_gen = DateTime.Now;

        }
        string root_path = Application.persistentDataPath + "/";
        string filename = root_path + "Training.csv";
        Utils.TableToCSV(data, filename);
        UnityEngine.Debug.Log("end training");
    }

    private HardIAPlayer[] PlayPopulation(HardIAPlayer[] population)
    {

        for (int i = 0; i < population.Length; i++)
        {
            Game game_test = new Game(new Vector2(3, 3), Enums.GameMode.IA_vs_IA, Enums.Difficulty.HARD, Enums.Difficulty.MEDIUM);
            game_test.Players[0] = population[i];
            population[i].Game = game_test;
            population[i].Score = 0;

            game_test.Start();

            while (!game_test.Completed)
            {
                game_test.Active_player.play();
            }
        }
        return population;
    }

    private HardIAPlayer[] GetNextGeneration(HardIAPlayer[] population)
    {
        HardIAPlayer[] new_population = new HardIAPlayer[population.Length];

        int child_index = 0;

        if (elitism)
        {
            new_population[0] = GetBest(population);
            child_index = 1;
        }

        for (int i = child_index; i < population.Length; i++)
        {
            HardIAPlayer father = RankSelection(population);
            HardIAPlayer mother =  RankSelection(population);

            HardIAPlayer child = MakeChild(father, mother);

            if (child == null)
            {
                child = father;
            }

            child = Mutate(child);
            new_population[i] = child;
        }

        return new_population;
    }

    private HardIAPlayer RankSelection(HardIAPlayer[] population)
    {
        Array.Sort(population);
        int selected = rdm.Next(0, (int)(rank_rate * population_size));

        return population[selected];
    }

    private HardIAPlayer GetBest(HardIAPlayer[] population)
    {
        Array.Sort(population);
        return population[0];
    }

    private HardIAPlayer MakeChild(HardIAPlayer father, HardIAPlayer mother)
    {
        if (rdm.NextDouble() > crossover_rate) return null;

        NeuralNetwork brain = new NeuralNetwork(father.Brain.Inputs, father.Brain.Outputs, father.Brain.Hidden_layer, father.Brain.Neurons_per_hidden_layer,father.Brain.Input_weight_size, father.Brain.Activation);

        int crosspoint = rdm.Next(0, father.Brain.getGene().Length);

        string new_gene = father.Brain.getGene().Substring(0, crosspoint);
        new_gene += mother.Brain.getGene().Substring(crosspoint);

        int index = 0, sequence = 0;
        float[] new_weights = new float[father.Brain.WeightCount];

        while (index < new_gene.Length)
        {
            new_weights[sequence] = BinaryToFloat(new_gene.Substring(index, 10));

            sequence++;
            index += 10;
        }

        brain.Weights = new_weights;
        HardIAPlayer child = new HardIAPlayer();
        child.Brain = brain;
        return child;
    }

    private HardIAPlayer Mutate(HardIAPlayer player)
    {
        for (int weight = 0; weight < player.Brain.WeightCount; weight++)
        {
            if (rdm.NextDouble() < mutation_rate)
            {
                player.Brain.Weights[weight] += (float)rdm.NextDouble();
            }
        }

        return player;
    }

}
