using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClassLogicaNeg;

namespace WebPresentacion
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        private DibujoListaWeb objDibuja = new DibujoListaWeb();
        protected void Page_Load(object sender, EventArgs e)
        {
            var grafo = (Grafo)Session["grafo"];
            if (grafo != null)
            {
                int totalNodos = grafo.Nodos.Count;
                objDibuja.DibujaGrafo(grafo.Nodos, new SolidBrush(Color.Blue), new SolidBrush(Color.Orange));

                Bitmap completa = objDibuja.ImagenVirtual();
                Response.ContentType = "image/jpeg";
                completa.Save(Response.OutputStream, ImageFormat.Jpeg);
                Response.End();
            }
        }
    }
}