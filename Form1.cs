using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms.DataVisualization.Charting;



namespace Mission4_Atari_BreakOut
{
    public partial class Form1 : Form
    {

        #region ======================================================= Parameters of the interface ======================================
        //a essayer : donner en entree que la moyenne des couleurs par carre de pixels (jerome a dit la moyenne des couleurs par dessin carrement ??)
        //ya un pb dans ma reduction de dimension


        readonly int dim = 300;//dimentions of the interface
        private float[,] CurrentDrawing;
        private float[,] CropDrawing;
        private List<Tuple<float[,], string>> trainingDrawings = new List<Tuple<float[,], string>>();
        private List<Tuple<float[,], string>> validationDrawings = new List<Tuple<float[,], string>>();
        private bool isDrawing;
        private Point previousLocation;
        readonly int reductionPrecision = 10;
        readonly int epaisseur = 15;
        Random random = new Random();

        int xMin, xMax, yMin, yMax;

        //parameters neural network
        int numInputs;
        int numOutputs;  // the percentage of chance for all classes
        List<string> labels = new List<string>();

        readonly bool initializeNN = true;
        float[,] hiddenWeights;
        float[] hiddenBiases;
        float[,] outputWeights;
        float[] outputBiases;

        //parameters training neural network
        float learningRate = 0.1f;
        int numNeurons = 15;

        public Form1()
        {
            InitializeComponent();
            InitializeVariables();
        }

        public void InitializeVariables()
        {
            isDrawing = false;
            previousLocation = Point.Empty;
            CurrentDrawing = new float[dim, dim];
            CropDrawing = new float[dim / reductionPrecision, dim / reductionPrecision];

            xMin = dim;
            xMax = 0;
            yMin = dim;
            yMax = 0;

            numInputs = (int)Math.Pow(dim / reductionPrecision, 2);

            pictureBox1.MouseMove += pictureBox1_MouseMove;
            pictureBox1.MouseDown += pictureBox1_MouseDown;
            pictureBox1.MouseUp += pictureBox1_MouseUp;

            InitializeDataGrid(true);



        }

        public void InitializeDataGrid(bool first = false)
        {
            if (first)
            {
                // Initialisez et configurez le DataGridView
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
                //dataGridView1.ColumnCount = dim / reductionPrecision;
                //dataGridView1.RowCount = dim / reductionPrecision;

                for (int i = 0; i < dim / reductionPrecision; i++)
                {
                    DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
                    column.Width = dataGridView1.Width / (dim / reductionPrecision);
                    dataGridView1.Columns.Add(column);
                }
            }
            else
            {
                dataGridView1.Rows.Clear();
            }
            

            for (int i = 0; i < dim / reductionPrecision; i++)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.Height = dataGridView1.Height / (dim / reductionPrecision);
                dataGridView1.Rows.Add(row);
            }
        }

