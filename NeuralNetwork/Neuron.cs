using System;
using UnityEngine;

[System.Serializable]
public class Neuron
{

    [System.Serializable]
    public enum ActivationFunction
    {
        AFStep = 0,
        AFSigmoid = 1,
    }

    //private float _output;
    [SerializeField]
    private float[] _weights;
    [SerializeField]
    private ActivationFunction _activation;

    public ActivationFunction Activation
    {
        get
        {
            return _activation;
        }

        set
        {
            _activation = value;
        }
    }

    public float[] getWeights()
    {
        return _weights;
    }

    public void setWeights(float[] weights)
    {
        _weights = weights;
    }

    public float getOutput(float[] inputs)
    {
        float total = 0f;
        float threshold;
        int i = 0;

        for (i = 0; i < inputs.Length; i++)
        {
            total += inputs[i] * _weights[i];
        }

        threshold = _weights[i] * -1;

        if (threshold == 0)
            Debug.Log(threshold);

        if (_activation == ActivationFunction.AFStep)
        {
            return threshold < total ? 1 : 0;
        }
        else
        {
            return 1 / (1 + Mathf.Exp(-total / threshold));
        }
    }

    public Neuron(int nb_weights, ActivationFunction activation)
    {
        _weights = new float[nb_weights];
        for (int i = 0; i < _weights.Length; i++)
        {
            float f = 0f;

            do
            {
                f = (float)GeneticAlgorithm.rdm.NextDouble();
                f = (float)Math.Round(f, 3);
            } while (f == 0);
            _weights[i] = (float)Math.Round(f, 3);
        }
        _activation = activation;
    }



}
