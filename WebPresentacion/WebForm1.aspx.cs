using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassEntidades;
using ClassLogicaNeg;

namespace WebPresentacion
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private Grafo grafo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                grafo = new Grafo();
                Session["grafo"] = grafo;
            }
            else
            {
                grafo = (Grafo)Session["grafo"];
            }
        }

        protected void ButtonAddNode_Click(object sender, EventArgs e)
        {
            var datos = new PaseLista
            {
                Fecha = DateTime.Now,
                Matricula = TextBoxMatricula.Text,
                Asistencia = CheckBoxAsistencia.Checked
            };
            grafo.InsertarNodo(datos);
            Session["grafo"] = grafo;
        }

        protected void ButtonAddEdge_Click(object sender, EventArgs e)
        {
            var origen = new PaseLista { Matricula = TextBoxMatriculaOrigen.Text };
            var destino = new PaseLista { Matricula = TextBoxMatriculaDestino.Text };
            var peso = double.Parse(TextBoxPeso.Text);
            grafo.InsertarArco(origen, destino, peso);
            Session["grafo"] = grafo;
        }

        protected void ButtonVisualize_Click(object sender, EventArgs e)
        {
            ImageGrafo.ImageUrl = "WebForm2.aspx";
        }

        protected void ButtonTopologica_Click(object sender, EventArgs e)
        {
            var grafo = (Grafo)Session["grafo"];
            if (grafo != null)
            {
                var ordenTopologico = grafo.BusquedaTopologica();

                // Mostrar el resultado en el Label
                LabelResultado.Text = "Orden Topológico: ";
                foreach (var nodo in ordenTopologico)
                {
                    LabelResultado.Text += nodo.Datos.Matricula + " ";
                }
            }
        }

        //private void MarcarCaminoMasCorto(Grafo grafo, Nodo origen, Nodo destino)
        //{
        //    // Reiniciar los colores de todos los nodos
        //    foreach (var nodo in grafo.Nodos)
        //    {
        //        // Asignar un color base a todos los nodos
        //        nodo.Color = Color.DarkSeaGreen;

        //    }

        //    // Marcar el camino más corto desde destino hacia origen
        //    Nodo actual = destino;
        //    while (actual != null && actual != origen)
        //    {
        //        // Asignar color verde al nodo en el camino más corto
        //        actual.Color = Color.Green;
        //        actual = actual.Padre; // Seguir al nodo padre
        //    }
        //}

        protected void ButtonDijkstra_Click(object sender, EventArgs e)
        {
            var grafo = (Grafo)Session["grafo"];
            if (grafo != null)
            {
                // Obtener el origen y destino desde los TextBox o de donde sea que los obtengas
                string matriculaOrigen = TextBoxMatriculaOrigen.Text.Trim();
                string matriculaDestino = TextBoxMatriculaDestino.Text.Trim();

                var nodoOrigen = grafo.Nodos.FirstOrDefault(n => n.Datos.Matricula == matriculaOrigen);
                var nodoDestino = grafo.Nodos.FirstOrDefault(n => n.Datos.Matricula == matriculaDestino);

                if (nodoOrigen != null && nodoDestino != null)
                {
                    var distancias = grafo.Dijkstra(nodoOrigen);

                    double distancia = distancias.ContainsKey(nodoDestino) ? distancias[nodoDestino] : double.PositiveInfinity;

                    if (distancia < double.PositiveInfinity)
                    {
                        // Mostrar el resultado en el Label
                        LabelResultado.Text = $"Distancia más corta desde {matriculaOrigen} a {matriculaDestino}: {distancia}";
                    }
                    else
                    {
                        LabelResultado.Text = $"No hay camino desde {matriculaOrigen} a {matriculaDestino}";
                    }
                }
                else
                {
                    LabelResultado.Text = "Nodos de origen o destino no encontrados";
                }
            }
        }

        protected void MostrarResultado(string titulo, List<Nodo> nodos)
        {
            LabelResultado.Text += "<br>" + titulo + "<br>";
            foreach (var nodo in nodos)
            {
                LabelResultado.Text += nodo.Datos.Matricula + " ";
            }
            LabelResultado.Text += "<br>";
        }

        protected void ButtonDFS_Click(object sender, EventArgs e)
        {
            var grafo = (Grafo)Session["grafo"];
            if (grafo != null)
            {
                // Supongamos que el origen es el primer nodo en la lista 
                var origen = grafo.Nodos.FirstOrDefault();

                var recorridoDFS = grafo.DFS(origen);

                // Mostrar el resultado en el Label
                MostrarResultado("Recorrido DFS:", recorridoDFS);
            }
        }

        protected void ButtonBFS_Click(object sender, EventArgs e)
        {
            var grafo = (Grafo)Session["grafo"];
            if (grafo != null)
            {
                // Supongamos que el origen es el primer nodo en la lista 
                var origen = grafo.Nodos.FirstOrDefault();

                var recorridoBFS = grafo.BFS(origen);

                // Mostrar el resultado en el Label
                MostrarResultado("Recorrido BFS:", recorridoBFS);
            }
        }
    }
}