<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebPresentacion.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Agregar Nodo</h2>
             <asp:TextBox ID="TextBoxMatricula" runat="server" Placeholder="Matrícula"></asp:TextBox>
            <asp:CheckBox ID="CheckBoxAsistencia" runat="server" Text="Asistencia" />
            <asp:Button ID="ButtonAddNode" runat="server" Text="Agregar Nodo" OnClick="ButtonAddNode_Click" />

            <h2>Agregar Arco</h2>
            <asp:TextBox ID="TextBoxMatriculaOrigen" runat="server" Placeholder="Matrícula Origen"></asp:TextBox>
            <asp:TextBox ID="TextBoxMatriculaDestino" runat="server" Placeholder="Matrícula Destino"></asp:TextBox>
            <asp:TextBox ID="TextBoxPeso" runat="server" Placeholder="Peso"></asp:TextBox>
            <asp:Button ID="ButtonAddEdge" runat="server" Text="Agregar Arco" OnClick="ButtonAddEdge_Click" />

            <h2>Operaciones con el Grafo</h2>
            <asp:Button ID="ButtonTopologica" runat="server" Text="Buscar Topológica" OnClick="ButtonTopologica_Click" />
            <asp:Button ID="ButtonDijkstra" runat="server" Text="Aplicar Dijkstra" OnClick="ButtonDijkstra_Click" />
            <asp:Button ID="ButtonDFS" runat="server" Text="Recorrido DFS" OnClick="ButtonDFS_Click" />
            <asp:Button ID="ButtonBFS" runat="server" Text="Recorrido BFS" OnClick="ButtonBFS_Click" />

            <h3>Resultados</h3>
            <asp:Label ID="LabelResultado" runat="server"></asp:Label>

            <h2>Visualizar Grafo</h2>
            <asp:Button ID="ButtonVisualize" runat="server" Text="Visualizar Grafo" OnClick="ButtonVisualize_Click" />
            <asp:Image ID="ImageGrafo" runat="server" />
        </div>
    </form>
</body>
</html>