        public void InitializeNN(bool initialize)
        {
            //parameters neural network
            this.hiddenWeights = new float[numInputs, numNeurons];
            this.hiddenBiases = new float[numNeurons];
            this.outputWeights = new float[numNeurons, numOutputs];
            this.outputBiases = new float[numOutputs];

            string JSONhiddenWeights;
            List<List<float>> hiddenWeightsList;
            string JSONhiddenBiases;
            List<float> hiddenBiasesList;
            string JSONoutputWeights;
            List<List<float>> outputWeightsList;
            string JSONoutputBiases;
            List<float> outputBiasesList;

            if (initialize)
            {
                // we assign random values
                for (int i = 0; i < numInputs; i++)
                {
                    for (int j = 0; j < numNeurons; j++)
                    {
                        //hiddenWeights[i, j] = (float)random.NextDouble() * 2 - 1; 
                        //initialiser pas au pif            
                        hiddenWeights[i, j] = (float)(2 * (random.NextDouble() < 0.5 ? -1 : random.NextDouble()) * random.NextDouble() * Math.Sqrt(1.0 / numNeurons));
                        //hiddenBiases[j] = (float)random.NextDouble() * 2 - 1;
                        hiddenBiases[j] = (float)(2 * (random.NextDouble() < 0.5 ? -1 : random.NextDouble()) * random.NextDouble() * Math.Sqrt(1.0 / numNeurons));
                    }
                }
                for (int i = 0; i < numNeurons; i++)
                {
                    for (int j = 0; j < numOutputs; j++)
                    {
                        //outputWeights[i, j] = (float)random.NextDouble() * 2 - 1;
                        outputWeights[i, j] = (float)(2 * (random.NextDouble() < 0.5 ? -1 : random.NextDouble()) * random.NextDouble() * Math.Sqrt(1.0 / numNeurons));
                        //outputBiases[j] = (float)random.NextDouble() * 2 - 1;
                        outputBiases[j] = (float)(2 * (random.NextDouble() < 0.5 ? -1 : random.NextDouble()) * random.NextDouble() * Math.Sqrt(1.0 / numNeurons));
                    }
                }
            }
            else
            {
                JSONhiddenWeights = File.ReadAllText("NN/hiddenWeights.json");
                hiddenWeightsList = JsonSerializer.Deserialize<List<List<float>>>(JSONhiddenWeights);
                for (int i = 0; i < numInputs; i++)
                {
                    for (int j = 0; j < numNeurons; j++)
                    {
                        hiddenWeights[i, j] = hiddenWeightsList[i][j];
                    }
                }

                JSONhiddenBiases = File.ReadAllText("NN/hiddenBiases.json");
                hiddenBiasesList = JsonSerializer.Deserialize<List<float>>(JSONhiddenBiases);
                hiddenBiases = hiddenBiasesList.ToArray();

                JSONoutputWeights = File.ReadAllText("NN/outputWeights.json");
                outputWeightsList = JsonSerializer.Deserialize<List<List<float>>>(JSONoutputWeights);
                for (int i = 0; i < numNeurons; i++)
                {
                    for (int j = 0; j < numOutputs; j++)
                    {
                        outputWeights[i, j] = outputWeightsList[i][j];
                    }
                }

                JSONoutputBiases = File.ReadAllText("NN/outputBiases.json");
                outputBiasesList = JsonSerializer.Deserialize<List<float>>(JSONoutputBiases);
                outputBiases = outputBiasesList.ToArray();
            }

            hiddenWeightsList = new List<List<float>>();
            for (int i = 0; i < numInputs; i++)
            {
                List<float> innerList = new List<float>();
                for (int j = 0; j < numNeurons; j++)
                {
                    innerList.Add(hiddenWeights[i, j]);
                }
                hiddenWeightsList.Add(innerList);
            }
            //JSONhiddenWeights = JsonSerializer.Serialize(hiddenWeightsList);
            //File.WriteAllText("NN/hiddenWeights.json", JSONhiddenWeights);

            //hiddenBiasesList = hiddenBiases.ToList();
            //JSONhiddenBiases = JsonSerializer.Serialize(hiddenBiasesList);
            //File.WriteAllText("NN/hiddenBiases.json", JSONhiddenBiases);

            //outputWeightsList = new List<List<float>>();
            //for (int i = 0; i < numNeurons; i++)
            //{
            //    List<float> innerList = new List<float>();
            //    for (int j = 0; j < numOutputs; j++)
            //    {
            //        innerList.Add(outputWeights[i, j]);
            //    }
            //    outputWeightsList.Add(innerList);
            //}
            //JSONoutputWeights = JsonSerializer.Serialize(outputWeightsList);
            //File.WriteAllText("NN/outputWeights.json", JSONoutputWeights);

            //outputBiasesList = outputBiases.ToList();
            //JSONoutputBiases = JsonSerializer.Serialize(outputBiasesList);
            //File.WriteAllText("NN/outputBiases.json", JSONoutputBiases);
        }

        #endregion


        #region ======================================================= Train and predict ================================================

