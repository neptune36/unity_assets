using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NeuralNetwork {

    [SerializeField]
    private List<NeuronLayer> _layers;
    [SerializeField]
    private NeuronLayer _input_layer;
    [SerializeField]
    private NeuronLayer _output_layer;
    [SerializeField]
    private int inputs;
    [SerializeField]
    private int outputs;
    [SerializeField]
    Neuron.ActivationFunction activation;
    [SerializeField]
    private int hidden_layer;
    [SerializeField]
    private int neurons_per_hidden_layer;
    [SerializeField]
    int input_weight_size;

    public NeuralNetwork(int inputs,int outputs, int hidden_layer, int neurons_per_hidden_layer, int input_weight_size, Neuron.ActivationFunction activation)
    {
        this.inputs = inputs;
        this.outputs = outputs;
        this.hidden_layer = hidden_layer;
        this.neurons_per_hidden_layer = neurons_per_hidden_layer;
        this.input_weight_size = input_weight_size;

        _input_layer = new NeuronLayer(inputs, input_weight_size + 1, activation);
        _layers = new List<NeuronLayer>();
        int last_layer_size = inputs;

        for(int i = 0; i < hidden_layer; i++)
        {
            _layers.Add(new NeuronLayer(neurons_per_hidden_layer, last_layer_size + 1, activation));
            last_layer_size = neurons_per_hidden_layer;
        }
        _output_layer = new NeuronLayer(outputs, last_layer_size + 1, activation);
    }

    public string getGene()
    {
        string gene = "";
        foreach(float weight in Weights)
        {
            gene += GeneticAlgorithm.FloatToBinary(weight);
        }

        return gene;
    }

    public int WeightCount
    {
        get
        {
            int cnt = _input_layer.nbWeights;

            foreach(NeuronLayer nl in _layers)
            {
                cnt += nl.nbWeights;
            }

            cnt += _output_layer.nbWeights;

            return cnt;
        }
    }

    public float[] getOutputs(float[] inputs)
    {
        float[] ans = _input_layer.getOutputs(inputs);
        foreach(NeuronLayer nl in _layers)
        {
            ans = nl.getOutputs(ans);
        }
        ans = _output_layer.getOutputs(ans);
        return ans;
    }

    public float[] Weights
    {
        get
        {
            float[] ans = new float[WeightCount];
            float[] layer_weights = _input_layer.Weights;
            int j = 0;

            for(int i=0;i<layer_weights.Length; i++)
            {
                ans[j] = layer_weights[i];
                j++;
            }

            foreach(NeuronLayer nl in _layers)
            {
                layer_weights = nl.Weights;

                for(int i = 0; i < layer_weights.Length; i++)
                {
                    ans[j] = layer_weights[i];
                    j++;
                }
            }

            layer_weights = _output_layer.Weights;

            for(int i = 0; i < layer_weights.Length; i++)
            {
                ans[j] = layer_weights[i];
                j++;
            }

            return ans;
        }
        set
        {
            float[] layer_weights;
            int i,j = 0;

            for (i = 0; i < value.Length; i++)
            {

                layer_weights = new float[_input_layer.nbWeights];

                for (j = 0; i < layer_weights.Length; j++)
                {
                    layer_weights[j] = value[i];
                    i++;
                }

                _input_layer.Weights = layer_weights;

                foreach(NeuronLayer nl in _layers)
                {
                    layer_weights = new float[nl.nbWeights];
                    for (j = 0; j < layer_weights.Length; j++)
                    {
                        layer_weights[j] = value[i];
                        i++;
                    }
                    nl.Weights = layer_weights;
                }
                layer_weights = new float[_output_layer.nbWeights];
                for (j = 0; j < layer_weights.Length; j++)
                {
                    layer_weights[j] = value[i];
                    i++;
                }
                _output_layer.Weights = layer_weights;
            }
        }
    }

    public NeuronLayer Input_layer
    {
        get
        {
            return _input_layer;
        }
    }

    public NeuronLayer Output_layer
    {
        get
        {
            return _output_layer;
        }
    }

    public int Inputs
    {
        get
        {
            return inputs;
        }

        set
        {
            inputs = value;
        }
    }

    public int Outputs
    {
        get
        {
            return outputs;
        }

        set
        {
            outputs = value;
        }
    }

    public int Neurons_per_hidden_layer
    {
        get
        {
            return neurons_per_hidden_layer;
        }

        set
        {
            neurons_per_hidden_layer = value;
        }
    }

    public int Hidden_layer
    {
        get
        {
            return hidden_layer;
        }

        set
        {
            hidden_layer = value;
        }
    }

    public Neuron.ActivationFunction Activation
    {
        get
        {
            return activation;
        }

        set
        {
            activation = value;
        }
    }

    public int Input_weight_size
    {
        get
        {
            return input_weight_size;
        }

        set
        {
            input_weight_size = value;
        }
    }
}
