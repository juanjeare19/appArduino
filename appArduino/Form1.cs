using appArduino.Models;
using appArduino.Storage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace appArduino
{
    public partial class Form1 : Form
    {
        System.IO.Ports.SerialPort port;
        bool isClosed = false;
        private static ModeloColorStorage objStorage;

        public Form1()
        {
            InitializeComponent();
            port = new System.IO.Ports.SerialPort();
            port.PortName = "COM4";
            port.BaudRate = 9600;
            port.ReadTimeout = 500;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread Hilo = new Thread(EscucharSerial);
            Hilo.Start();
        }
        private void EscucharSerial()
        {
            while (!isClosed)
            {
                try
                {
                    string cadena = port.ReadLine();
                    if (cadena == "AZUL CIELO")
                    {
                        label10.Invoke(new MethodInvoker(delegate

                        {

                            label10.Text = cadena;
                            label10.BackColor = Color.SkyBlue;
                            label8.Text = "Elevador Abajo";
                        }));
                    }
                    else if (cadena == "AMARILLO")
                    {
                        label10.Invoke(new MethodInvoker(delegate

                        {
                            label10.Text = cadena;
                            label10.BackColor = Color.Yellow;
                            label8.Text = "Brazo Arriba";
                        }));
                    }
                    else if (cadena == "ROJO")
                    {
                        label10.Invoke(new MethodInvoker(delegate

                        {
                            label10.Text = cadena;
                            label10.BackColor = Color.Red;
                            label8.Text = "Pinza Cierra";
                        }));
                    }
                    else if (cadena == "NO DETECTADO")
                    {
                        label10.Invoke(new MethodInvoker(delegate

                        {
                            label10.Text = "NO DETECTADO";
                            label10.BackColor = Color.Gray;
                            label8.Text = "Movimiento no detectado";
                        }));
                    }
                    else if (cadena != "AZUL CIELO" && cadena != "AMARILLO" && cadena != "ROJO" && cadena != "NO DETECTADO")
                    {
                        label2.Invoke(new MethodInvoker(delegate

                        {
                            label2.Text = cadena;
                            //label10.BackColor = Color.Gray;
                            //label8.Text = "Movimiento no detectado";

                        }));
                    }
                    //EntidadColores color = JsonSerializer.Deserialize<EntidadColores>(cadena);
                    objStorage = new ModeloColorStorage();
                    objStorage.GuardarRegistros(label4.Text, label3.Text, label2.Text, label10.Text, label8.Text);
                    //Console.WriteLine(objStorage.ToString());
                }
                catch(Exception ex)
                {
                    string error = ex.Message;

                    MessageBox.Show("Error: " + error);
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            isClosed = true;
            if (port.IsOpen)
                port.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = DateTime.Now.ToLongTimeString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label4.Text = DateTime.Now.ToLongDateString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                port.Open();
                label5.Text = "CONECTADO";
                label5.BackColor = Color.White;
            }
            catch
            {
                MessageBox.Show("El arduino esta desconectado");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                port.Close();
            }
            catch
            {
                MessageBox.Show("El arduino esta desconectado");
            }
            label5.Text = "DESCONECTADO";
            label5.BackColor = Color.White;
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label8.Text = "";
            label10.Text = "";
        }
    }
}