        private void Train()
        {
            numOutputs = labels.Count;
            InitializeNN(initializeNN);

            if (trainingDrawings != null && validationDrawings != null)
            {
                List<string> listMsg = new List<string>();
                //for (int epochs =0;epochs< numEpochs;epochs++)
                int epochs = 0;
                int numEpochs = 1000;
                int nbGoodResponse = 0;
                while (epochs < numEpochs)
                {
                    epochs++;
                    textBox5.Text = "Number of epochs = " + epochs;
                    textBox5.Refresh();
                    //prepare data
                    List<Tuple<float[,], int[]>> data = new List<Tuple<float[,], int[]>>();

                    foreach (Tuple<float[,], string> drawing in trainingDrawings)
                    {
                        int[] output = new int[numOutputs];
                        for (int i = 0; i < labels.Count; i++)
                        {
                            if (labels[i] == drawing.Item2)
                            {
                                output[i] = 1;
                            }
                            else
                            {
                                output[i] = 0;
                            }
                        }
                        data.Add(new Tuple<float[,], int[]>(drawing.Item1, output));
                    }
                    data.Sort((x, y) => random.Next(-1, 2));

                    // train

                    foreach (Tuple<float[,], int[]> drawing in data)
                    {
                        NeuralNetwork(true, drawing.Item1, drawing.Item2);
                        string msg = "(";
                        for (int i = 0; i < numOutputs; i++)
                        {
                            msg += drawing.Item2[i] + ", ";
                        }

                        msg += "), ";
                        listMsg.Add(msg);
                    }

                    for (int i = 0; i < validationDrawings.Count; i++)
                    {
                        if (Prediction(validationDrawings[i].Item1, validationDrawings[i].Item2))
                        {
                            nbGoodResponse++;
                        }
                    }
                    if (nbGoodResponse == validationDrawings.Count)
                    {
                        break;
                    }
                    else
                    {
                        nbGoodResponse = 0;
                    }
                }

                labelsListBox.Items.Clear();
                labelsListBox.Items.Add("Outputs : "); labelsListBox.Items.Add("");
                foreach (string msg in listMsg)
                {
                    labelsListBox.Items.Add(msg);
                }

                using (Graphics g = pictureBox1.CreateGraphics())
                {
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.Alignment = StringAlignment.Center;
                    stringFormat.LineAlignment = StringAlignment.Center;

                    g.DrawString("Training complete", new Font("Consolas", 20), Brushes.Black, pictureBox1.ClientRectangle, stringFormat);
                }
            }

            else
            {
                MessageBox.Show("Veuillez entrer des dessins dans le test set et dans le validation set", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void TrainButton_Click(object sender, EventArgs e)
        {
            Train();
        }



        private bool Prediction(float[,] drawing = null, string label = null)
        {
            bool response = false;
            if (drawing == null)
            {
                drawing = CropDrawing;
                labelsListBox.Items.Clear();
                labelsListBox.Items.Add("Predictions : "); labelsListBox.Items.Add("");
            }
            float[] prediction = NeuralNetwork(false, drawing);

            int indexMax = 0;
            float predictionMax = prediction[0];
            for (int i = 0; i < numOutputs; i++)
            {
                if (drawing == CropDrawing)
                {
                    string msg = labels[i] + " : " + (prediction[i] * 100) + " %";
                    labelsListBox.Items.Add(msg);
                }
                if (prediction[i] > predictionMax)
                {
                    indexMax = i;
                    predictionMax = prediction[i];
                }
            }

            //float[,] averageDrawing = new float[dim/reductionPrecision, dim / reductionPrecision];
            //int compt = 0;
            //foreach (Tuple<float[,], string> draw in trainingDrawings)
            //{
            //    if(draw.Item2 == labels[indexMax])
            //    {
            //        compt += 1;
            //        for (int i=0;i< dim / reductionPrecision; i++)
            //        {
            //            for (int j = 0; j < dim / reductionPrecision; j++)
            //            {
            //                averageDrawing[i, j] += draw.Item1[i, j];
            //            }
            //        }

            //    }
            //}

            //pictureBox2.Refresh();
            //for (int i = 0; i < dim / reductionPrecision; i++)
            //{
            //    for (int j = 0; j < dim / reductionPrecision; j++)
            //    {
            //        using (Graphics g2 = pictureBox2.CreateGraphics())
            //        {
            //            Color color = Color.FromArgb((int)(255 * averageDrawing[i, j]/compt), (int)(255 * averageDrawing[i, j] / compt), (int)(255 * averageDrawing[i, j] / compt));
            //            Brush brush = new SolidBrush(color);
            //            g2.FillRectangle(Brushes.Black, new Rectangle(i, j, 1, 1));
            //        }
            //    }
            //}


            if (labels[indexMax] == label)
            {
                response = true;
            }
            return response;
        }

        private void PredictButton_Click(object sender, EventArgs e)
        {
            Prediction();
        }

        #endregion


        #region ======================================================= Neural network ====================================================
        // Réseau de neurones simple a 1 layer : 
        // - prenant en entrée l'etat des pixels du dessin (le pixel vaut 1 si il est dessine, 0 sinon)
        // - prédit les probabilites pour chaque label (chien, homme, etc). il y a numOutputs labels
        // --> permettra de determiner quel lqbel porte le dessin



        static public float Sigmoid(float x)
        {
            // activation function sigmoid (we chose this one because generate a number btw 0 and 1)
            return (float)(1 / (1 + Math.Exp(-x)));
        }

        static public float Deriv(float x, Func<float, float> f)
        {
            // Define the derivative of the activation function (for backpropagation)
            return f(x) * (1 - f(x));
        }

        public float[] NeuralNetwork(bool train, float[,] inputGrid, int[] targetOutput = null)
        {
            float[] outputPrediction = new float[numOutputs];

            if (inputGrid != null)
            {

                List<float> input = new List<float>();
                for (int i = 0; i < inputGrid.GetLength(0); i++)
                {
                    for (int j = 0; j < inputGrid.GetLength(1); j++)
                    {
                        //Normalize btw -3 and 3 the inputs
                        float value = 6 * inputGrid[i, j] - 3;
                        input.Add(value);
                    }
                }

                if (train && targetOutput != null)
                {

                    // Forward pass
                    float[] hiddenLayer = new float[numNeurons];
                    for (int j = 0; j < numNeurons; j++)
                    {
                        float sum = 0;
                        for (int k = 0; k < numInputs; k++)
                        {
                            sum += input[k] * hiddenWeights[k, j];
                        }
                        sum += hiddenBiases[j];
                        hiddenLayer[j] = Sigmoid(sum);
                    }

                    float[] output = new float[numOutputs];
                    for (int j = 0; j < numOutputs; j++)
                    {
                        float sum = 0;
                        for (int k = 0; k < numNeurons; k++)
                        {
                            sum += hiddenLayer[k] * outputWeights[k, j];
                        }
                        sum += outputBiases[j];
                        output[j] = Sigmoid(sum);
                    }

                    // Backward pass
                    float[] outputErrors = new float[numOutputs];
                    for (int j = 0; j < numOutputs; j++)
                    {
                        outputErrors[j] = (targetOutput[j] - output[j]) * Deriv(output[j], Sigmoid);
                    }
                    float[] hiddenErrors = new float[numNeurons];
                    for (int j = 0; j < numNeurons; j++)
                    {
                        float weightedErrorSum = 0;
                        for (int k = 0; k < numOutputs; k++)
                        {
                            weightedErrorSum += outputErrors[k] * outputWeights[j, k];
                        }
                        hiddenErrors[j] = weightedErrorSum * Deriv(hiddenLayer[j], Sigmoid);
                    }


                    // Update weights and biases
                    for (int j = 0; j < numNeurons; j++)
                    {
                        for (int k = 0; k < numOutputs; k++)
                        {
                            outputWeights[j, k] += learningRate * outputErrors[k] * hiddenLayer[j];
                            outputBiases[k] += learningRate * outputErrors[k];
                        }
                    }
                    for (int j = 0; j < numInputs; j++)
                    {
                        for (int k = 0; k < numNeurons; k++)
                        {
                            hiddenWeights[j, k] += learningRate * hiddenErrors[k] * input[j];
                            hiddenBiases[k] += learningRate * hiddenErrors[k];
                        }
                    }
                }


                else //train = false, prediction
                {
                    // Forward pass
                    float[] hiddenLayer = new float[numNeurons];
                    for (int j = 0; j < numNeurons; j++)
                    {
                        float sum = 0;
                        for (int k = 0; k < numInputs; k++)
                        {
                            sum += input[k] * hiddenWeights[k, j];
                        }
                        sum += hiddenBiases[j];
                        hiddenLayer[j] = Sigmoid(sum);
                    }

                    // Prediction
                    for (int j = 0; j < numOutputs; j++)
                    {
                        float predict = 0;
                        for (int k = 0; k < numNeurons; k++)
                        {
                            predict += hiddenLayer[k] * outputWeights[k, j];
                        }
                        predict += outputBiases[j];
                        outputPrediction[j] = Sigmoid(predict);
                    }

                }

            }


            return outputPrediction; //retourne les predictions de probabilites pour les differents labels de dessins
        }






        #endregion


        #region ======================================================= Parameters of the Form =============================================

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            previousLocation = e.Location;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                using (Graphics g1 = pictureBox1.CreateGraphics())
                {
                    g1.DrawLine(Pens.Black, previousLocation, e.Location);
                }
                previousLocation = e.Location;
                CurrentDrawing[e.Location.X, e.Location.Y] = 1;

                if (e.Location.X < xMin)
                {
                    xMin = e.Location.X;
                }
                else if (e.Location.X >= xMax && e.Location.X != xMin)
                {
                    xMax = e.Location.X;
                }
                if (e.Location.Y < yMin)
                {
                    yMin = e.Location.Y;
                }
                else if (e.Location.Y >= yMax && e.Location.Y != yMin)
                {
                    yMax = e.Location.Y;
                }
                textBox2.Text = "xMin = " + xMin + "; yMin = " + yMin + "; xMax = " + xMax + "; yMax = " + yMax;
                textBox2.Refresh();


                if (xMin == dim && xMax != 0)
                {
                    xMin = xMax - 1;
                }
                else if (xMin != dim && xMax == 0)
                {
                    xMax = xMin + 1;
                }
                if (yMin == dim && yMax != 0)
                {
                    yMin = yMax - 1;
                }
                else if (yMin != dim && yMax == 0)
                {
                    yMax = yMin + 1;
                }

            }
        }


        

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isDrawing = false;
            pictureBox2.Refresh();
            InitializeDataGrid();

            if (xMin == dim && xMax != 0)
            {
                xMin = xMax - 1;
            }
            else if (xMin != dim && xMax == 0)
            {
                xMax = xMin + 1;
            }
            if (yMin == dim && yMax != 0)
            {
                yMin = yMax - 1;
            }
            else if (yMin != dim && yMax == 0)
            {
                yMax = yMin + 1;
            }

            int width = Math.Abs(xMax - xMin);
            int height = Math.Abs(yMax - yMin);
            float scaleX = (float)pictureBox2.Width / (float)width;
            float scaleY = (float)pictureBox2.Height / (float)height;
            CropDrawing = new float[dim / reductionPrecision, dim / reductionPrecision];

            for (int x = 0; x < dim; x++)
            {
                for (int y = 0; y < dim; y++)
                {
                    if (CurrentDrawing[x,y]>0)
                    {
                        int resizedX = (int)((x - xMin) * scaleX);
                        int resizedY = (int)((y - yMin) * scaleY);
                        textBox4.Text = "resizedX = " + resizedX + "; resizedY = " + resizedY;
                        textBox4.Refresh();

                        // Mettre à 1 les pixels environnants dans CropDrawing
                        int halfThickness = epaisseur / 2;
                        int startX = resizedX - halfThickness;
                        int startY = resizedY - halfThickness;
                        int endX = startX + epaisseur;
                        int endY = startY + epaisseur;
                        int pixelCount = 0;

                        for (int j = startY; j < endY; j++)
                        {
                            for (int i = startX; i < endX; i++)
                            {
                                if (i >= 0 && i < pictureBox2.Width && j >= 0 && j < pictureBox2.Height)
                                {
                                    CropDrawing[i / reductionPrecision, j / reductionPrecision] += CurrentDrawing[x, y];
                                    pixelCount++;
                                }
                            }
                        }

                        if (pixelCount > 0)
                        {
                            float averagePixelValue = CropDrawing[resizedX / reductionPrecision, resizedY / reductionPrecision] / pixelCount;
                            int greyValue = (int)Math.Max(0, Math.Min(255, 255-averagePixelValue * 255));
                            Color color = Color.FromArgb(greyValue, greyValue, greyValue);
                            Brush brush = new SolidBrush(color);

                            using (Graphics g2 = pictureBox2.CreateGraphics())
                            {
                                g2.FillRectangle(brush, new Rectangle(resizedX, resizedY, reductionPrecision, reductionPrecision));
                            }
                            dataGridView1.Rows[resizedY / reductionPrecision].Cells[resizedX / reductionPrecision].Style.BackColor = color;
                        }
                    }
                }
            }

            textBox3.Text = "width = " + width;
        }



