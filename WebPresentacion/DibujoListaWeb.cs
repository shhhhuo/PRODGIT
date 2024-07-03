using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using ClassEntidades;

namespace WebPresentacion
{
    public class DibujoListaWeb
    {
        private Graphics papel;
        private Bitmap imagen1 = new Bitmap(912, 1100);

        public DibujoListaWeb()
        {
            papel = Graphics.FromImage(imagen1);
        }

        public void PuroNodo(int esqx, int esqy, int dx, int dy, SolidBrush b1, SolidBrush b2)
        {
            // Rectángulo grande
            papel.FillRectangle(b1, new Rectangle(esqx, esqy, dx - 30, dy));
            papel.FillRectangle(b2, new Rectangle(esqx + dx - 30, esqy, 30, dy));

            // Efecto 3D del rectángulo grande
            Point[] efecto1 = {
                new Point(esqx, esqy),
                new Point(esqx + 6, esqy - 10),
                new Point(esqx + dx - 24, esqy - 10),
                new Point(esqx + dx - 30, esqy)
            };
            papel.FillPolygon(b1, efecto1);
            papel.DrawPolygon(new Pen(Color.WhiteSmoke, 1), efecto1);

            // Efecto 3D del enlace
            Point[] efecto2 = {
                new Point(esqx + dx - 30, esqy),
                new Point(esqx + dx - 24, esqy - 10),
                new Point(esqx + dx + 6, esqy - 10),
                new Point(esqx + dx, esqy)
            };
            papel.FillPolygon(b2, efecto2);
            papel.DrawPolygon(new Pen(Color.WhiteSmoke, 1), efecto2);

            Point[] efecto3 = {
                new Point(esqx + dx, esqy),
                new Point(esqx + dx + 6, esqy - 10),
                new Point(esqx + dx + 6, esqy + dy - 10),
                new Point(esqx + dx, esqy + dy)
            };
            papel.FillPolygon(b2, efecto3);
            papel.DrawPolygon(new Pen(Color.WhiteSmoke, 1), efecto3);

            // Dibujar enlace
            int xcc = esqx + dx - 15;
            int ycc = esqy + dy / 2;
            papel.FillEllipse(new SolidBrush(Color.Black), new Rectangle(xcc - 8, ycc - 8, 16, 16));
        }

        public void DibujaLista(int inix, int iniy, int dxNodo, int dyNodo, SolidBrush b1, SolidBrush b2, int totalNodo)
        {
            int ex = inix, ey = iniy;
            int xcc = 0, ycc = 0, xt = 0, yt = 0;

            papel.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, imagen1.Width, imagen1.Height));
            Pen lap2 = new Pen(Color.Black, 3);

