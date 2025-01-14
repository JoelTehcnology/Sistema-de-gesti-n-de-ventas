﻿using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_Joel_AF
{
    public partial class frmNegocio : Form
    {
        public frmNegocio()
        {
            InitializeComponent();
        }

        public Image ByteToImage(byte[] imageBytes) { 
          MemoryStream ms = new MemoryStream();
            ms.Write(imageBytes,0,imageBytes.Length);
            Image image = new Bitmap(ms);

            return image;   
        
        }
        private void frmNegocio_Load(object sender, EventArgs e)
        {
            bool obtenido = true;
            byte[] byteimage = new CN_Negocio().ObtenerLogo(out obtenido);

            if (obtenido)
                piclogo.Image = ByteToImage(byteimage);

            Negocio datos = new CN_Negocio().ObtenerDatos();

            txtnombre.Text = datos.Nombre;
            txtruc.Text = datos.RUC;
            textdireccion.Text = datos.Direccion;

        }

        private void btnsubir_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.FileName = "Files|*.jpg;*.jpeg;*.png";

            if(openFile.ShowDialog() == DialogResult.OK)
            {
                byte[] byteimage =File.ReadAllBytes(openFile.FileName); 
                bool respuesta = new CN_Negocio().ActualizarLogo(byteimage,out mensaje);

                if (respuesta)
                    piclogo.Image= ByteToImage(byteimage);

                else
                     MessageBox.Show(mensaje,"Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }

    }
}
