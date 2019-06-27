<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPrincipal.Master" AutoEventWireup="true" CodeBehind="ReporteGrafico.aspx.cs" Inherits="ProyectoAMCRL.ReporteGrafico" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="https://cdn.anychart.com/releases/v8/js/anychart-base.min.js?hcode=be5162d915534272a57d0bb781d27f2b"></script>
    <script src="https://cdn.anychart.com/releases/v8/js/anychart-ui.min.js?hcode=be5162d915534272a57d0bb781d27f2b"></script>
    <script src="https://cdn.anychart.com/releases/v8/js/anychart-exports.min.js?hcode=be5162d915534272a57d0bb781d27f2b"></script>
    <link href="https://cdn.anychart.com/releases/v8/css/anychart-ui.min.css?hcode=be5162d915534272a57d0bb781d27f2b" type="text/css" rel="stylesheet">
    <link href="https://cdn.anychart.com/releases/v8/fonts/css/anychart-font.min.css?hcode=be5162d915534272a57d0bb781d27f2b" type="text/css" rel="stylesheet">
    <style type="text/css">
        html, body, #container {
            width: 100%;
            height: 100%;
            margin: 0;
            padding: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <nav id="migajasNav" aria-label="breadcrumb">
        <ol class="breadcrumb">
            <li class="breadcrumb-item"><a href="PaginaPrincipal.aspx">Inicio</a></li>
            <li class="breadcrumb-item"><a href="#">Inventario</a></li>
            <li class="breadcrumb-item active" aria-current="page">Reporte gráfico</li>
        </ol>
    </nav>


     <div id="container"></div>
  <script>
anychart.onDocumentReady(function () {
    // create pie chart with passed data
    var chart = anychart.pie([
        ['Hierro', 6371664],
        ['Aluminio', 789622],
        ['Cobre', 7216301],
        ['Lata', 1486621],
        ['Metal', 1200000]
    ]);

    // set chart title text settings
    chart.title('Distribución de materiales actual en inventario');
    // set chart labels position to outside
    chart.labels().position('outside');
    // set legend title settings
    chart.legend().title()
        .enabled(true)
        .text('Materiales:')
        .padding([0, 0, 10, 0]);

    // set legend position and items layout
    chart.legend()
        .position('center-bottom')
        .itemsLayout('horizontal')
        .align('center');

    // set container id for the chart
    chart.container('container');
    // initiate chart drawing
    chart.draw();
});
</script>
</asp:Content>