        private void SaveValidationButton_Click(object sender, EventArgs e)
        {
            string label = textBoxLabel.Text.Trim();
            if (string.IsNullOrEmpty(label))
            {
                MessageBox.Show("Veuillez entrer un label pour le dessin.", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (!labels.Contains(label))
                {
                    labels.Add(label);
                }
                Tuple<float[,], string> drawing = new Tuple<float[,], string>(CropDrawing, label);
                validationDrawings.Add(drawing);
                numOutputs = labels.Count;

                // on nettoie l'interface
                ErazeButton_Click(sender, e);
                UpdateLabelsListBox(false);
            }
        }

        private void ErazeButton_Click(object sender, EventArgs e)
        {
            CurrentDrawing = new float[dim, dim];
            CropDrawing = new float[dim / reductionPrecision, dim / reductionPrecision];
            xMin = dim;
            xMax = 0;
            yMin = dim;
            yMax = 0;
            pictureBox1.Refresh();
            pictureBox2.Refresh();
            labelsListBox.Items.Clear();
            previousLocation = Point.Empty;
            isDrawing = false;
        }


        private void SaveDrawingButton_Click(object sender, EventArgs e)
        {
            string label = textBoxLabel.Text.Trim();
            if (string.IsNullOrEmpty(label))
            {
                MessageBox.Show("Veuillez entrer un label pour le dessin.", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                if (!labels.Contains(label))
                {
                    labels.Add(label);
                }
                Tuple<float[,], string> drawing = new Tuple<float[,], string>(CropDrawing, label);
                trainingDrawings.Add(drawing);
                numOutputs = labels.Count;

                // on nettoie l'interface
                ErazeButton_Click(sender, e);
                UpdateLabelsListBox(true);
            }
        }

        private void UpdateLabelsListBox(bool training = true)
        {
            labelsListBox.Items.Clear();
            if (training)
            {
                labelsListBox.Items.Add("Elements dans Training set :"); labelsListBox.Items.Add("");
                foreach (Tuple<float[,], string> element in trainingDrawings)
                {
                    labelsListBox.Items.Add(element.Item2);
                }
            }
            else
            {
                labelsListBox.Items.Add("Elements dans Validation set :"); labelsListBox.Items.Add("");
                foreach (Tuple<float[,], string> element in validationDrawings)
                {
                    labelsListBox.Items.Add(element.Item2);
                }
            }

        }


        #endregion

    }
}


