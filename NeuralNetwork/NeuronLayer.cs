using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NeuronLayer
{
    [SerializeField]
    public Neuron[] _neurons;
    [SerializeField]
    private int _weights_per_neuron;
    [SerializeField]
    private float[] _outputs;

    public NeuronLayer(int neurons_nb, int weights_per_neuron, Neuron.ActivationFunction activation_function)
    {
        _outputs = new float[neurons_nb];
        _neurons = new Neuron[neurons_nb];

        for (int i = 0; i < neurons_nb; i++)
        {
            _neurons[i] =  new Neuron(weights_per_neuron, activation_function);
        }
        _weights_per_neuron = weights_per_neuron;
    }

    public int nbWeights
    {
        get
        {
            return _neurons.Length * _weights_per_neuron;
        }
    }

    public int Weights_per_neuron
    {
        get
        {
            return _weights_per_neuron;
        }
    }

    public float[] Weights
    {
        get
        {
            float[] ans = new float[_weights_per_neuron * _neurons.Length];
            int i = 0;
            float[] weights_temp;

            foreach (Neuron n in _neurons)
            {
                weights_temp = n.getWeights();

                for (int j = 0; j < weights_temp.Length; j++)
                {
                    ans[i] = weights_temp[j];
                    i++;
                }
            }
            return ans;
        }
        set
        {
            int i = 0;
            float[] weights_temp;

            foreach (Neuron n in _neurons)
            {
                weights_temp = new float[_weights_per_neuron];

                for (int j = 0; j < weights_temp.Length; j++)
                {
                    weights_temp[j] = value[i];
                    i++;
                }
                n.setWeights(weights_temp);
            }
        }
    }

    public float[] getOutputs(float[] inputs)
    {

        _outputs = new float[_neurons.Length];
        int i = 0;

        

        foreach (Neuron n in _neurons)
        {

            if (_weights_per_neuron - 1 == 1)
            {
                _outputs[i] = n.getOutput(new float[] { inputs[i]});
            }
            else
            {
                _outputs[i] = n.getOutput(inputs);

            }

            i++;
        }
        return _outputs;
    }

}