            for (int a = 1; a <= totalNodo; a++)
            {
                PuroNodo(ex, ey, dxNodo, dyNodo, b1, b2);
                xcc = ex + dxNodo - 15;
                ycc = ey + dyNodo / 2;
                ex = ex + 150;

                if (a % 6 == 0)
                {
                    papel.DrawLine(lap2, xcc, ycc, ex - 35, ycc);
                    xt = ex - 35;
                    yt = ycc + 40;
                    papel.DrawLine(lap2, ex - 35, ycc, xt, yt);
                    ex = inix;
                    ey = ey + 100;
                    papel.DrawLine(lap2, xt, yt, ex - 15, yt);
                    papel.DrawLine(lap2, ex - 15, yt, ex - 15, ey + 25);
                    papel.DrawLine(lap2, ex - 15, ey + 25, ex, ey + 25);
                }
                else if (a == totalNodo)
                {
                    papel.DrawLine(lap2, xcc, ycc, xcc + 35, ycc);
                    DibujaTierra(xcc + 35, ycc);
                }
                else
                {
                    papel.DrawLine(lap2, xcc, ycc, ex, ycc);
                }
            }
        }

        public void DibujaTierra(int x, int y)
        {
            Pen lap2 = new Pen(Color.Black, 3);
            papel.DrawLine(lap2, x - 4, y + 10, x + 5, y + 10);
            papel.DrawLine(lap2, x - 2, y + 15, x + 3, y + 15);
            papel.DrawLine(lap2, x - 1, y + 20, x + 1, y + 20);
            papel.DrawLine(lap2, x, y, x, y + 25);
        }

        public Bitmap ImagenVirtual()
        {
            return imagen1;
        }

        public void DibujaGrafo(List<Nodo> nodos, SolidBrush b1, SolidBrush b2)
        {
            int width = imagen1.Width; // Ancho del Bitmap
            int height = imagen1.Height; // Alto del Bitmap

            // Limpiar el espacio antes de volver a dibujar
            papel.Clear(Color.White);

            // Obtener el número total de nodos y calcular el ángulo entre cada nodo
            int totalNodos = nodos.Count;
            float angleIncrement = (float)(2 * Math.PI / totalNodos);

            // Radio del círculo en el que se distribuirán los nodos
            int radio = Math.Min(width, height) / 3;

            // Centro del área de dibujo
            Point centro = new Point(width / 2, height / 2);

            // Dibuja los nodos
            for (int i = 0; i < totalNodos; i++)
            {
                // Calcular la posición del nodo en coordenadas polares
                float angle = i * angleIncrement;
                int posX = centro.X + (int)(radio * Math.Cos(angle));
                int posY = centro.Y + (int)(radio * Math.Sin(angle));

                // Determina el color según la asistencia
                SolidBrush colorNodo = nodos[i].Datos.Asistencia ? new SolidBrush(Color.LightGreen) : new SolidBrush(Color.LightSalmon);

                // Dibuja el nodo como un círculo
                papel.FillEllipse(colorNodo, posX - 15, posY - 15, 30, 30);
                papel.DrawEllipse(new Pen(Color.Black), posX - 15, posY - 15, 30, 30);

                // Dibuja el texto del nodo 
                papel.DrawString(nodos[i].Datos.Matricula, new Font("Arial", 10), Brushes.Black, posX - 15, posY - 8);

                // Dibuja los arcos salientes del nodo
                foreach (var arco in nodos[i].Arcos)
                {
                    var destino = arco.Destino;
                    int destinoIndex = nodos.IndexOf(destino);

                    if (destinoIndex != -1)
                    {
                        // Calcular la posición del nodo destino en coordenadas polares
                        float destAngle = destinoIndex * angleIncrement;
                        int destPosX = centro.X + (int)(radio * Math.Cos(destAngle));
                        int destPosY = centro.Y + (int)(radio * Math.Sin(destAngle));

                        // Dibuja una línea entre el nodo actual y su destino
                        papel.DrawLine(new Pen(Color.Black), posX, posY, destPosX, destPosY);

                        // Dibuja una flecha en el extremo del arco 
                        DibujaFlecha(new Pen(Color.Black), posX, posY, destPosX, destPosY);
                    }
                }
            }
        }

        private void DibujaFlecha(Pen pen, int x1, int y1, int x2, int y2)
        {
            // Calcula los puntos para dibujar la flecha
            double angle = Math.Atan2(y2 - y1, x2 - x1);
            double offset = 10; // Tamaño de la flecha

            // Puntos de la flecha
            Point arrowPoint1 = new Point((int)(x2 - offset * Math.Cos(angle - Math.PI / 6)), (int)(y2 - offset * Math.Sin(angle - Math.PI / 6)));
            Point arrowPoint2 = new Point((int)(x2 - offset * Math.Cos(angle + Math.PI / 6)), (int)(y2 - offset * Math.Sin(angle + Math.PI / 6)));

            // Dibuja la línea y la flecha
            papel.DrawLine(pen, x1, y1, x2, y2);
            papel.DrawLine(pen, x2, y2, arrowPoint1.X, arrowPoint1.Y);
            papel.DrawLine(pen, x2, y2, arrowPoint2.X, arrowPoint2.Y);
        }

    }
}